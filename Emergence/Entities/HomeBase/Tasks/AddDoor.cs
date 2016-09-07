using System;
using System.Collections.Generic;
using Emergence.Entities.Personnel;
using Emergence.Utilities;

namespace Emergence.Entities.HomeBase.Tasks {
    public class AddDoor : BaseTaskType {
        public override string Title => "Add Door";
        public override string GeneralDescription =>
            "Adds a door between two existing rooms.";
        public override int MaxCharacters => 1;
        public override int TotalTimeToComplete => 1;

        public override bool IsValid(Character character) {
            return true;
        }
        public override bool IsValid(Room room) {
            if(room == null) return false;
            return CanAddNorth(room) ||
                CanAddSouth(room) ||
                CanAddEast(room) ||
                CanAddWest(room);
        }
        public override string[] GetOptions(Room room) {
            var optionList = new List<string>();
            if(CanAddNorth(room)) optionList.Add("North");
            if(CanAddSouth(room)) optionList.Add("South");
            if(CanAddEast(room)) optionList.Add("East");
            if(CanAddWest(room)) optionList.Add("West");
            return optionList.ToArray();
        }

        public override void Process(Task task) {
            var direction = (Direction)Enum.Parse(
                typeof(Direction), task.SelectedOption);
            Room room = task.Room;
            Room otherRoom = null;

            switch(direction) {
                case Direction.North:
                    otherRoom = room.HomeBase.Rooms[room.X, room.Y - 1];
                    break;
                case Direction.South:
                    otherRoom = room.HomeBase.Rooms[room.X, room.Y + 1];
                    break;
                case Direction.East:
                    otherRoom = room.HomeBase.Rooms[room.X + 1, room.Y];
                    break;
                case Direction.West:
                    otherRoom = room.HomeBase.Rooms[room.X - 1, room.Y];
                    break;
            }

            room.AddExit(direction, otherRoom);
        }

        private bool CanAddNorth(Room room) {
            return room.Y > 0 && 
                room.HomeBase.Rooms[room.X, room.Y - 1] != null && 
                !room.Exits.ContainsKey(Utilities.Direction.North);
        }
        private bool CanAddSouth(Room room) {
            return room.Y < room.HomeBase.Height - 1 &&
                room.HomeBase.Rooms[room.X, room.Y + 1] != null &&
                !room.Exits.ContainsKey(Utilities.Direction.South);
        }
        private bool CanAddEast(Room room) {
            return room.X < room.HomeBase.Width - 1 &&
                room.HomeBase.Rooms[room.X + 1, room.Y] != null &&
                !room.Exits.ContainsKey(Utilities.Direction.East);
        }
        private bool CanAddWest(Room room) {
            return room.X > 0 && 
                room.HomeBase.Rooms[room.X - 1, room.Y] != null &&
                !room.Exits.ContainsKey(Utilities.Direction.West);
        }

    }
}
