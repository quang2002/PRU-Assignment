namespace GameplayScene.EnemyBehaviours
{
    using GameplayScene.Ability.System;
    using GameplayScene.Entity;
    using GDK.ObjectPool;
    using Models.Blueprint;
    using UnityEngine;

    public class DeathBringerBehaviour : IEnemyBehaviour
    {
        public string                     EnemyID     => "death-bringer";
        public Enemy                      Enemy       { get; set; }
        public EnemyBlueprint.EnemyRecord EnemyRecord { get; set; }

        #region Inject

        public Player         Player         { get; }
        public EffectFactory  EffectFactory  { get; }
        public EnemyBlueprint EnemyBlueprint { get; }

        public DeathBringerBehaviour(Player player, EffectFactory effectFactory, EnemyBlueprint enemyBlueprint)
        {
            this.Player         = player;
            this.EffectFactory  = effectFactory;
            this.EnemyBlueprint = enemyBlueprint;
        }

        #endregion

        public void UpdatePerFrame()
        {
            if ((this.Enemy.transform.position - this.Player.transform.position).sqrMagnitude < .5f)
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
            this.EffectFactory.Create(new("damage", 100, 0)).ApplyEffect(this.Player);
        }

        public void OnDead()
        {
            this.Enemy.Rigidbody2D.velocity = Vector2.zero;
        }
    }
}