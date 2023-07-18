namespace GameplayScene.Entity
{
    using GDK.ObjectPool;
    using Models.Blueprint;
    using Models.Common;
    using UnityEngine;
    using Zenject;

    public class EnemyObjectPool : UnityObjectPool<EnemyObjectPool.Model, Enemy>
    {
        #region Inject

        private EnemyBlueprint EnemyBlueprint { get; set; }
        private DiContainer    Container      { get; set; }

        [Inject]
        public void Inject(EnemyBlueprint enemyBlueprint, DiContainer container)
        {
            this.EnemyBlueprint = enemyBlueprint;
            this.Container      = container;
        }

        #endregion

        public class Model
        {
            public string ID    { get; init; }
            public uint   Level { get; init; }
        }

        protected override Enemy CreateObject(Model model)
        {
            var enemy = new GameObject(nameof(Enemy))
            {
                transform =
                {
                    parent        = this.transform,
                    localPosition = Vector3.zero
                },
                layer = (int)Layer.Enemy
            }.AddComponent<Enemy>();
            this.Container.Inject(enemy);
            return enemy;
        }

        protected override void OnInstantiate(Enemy obj, Model model)
        {
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