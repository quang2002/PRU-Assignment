namespace GameplayScene.Entity
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using GameplayScene.Ability.System;
    using GDK.AssetsManager;
    using GDK.ObjectPool;
    using Models.Blueprint;
    using Models.DataControllers;
    using Services;
    using Signals;
    using UnityEditor.Animations;
    using UnityEngine;
    using Zenject;

    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(BoxCollider2D))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class Enemy : MonoBehaviour, IEntity, IPooledObject, IDisposable
    {
        public List<BaseEffect>   Effects    { get; } = new();
        public float              Health     { get; set; }
        public float              Damage     { get; set; }
        IObjectPool IPooledObject.ObjectPool { get; set; }

        #region Properties

        private static readonly int AnimationKeyHealth = Animator.StringToHash("Health");
        private static readonly int AnimationKeyAttack = Animator.StringToHash("Attack");
        private static readonly int AnimationKeyMoving = Animator.StringToHash("Moving");

        public bool IsMoving
        {
            get => this.Animator.GetBool(AnimationKeyMoving);
            set => this.Animator.SetBool(AnimationKeyMoving, value);
        }

        public bool HasBehaviour => this.EnemyBehaviour is not null;

        public IEnemyBehaviour EnemyBehaviour { get; private set; }

        public Animator      Animator   { get; private set; }
        public BoxCollider2D Collider2D { get; private set; }

        public SpriteRenderer SpriteRenderer { get; private set; }

        public Rigidbody2D Rigidbody2D { get; private set; }

        #endregion

        #region Inject

        private IAssetsManager          AssetsManager           { get; set; }
        private EnemyBlueprint          EnemyBlueprint          { get; set; }
        private EnemyBehaviourProvider  EnemyBehaviourProvider  { get; set; }
        private Player                  Player                  { get; set; }
        private SignalBus               SignalBus               { get; set; }
        private DamageTextService       DamageTextService       { get; set; }
        private MainLocalDataController MainLocalDataController { get; set; }

        [Inject]
        private void Inject(IAssetsManager          assetsManager,
                            EnemyBehaviourProvider  enemyBehaviourProvider,
                            EnemyBlueprint          enemyBlueprint,
                            MainLocalDataController mainLocalDataController,
                            Player                  player,
                            SignalBus               signalBus,
                            DamageTextService       damageTextService)
        {
            this.AssetsManager           = assetsManager;
            this.EnemyBlueprint          = enemyBlueprint;
            this.EnemyBehaviourProvider  = enemyBehaviourProvider;
            this.Player                  = player;
            this.SignalBus               = signalBus;
            this.DamageTextService       = damageTextService;
            this.MainLocalDataController = mainLocalDataController;

            signalBus.Subscribe<TookDamageSignal>(this.OnTookDamageSignal);
        }

        #endregion

        private void Awake()
        {
            this.Animator       = this.GetComponent<Animator>();
            this.Collider2D     = this.GetComponent<BoxCollider2D>();
            this.SpriteRenderer = this.GetComponent<SpriteRenderer>();
            this.Rigidbody2D    = this.GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            this.Animator.SetInteger(AnimationKeyHealth, (int)this.Health);
            this.Collider2D.size = this.SpriteRenderer.size;
            this.EnemyBehaviour?.UpdatePerFrame();

            if (!this.IsAlive() && this.HasBehaviour)
            {
                this.SignalBus.Fire(new EnemyDeadSignal
                {
                    EnemyRecord = this.EnemyBehaviour!.EnemyRecord
                });

                this.EnemyBehaviour!.OnDead();
                this.EnemyBehaviour = null;
                var deathAnimationLength = this.Animator.GetNextAnimatorStateInfo(0).length;
                Task.Delay((int)(deathAnimationLength * 1000)).ContinueWith(_ => this.Release());
            }
        }

        public void Attack()
        {
            this.Animator.SetTrigger(AnimationKeyAttack);
        }

        public void InternalAttack()
        {
            this.EnemyBehaviour?.OnAttack();
        }

        public void TransformTo(EnemyBlueprint.EnemyRecord enemyRecord, uint level)
        {
            var animatorController = this.AssetsManager.Load<AnimatorController>(enemyRecord.AnimatorController);
            var enemyID            = string.Empty;

            foreach (var (key, value) in this.EnemyBlueprint)
                if (value == enemyRecord)
                {
                    enemyID = key;
                    break;
                }

            if (enemyID == string.Empty)
                throw new Exception($"Enemy ID not found for {enemyRecord.Name}.");

            this.Animator.runtimeAnimatorController = animatorController;
            this.Animator.Rebind();

            this.Health = enemyRecord.BaseHealth + enemyRecord.HealthInc * level;
            this.Damage = enemyRecord.BaseDamage + enemyRecord.DamageInc * level;

            this.EnemyBehaviour = this.EnemyBehaviourProvider.CreateBehaviour(enemyID, this);

            this.Player.Enemies.Add(this);
        }

        private void OnTookDamageSignal(TookDamageSignal signal)
        {
            if (signal.Entity is not Enemy enemy || enemy != this || !this.HasBehaviour)
            {
                return;
            }

            var (damage, critical) = this.MainLocalDataController.GetDamageWithCritical(signal.Damage);

            this.DamageTextService.Instantiate(new DamageTextService.Model
            {
                Damage   = (long)damage,
                Position = this.transform.position + Vector3.up * 0.4f,
                Critical = critical
            });
        }

        public void Dispose()
        {
            this.EnemyBehaviour                     = null;
            this.Animator.runtimeAnimatorController = null;
            this.Player.Enemies.Remove(this);
        }
    }
}