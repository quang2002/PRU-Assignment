namespace GameplayScene.MapGenerator
{
    using System;
    using UnityEngine;
    using UnityEngine.Tilemaps;

    [Serializable]
    public class MapTiles
    {
        [field: SerializeField]
        public Tile BlockCenterTile { get; private set; }
    }
}