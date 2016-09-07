using System;
using System.Collections.Generic;
using Emergence.Entities.Personnel;
using Emergence.Utilities;

namespace Emergence.Entities.HomeBase.Tasks {
    public class DigNewRoom :BaseTaskType {
        public override string Title => "Dig New Room";
        public override string GeneralDescription =>
            "Carves out a new room adjacent to this one.";
        public override int MaxCharacters => 3;
        public override int TotalTimeToComplete => 6;

        public override bool IsValid(Character character) {
            return true;
        }
        public override bool IsValid(Room room) {
            if(room == null) return false;
            return CanDigNorth(room) ||
                CanDigSouth(room) ||
                CanDigEast(room) ||
                CanDigWest(room);
        }
        public override string[] GetOptions(Room room) {
            var optionList = new List<string>();
            if(CanDigNorth(room)) optionList.Add("North");
            if(CanDigSouth(room)) optionList.Add("South");
            if(CanDigEast(room)) optionList.Add("East");
            if(CanDigWest(room)) optionList.Add("West");
            return optionList.ToArray();
        }

        public override void Process(Task task) {
            var direction = (Direction)Enum.Parse(
                typeof(Direction), task.SelectedOption);
            Room room = task.Room;
            Room otherRoom = null;

            switch(direction) {
                case Direction.North:
                    room.HomeBase.Rooms[room.X, room.Y - 1] =
                        new Room(room.HomeBase, room.X, room.Y - 1, RoomType.Empty);
                    otherRoom = room.HomeBase.Rooms[room.X, room.Y - 1];
                    break;
                case Direction.South:
                    room.HomeBase.Rooms[room.X, room.Y + 1] =
                        new Room(room.HomeBase, room.X, room.Y + 1, RoomType.Empty);
                    otherRoom = room.HomeBase.Rooms[room.X, room.Y + 1];
                    break;
                case Direction.East:
                    room.HomeBase.Rooms[room.X + 1, room.Y] =
                        new Room(room.HomeBase, room.X + 1, room.Y, RoomType.Empty);
                    otherRoom = room.HomeBase.Rooms[room.X + 1, room.Y];
                    break;
                case Direction.West:
                    room.HomeBase.Rooms[room.X - 1, room.Y] =
                        new Room(room.HomeBase, room.X - 1, room.Y, RoomType.Empty);
                    otherRoom = room.HomeBase.Rooms[room.X - 1, room.Y];
                    break;
            }

            room.AddExit(direction, otherRoom);
        }

        private bool CanDigNorth(Room room) {
            return room.Y > 0 &&
                room.HomeBase.Rooms[room.X, room.Y - 1] == null;
        }
        private bool CanDigSouth(Room room) {
            return room.Y < room.HomeBase.Height - 1 &&
                room.HomeBase.Rooms[room.X, room.Y + 1] == null;
        }
        private bool CanDigEast(Room room) {
            return room.X < room.HomeBase.Width - 1 &&
                room.HomeBase.Rooms[room.X + 1, room.Y] == null;
        }
        private bool CanDigWest(Room room) {
            return room.X > 0 &&
                room.HomeBase.Rooms[room.X - 1, room.Y] == null;
        }
    }
}
