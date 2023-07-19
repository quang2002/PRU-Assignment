namespace GameplayScene.Entity
{
    using Models.Blueprint;

    public interface IEnemyBehaviour
    {
        public string                     EnemyID     { get; }
        public Enemy                      Enemy       { get; set; }
        public EnemyBlueprint.EnemyRecord EnemyRecord { get; set; }

        public void UpdatePerFrame();

        public void OnAttack();

        public void OnDead();
    }
}