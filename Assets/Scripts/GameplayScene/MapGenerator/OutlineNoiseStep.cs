namespace GameplayScene.MapGenerator
{
    using System.Collections.Generic;
    using UnityEngine;

    public class OutlineNoiseStep : IMapGenerateStep
    {
        public MapGeneratorSettings Settings { get; set; }
        public dynamic              Input    { get; set; }
        public dynamic              Output   { get; set; }

        private HashSet<Room> Rooms     { get; set; }
        private BoundsInt     BoundsInt { get; set; }

        public void Execute()
        {
            // this.Rooms     = this.Input.Rooms as HashSet<Room>;
            // this.BoundsInt = (BoundsInt)this.Input.CellBounds;

            this.Output = this.Input;
        }
    }
}