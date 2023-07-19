namespace GameplayScene.Entity
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Common;
    using GameplayScene.Ability.Effects;
    using GameplayScene.Ability.System;
    using GameplayScene.Screens;
    using GDK.UIManager;
    using Models.Blueprint;
    using Models.DataControllers;
    using Services;
    using Signals;
    using UnityEngine;
    using UnityEngine.UI;
    using Zenject;

    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(Animator))]
    public class Player : MonoBehaviour, IEntity
    {
        public List<BaseEffect> Effects { get; } = new();
        public float            Health  { get; set; }

        public SpriteRenderer SpriteRenderer { get; private set; }
        public Animator       Animator       { get; private set; }

        public HashSet<Enemy> Enemies    { get; } = new();
        public bool           IsPlayable { get; private set; }

        #region Serialize Fields

        [field: SerializeField]
        public Slider HealthSlider { get; private set; }

        #endregion

        #region Inject

        private SignalBus               SignalBus               { get; set; }
        private MainLocalDataController MainLocalDataController { get; set; }
        private VFXService              VFXService              { get; set; }
        private IAbilitySystem          AbilitySystem           { get; set; }
        private UIManager               UIManager               { get; set; }

        [Inject]
        private void Inject(SignalBus               signalBus,
                            MainLocalDataController mainLocalDataController,
                            VFXService              vfxService,
                            IAbilitySystem          abilitySystem,
                            UIManager               uiManager)
        {
            this.SignalBus               = signalBus;
            this.MainLocalDataController = mainLocalDataController;
            this.VFXService              = vfxService;
            this.AbilitySystem           = abilitySystem;
            this.UIManager               = uiManager;

            this.SignalBus.Subscribe<TookDamageSignal>(this.OnTookDamage);
            this.SignalBus.Subscribe<EnemyDeadSignal>(this.OnEnemyDead);
        }

        #endregion

        private void Awake()
        {
            this.SpriteRenderer = this.GetComponent<SpriteRenderer>();
            this.Animator       = this.GetComponent<Animator>();
        }

        private void Start()
        {
            this.InternalResetHealth();
            this.IsPlayable = true;
        }

        private void Update()
        {
            if (!this.IsAlive() && this.IsPlayable)
            {
                this.IsPlayable = false;
                this.SetIsAlive(false);

                this.UIManager.OpenScreen<TransitionScreen>(new TransitionScreen.Model
                {
                    WhileTransition = this.Revive,
                    Delay           = 1.0f,
                    Duration        = 1.0f
                });
                return;
            }

            if (this.Enemies.Any(VisibleInScreen))
            {
                this.SetAttack();
            }

            this.HealthSlider.value = this.Health / this.MainLocalDataController.GetStatValue(StatType.Health);
        }

        private void InternalAttack()
        {
            _ = this.VFXService.SpawnVFX("vfx-attack", this.transform.position);

            var damage = this.MainLocalDataController.GetStatValue(StatType.Attack);

            foreach (var enemy in this.Enemies.Where(VisibleInScreen))
            {
                this.AbilitySystem.ApplyEffect(new BaseEffect.EffectData
                {
                    EffectType = typeof(DamageEffect),
                    Value      = damage
                }, enemy);
            }
        }

        private void InternalResetHealth()
        {
            this.Health = this.MainLocalDataController.GetStatValue(StatType.Health);
        }

        private void OnTookDamage(TookDamageSignal signal)
        {
            if (signal.Entity is not Player || !this.IsAlive())
            {
                return;
            }
        }

        private void OnEnemyDead(EnemyDeadSignal signal)
        {
            this.InternalHealthSteal();
        }

        private void InternalHealthSteal()
        {
            var healAmount = this.MainLocalDataController.GetStatValue(StatType.HealthSteal);
            var maxHealth  = this.MainLocalDataController.GetStatValue(StatType.Health);
            this.Health = Mathf.Clamp(this.Health + healAmount, 0, maxHealth);
        }

        private static bool VisibleInScreen(Component e)
        {
            var screenPoint = Camera.main!.WorldToScreenPoint(e.transform.position);
            return screenPoint.x < Screen.width && screenPoint.y < Screen.height && screenPoint is { x: > 0, y: > 0 };
        }

        public void Revive()
        {
            foreach (var enemy in this.Enemies)
            {
                this.AbilitySystem.ApplyEffect(new BaseEffect.EffectData
                {
                    EffectType = typeof(DamageEffect),
                    Value      = enemy.Health
                }, enemy);
            }

            this.IsPlayable = true;
            this.SetIsAlive(true);
            this.InternalResetHealth();
        }

        #region Animator

        private static readonly int AnimationKeyIsAlive = Animator.StringToHash("IsAlive");
        private static readonly int AnimationKeyAttack  = Animator.StringToHash("Attack");

        private void SetIsAlive(bool isAlive)
        {
            this.Animator.SetBool(AnimationKeyIsAlive, isAlive);
        }

        private void SetAttack()
        {
            this.Animator.SetTrigger(AnimationKeyAttack);
        }

        #endregion
    }
}