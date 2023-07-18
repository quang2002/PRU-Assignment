namespace GameplayScene.Entity
{
    using System;
    using System.Collections.Generic;
    using Models.Blueprint;
    using Zenject;

    public class EnemyBehaviourProvider
    {
        public  DiContainer              Container      { get; }
        public  EnemyBlueprint           EnemyBlueprint { get; }
        private Dictionary<string, Type> IdToTypes      { get; } = new();

        public EnemyBehaviourProvider(List<IEnemyBehaviour> behaviours, DiContainer container, EnemyBlueprint enemyBlueprint)
        {
            this.Container      = container;
            this.EnemyBlueprint = enemyBlueprint;
            foreach (var behaviour in behaviours)
            {
                this.IdToTypes.Add(behaviour.EnemyID, behaviour.GetType());
            }
        }

        public IEnemyBehaviour CreateBehaviour(string id, Enemy enemy)
        {
            if (!this.IdToTypes.TryGetValue(id, out var type))
            {
                return null;
            }

            var behaviour = (IEnemyBehaviour)this.Container.Instantiate(type);

            behaviour.Enemy = enemy;

            behaviour.EnemyRecord = this.EnemyBlueprint[id];

            return behaviour;
        }
    }
}