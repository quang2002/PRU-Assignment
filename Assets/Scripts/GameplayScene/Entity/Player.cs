namespace GameplayScene.Entity
{
    using System.Collections.Generic;
    using GameplayScene.Ability.System;
    using Signals;
    using UnityEngine;
    using Zenject;

    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(Animator))]
    public class Player : MonoBehaviour, IEntity
    {
        public List<BaseEffect> Effects { get; } = new();
        public float             Health  { get; set; }

        public SpriteRenderer SpriteRenderer { get; private set; }
        public Animator       Animator       { get; private set; }

        #region Inject

        public SignalBus SignalBus { get; private set; }

        [Inject]
        private void Inject(SignalBus signalBus)
        {
            this.SignalBus = signalBus;

            this.SignalBus.Subscribe<TookDamageSignal>(this.OnTookDamage);
        }

        private void OnTookDamage(TookDamageSignal signal)
        {
            if ((Player)signal.Entity != this)
            {
                return;
            }

            this.SetHurt();
        }

        #endregion

        private void Awake()
        {
            this.SpriteRenderer = this.GetComponent<SpriteRenderer>();
            this.Animator       = this.GetComponent<Animator>();
        }

        private void Update()
        {
        }

        private void InternalAttack()
        {
        }

        private void InternalSkill()
        {
        }

        #region Animator

        private static readonly int AnimationKeyIsAlive = Animator.StringToHash("IsAlive");
        private static readonly int AnimationKeyHurt    = Animator.StringToHash("Hurt");
        private static readonly int AnimationKeyAttack  = Animator.StringToHash("Attack");
        private static readonly int AnimationKeySkill   = Animator.StringToHash("Skill");

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

        public void SetSkill()
        {
            this.Animator.SetTrigger(AnimationKeySkill);
        }

        #endregion
    }
}