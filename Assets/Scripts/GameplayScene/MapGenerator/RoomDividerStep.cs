namespace GameplayScene.MapGenerator
{
    using System;
    using System.Collections.Generic;
    using GDK.GDKUtils.Scripts;
    using UnityEngine;
    using Random = UnityEngine.Random;

    public class RoomDividerStep : IMapGenerateStep
    {
        public MapGeneratorSettings Settings { get; set; }
        public dynamic              Input    { get; set; }
        public dynamic              Output   { get; set; }


        private HashSet<Room> Rooms { get; } = new();
        private Bounds        bounds;

        public void Execute()
        {
            Random.InitState(this.Settings.Seed);

            var originRoom = new Room
            {
                BoundsInt = new BoundsInt(Vector3Int.zero, this.Settings.MapSize)
            };

            this.DivideRecursive(originRoom);

            this.Output.Rooms = this.Rooms;
            this.Output.CellBounds = new BoundsInt(
                (int)this.bounds.min.x,
                (int)this.bounds.min.y,
                0,
                (int)this.bounds.size.x,
                (int)this.bounds.size.y,
                1
            );
        }

        private void DivideRecursive(Room room)
        {
            var divideDirection = this.GetDivideDirection(room);

            switch (divideDirection)
            {
                case DivideDirection.None:
                    this.bounds.Encapsulate(room.BoundsInt.ToBounds());
                    this.Rooms.Add(room);
                    return;
                case DivideDirection.Horizontal:
                    DivideHorizontal();
                    break;
                case DivideDirection.Vertical:
                    DivideVertical();
                    break;
                case DivideDirection.Both:
                    if (Random.value > 0.5f) DivideHorizontal();
                    else DivideVertical();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            void DivideHorizontal()
            {
                var randomX = Random.Range(
                    this.Settings.MinRoomSize.x,
                    this.Settings.MaxRoomSize.x
                );

                var room1 = new Room
                {
                    BoundsInt = new BoundsInt(
                        room.BoundsInt.position,
                        new Vector3Int(
                            randomX,
                            room.BoundsInt.size.y,
                            1)
                    )
                };

                var room2 = new Room
                {
                    BoundsInt = new BoundsInt(
                        new Vector3Int(
                            room.BoundsInt.position.x + randomX + this.Settings.RoomGap,
                            room.BoundsInt.position.y,
                            0),
                        new Vector3Int(
                            room.BoundsInt.size.x - randomX - this.Settings.RoomGap,
                            room.BoundsInt.size.y,
                            1)
                    )
                };

                this.DivideRecursive(room1);
                this.DivideRecursive(room2);
            }

            void DivideVertical()
            {
                var randomY = Random.Range(
                    this.Settings.MinRoomSize.y,
                    this.Settings.MaxRoomSize.y
                );

                var room1 = new Room
                {
                    BoundsInt = new BoundsInt(
                        room.BoundsInt.position,
                        new Vector3Int(
                            room.BoundsInt.size.x,
                            randomY,
                            1)
                    )
                };

                var room2 = new Room
                {
                    BoundsInt = new BoundsInt(
                        new Vector3Int(
                            room.BoundsInt.position.x,
                            room.BoundsInt.position.y + randomY + this.Settings.RoomGap,
                            0),
                        new Vector3Int(
                            room.BoundsInt.size.x,
                            room.BoundsInt.size.y - randomY - this.Settings.RoomGap,
                            1)
                    )
                };

                this.DivideRecursive(room1);
                this.DivideRecursive(room2);
            }
        }

        private DivideDirection GetDivideDirection(Room room)
        {
            var direction = DivideDirection.None;

            if (room.BoundsInt.size.x > this.Settings.MinRoomSize.x + this.Settings.MaxRoomSize.x)
            {
                direction |= DivideDirection.Horizontal;
            }

            if (room.BoundsInt.size.y > this.Settings.MinRoomSize.y + this.Settings.MaxRoomSize.y)
            {
                direction |= DivideDirection.Vertical;
            }

            return direction;
        }

        [Flags]
        public enum DivideDirection
        {
            None       = 0,
            Horizontal = 1 << 0,
            Vertical   = 1 << 1,
            Both       = Horizontal | Vertical
        }
    }
}