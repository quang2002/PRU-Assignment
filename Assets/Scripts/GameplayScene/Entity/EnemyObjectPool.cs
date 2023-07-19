namespace GameplayScene.Entity
{
    using Common;
    using GDK.ObjectPool;
    using Models.Blueprint;
    using Models.DataControllers;
    using UnityEngine;
    using Utilities;
    using Zenject;

    public class EnemyObjectPool : UnityObjectPool<EnemyObjectPool.Model, Enemy>
    {
        #region Inject

        private EnemyBlueprint EnemyBlueprint { get; set; }
        private DiContainer    Container      { get; set; }

        private MainLocalDataController MainLocalDataController { get; set; }

        [Inject]
        public void Inject(EnemyBlueprint          enemyBlueprint,
                           DiContainer             container,
                           MainLocalDataController mainLocalDataController)
        {
            this.EnemyBlueprint          = enemyBlueprint;
            this.Container               = container;
            this.MainLocalDataController = mainLocalDataController;
        }

        #endregion

        [field: SerializeField]
        private float SpawnInterval { get; set; }

        private float SpawnIntervalCounter { get; set; }

        public class Model
        {
            public string ID    { get; init; }
            public uint   Level { get; init; }
        }

        private void Update()
        {
            if (this.SpawnIntervalCounter < 0)
            {
                this.SpawnIntervalCounter = SpawnInterval;
                this.SpawnRandomEnemy();
            }
            else
            {
                this.SpawnIntervalCounter -= Time.deltaTime;
            }
        }

        private void SpawnRandomEnemy()
        {
            var id = this.EnemyBlueprint.Keys.GetRandom();

            this.Instantiate(new Model
            {
                ID    = id,
                Level = this.MainLocalDataController.Level
            });
        }

        protected override Enemy CreateObject(Model model)
        {
            var enemy = new GameObject(nameof(Enemy))
            {
                transform =
                {
                    parent = this.transform
                },
                layer = (int)Layer.Enemy
            }.AddComponent<Enemy>();
            this.Container.Inject(enemy);
            return enemy;
        }

        protected override void OnInstantiate(Enemy obj, Model model)
        {
            obj.transform.localPosition = Vector3.zero;
            obj.gameObject.SetActive(true);

            var enemyRecord = this.EnemyBlueprint[model.ID];
            var enemyLevel  = model.Level;
            obj.TransformTo(enemyRecord, enemyLevel);
        }

        protected override void OnRelease(Enemy obj)
        {
            obj.gameObject.SetActive(false);
            obj.Dispose();
        }
    }
}