namespace GameplayScene.Entity
{
    using System.Collections.Generic;
    using System.Linq;
    using GameplayScene.Ability.System;
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

        public HashSet<Enemy> Enemies { get; } = new();

        #region Serialize Fields

        [field: SerializeField]
        public Slider HealthSlider { get; private set; }

        #endregion

        #region Inject

        private SignalBus               SignalBus               { get; set; }
        private MainLocalDataController MainLocalDataController { get; set; }
        private VFXService              VFXService              { get; set; }
        private EffectFactory           EffectFactory           { get; set; }

        [Inject]
        private void Inject(SignalBus               signalBus,
                            MainLocalDataController mainLocalDataController,
                            VFXService              vfxService,
                            EffectFactory           effectFactory)
        {
            this.SignalBus               = signalBus;
            this.MainLocalDataController = mainLocalDataController;
            this.VFXService              = vfxService;
            this.EffectFactory           = effectFactory;

            this.SignalBus.Subscribe<TookDamageSignal>(this.OnTookDamage);
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
        }

        private void Update()
        {
            if (!this.IsAlive())
            {
                this.SetIsAlive(false);
            }

            if (this.Enemies.Any(VisibleInScreen))
            {
                this.SetAttack();
            }

            this.HealthSlider.value = this.Health / this.MainLocalDataController.MaxHealth;
        }

        private void InternalAttack()
        {
            _ = this.VFXService.SpawnVFX("vfx-attack", this.transform.position);

            foreach (var enemy in this.Enemies.Where(VisibleInScreen))
            {
                this.EffectFactory
                    .Create(new SkillBlueprint.EffectRecord("damage", 1000, 0))
                    .ApplyEffect(enemy);
            }
        }

        private void InternalResetHealth()
        {
            this.Health = this.MainLocalDataController.MaxHealth;
        }

        private void OnTookDamage(TookDamageSignal signal)
        {
            if (signal.Entity is not Player || !this.IsAlive())
            {
                return;
            }

            this.SetHurt();
        }

        private static bool VisibleInScreen(Component e)
        {
            var screenPoint = Camera.main!.WorldToScreenPoint(e.transform.position);
            return screenPoint.x < Screen.width && screenPoint.y < Screen.height && screenPoint is { x: > 0, y: > 0 };
        }

        #region Animator

        private static readonly int AnimationKeyIsAlive = Animator.StringToHash("IsAlive");
        private static readonly int AnimationKeyHurt    = Animator.StringToHash("Hurt");
        private static readonly int AnimationKeyAttack  = Animator.StringToHash("Attack");

        public void SetIsAlive(bool isAlive)
        {
            this.Animator.SetBool(AnimationKeyIsAlive, isAlive);
        }

        public void SetHurt()
        {
            this.Animator.SetTrigger(AnimationKeyHurt);
        }

        public void SetAttack()
        {
            this.Animator.SetTrigger(AnimationKeyAttack);
        }

        #endregion
    }
}