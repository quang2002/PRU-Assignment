namespace GameplayScene.Player
{
    using System;
    using UnityEngine;

    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(Animator))]
    public class Player : MonoBehaviour
    {
        public SpriteRenderer SpriteRenderer { get; private set; }
        public Animator       Animator       { get; private set; }

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

        private static readonly int AnimationKeyIsAlive = Animator.StringToHash("isAlive");
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