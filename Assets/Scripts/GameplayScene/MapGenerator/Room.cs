namespace GameplayScene.MapGenerator
{
    using System.Collections.Generic;
    using UnityEngine;

    public class Room
    {
        public BoundsInt BoundsInt { get; init; }

        public override bool Equals(object obj)
        {
            if (obj is Room room)
            {
                return this.BoundsInt.Equals(room.BoundsInt);
            }

            return false;
        }

        public override int GetHashCode()
        {
            return this.BoundsInt.GetHashCode();
        }
    }
}