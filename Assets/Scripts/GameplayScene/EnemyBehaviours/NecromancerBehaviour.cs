namespace GameplayScene.EnemyBehaviours
{
    using GameplayScene.Ability.Effects;
    using GameplayScene.Ability.System;
    using GameplayScene.Entity;
    using Models.Blueprint;
    using UnityEngine;

    public class NecromancerBehaviour : IEnemyBehaviour
    {
        public string                     EnemyID     => "necromancer";
        public Enemy                      Enemy       { get; set; }
        public EnemyBlueprint.EnemyRecord EnemyRecord { get; set; }

        #region Inject

        public Player         Player         { get; }
        public IAbilitySystem AbilitySystem  { get; }
        public EnemyBlueprint EnemyBlueprint { get; }

        public NecromancerBehaviour(Player player, IAbilitySystem abilitySystem, EnemyBlueprint enemyBlueprint)
        {
            this.Player         = player;
            this.AbilitySystem  = abilitySystem;
            this.EnemyBlueprint = enemyBlueprint;
        }

        #endregion

        public void UpdatePerFrame()
        {
            if ((this.Enemy.transform.position - this.Player.transform.position).sqrMagnitude < 4f)
            {
                this.Enemy.Attack();
                this.Enemy.Rigidbody2D.velocity = Vector2.zero;
            }
            else
            {
                this.Enemy.Rigidbody2D.velocity = new Vector2(-2, this.Enemy.Rigidbody2D.velocity.y);
            }

            this.Enemy.IsMoving = this.Enemy.Rigidbody2D.velocity.x != 0;
        }

        public void OnAttack()
        {
            this.AbilitySystem.ApplyEffect(new BaseEffect.EffectData
            {
                EffectType = typeof(DamageEffect),
                Value      = this.Enemy.Damage
            }, this.Player);
        }

        public void OnDead()
        {
            this.Enemy.Rigidbody2D.velocity = Vector2.zero;
        }
    }
}