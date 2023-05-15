namespace GameplayScene.MapGenerator
{
    using System;
    using UnityEngine;

    [Serializable]
    public class MapGeneratorSettings
    {
        [field: SerializeField]
        public int Seed { get; private set; }

        [field: SerializeField]
        public int RoomGap { get; private set; }

        [field: SerializeField]
        public Vector3Int MapSize { get; private set; }

        [field: SerializeField]
        public Vector3Int MinRoomSize { get; private set; }

        [field: SerializeField]
        public Vector3Int MaxRoomSize { get; private set; }

        [field: SerializeField]
        public AnimationCurve NoiseCurve { get; private set; }

        public void SetSeed(int seed)
        {
            this.Seed = seed;
        }
    }
}