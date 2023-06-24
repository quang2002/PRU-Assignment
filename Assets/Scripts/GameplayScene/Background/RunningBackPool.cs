namespace GameplayScene.Background
{
    using System;
    using System.Collections.Generic;
    using GDK.ObjectPool.Scripts;
    using UnityEngine;

    public class RunningBackPool : GameObjectPool<RunningBack>
    {
        [Serializable]
        public class RunningBackConfig
        {
            [field: SerializeField]
            public Sprite Sprite { get; set; }

            [field: SerializeField]
            public float OffsetY { get; set; }

            [field: SerializeField]
            public float Speed { get; set; }
        }

        [field: SerializeField]
        public RunningBackConfig[] RunningBackConfigs { get; set; }

        [field: SerializeField]
        public Camera Camera { get; private set; }

        [field: SerializeField]
        public float Interval { get; private set; }

        public float LastSpawnTime { get; private set; }

        public float LeftBound  { get; set; }
        public float RightBound { get; set; }

        private void Awake()
        {
            this.LeftBound  = this.Camera.ViewportToWorldPoint(Vector3.zero).x;
            this.RightBound = this.Camera.ViewportToWorldPoint(Vector3.one).x;
        }

        private void Update()
        {
            if (this.LastSpawnTime + this.Interval < Time.realtimeSinceStartup)
            {
                this.LastSpawnTime = Time.realtimeSinceStartup;
                this.Instantiate();
            }

            var removeList = new HashSet<RunningBack>();
            
            foreach (var children in this.UsedObjects)
            {
                if (children.transform.position.x < this.LeftBound - children.TileWidth / 2)
                {
                    removeList.Add(children);
                }
            }

            foreach (var removeObject in removeList)
            {
                this.Release(removeObject);
            }
        }

        protected override RunningBack CreateObject()
        {
            return new GameObject("RunningBack")
            {
                transform =
                {
                    parent = this.transform
                }
            }.AddComponent<RunningBack>();
        }

        protected override void OnInstantiate(RunningBack obj)
        {
            base.OnInstantiate(obj);

            var runningBackConfig = this.GetRandomRunningBackConfig();

            obj.UpdateSprite(runningBackConfig.Sprite);

            obj.transform.position = new Vector3(
                this.RightBound + obj.TileWidth / 2,
                this.transform.position.y + runningBackConfig.OffsetY,
                this.transform.position.z
            );

            obj.Speed = runningBackConfig.Speed;
        }

        private RunningBackConfig GetRandomRunningBackConfig()
        {
            return this.RunningBackConfigs[UnityEngine.Random.Range(0, this.RunningBackConfigs.Length)];
        }
    }
}