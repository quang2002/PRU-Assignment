namespace GameplayScene.MapGenerator
{
    using System.Collections.Generic;
    using System.Linq;
    using GDK.GDKUtils.Scripts;
    using UnityEditor;
    using UnityEngine;
    using UnityEngine.Tilemaps;

    public class MapGenerator : MonoBehaviour
    {
        #region Serialized Fields

        [field: SerializeField]
        public MapGeneratorSettings Settings { get; private set; }

        [field: SerializeField]
        public MapTiles Tiles { get; private set; }

        [field: SerializeField]
        public Tilemap Tilemap { get; private set; }

        #endregion

        public void Generate()
        {
            this.Tilemap.ClearAllTiles();

            this.Settings.SetSeed(Random.Range(0, int.MaxValue));

            var output =
                this.UseStep<RoomDividerStep>()
                    .UseStep<OutlineNoiseStep>()
                    .UseStep<PathGenerateStep>()
                    .Output;

            var cellBounds = (BoundsInt)output.CellBounds;

            cellBounds.position -= Vector2Int.one.ToVector3Int();
            cellBounds.size     += Vector2Int.one.ToVector3Int() * 2;

            foreach (var position in cellBounds.allPositionsWithin)
            {
                if ((output.Rooms as HashSet<Room>)?.Any(room => room.BoundsInt.Contains(position)) ?? true)
                {
                    this.Tilemap.SetTile(position, null);
                    continue;
                }

                this.Tilemap.SetTile(position, this.Tiles.BlockCenterTile);
            }
        }
    }

    [CustomEditor(typeof(MapGenerator), true)]
    public class MapGeneratorInspector : Editor
    {
        public MapGenerator MapGenerator => this.target as MapGenerator;

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (GUILayout.Button("Generate"))
                this.MapGenerator.Generate();
        }
    }
}