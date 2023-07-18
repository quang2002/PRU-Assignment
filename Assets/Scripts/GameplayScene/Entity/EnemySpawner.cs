namespace GameplayScene.Entity
{
    using Models.Blueprint;
    using Utilities;
    using Zenject;

    public class EnemySpawner : ITickable
    {
        #region Inject

        public EnemyObjectPool EnemyObjectPool { get; }
        public EnemyBlueprint  EnemyBlueprint  { get; }

        public EnemySpawner(EnemyObjectPool enemyObjectPool, EnemyBlueprint enemyBlueprint)
        {
            this.EnemyObjectPool = enemyObjectPool;
            this.EnemyBlueprint  = enemyBlueprint;
        }

        #endregion

        public static float SpawnInterval => 3f;

        private float SpawnIntervalCounter { get; set; }

        public void Tick()
        {
            if (this.SpawnIntervalCounter < 0)
            {
                this.SpawnIntervalCounter = SpawnInterval;
                this.SpawnRandomEnemy();
            }
            else
            {
                this.SpawnIntervalCounter -= UnityEngine.Time.deltaTime;
            }
        }

        private void SpawnRandomEnemy()
        {
            var id = this.EnemyBlueprint.Keys.GetRandom();

            this.EnemyObjectPool.Instantiate(new EnemyObjectPool.Model
            {
                ID    = id,
                Level = 1
            });
        }
    }
}