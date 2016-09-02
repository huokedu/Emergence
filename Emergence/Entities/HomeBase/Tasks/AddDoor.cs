using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emergence.Entities.Personnel;

namespace Emergence.Entities.HomeBase.Tasks {
    public class AddDoor : BaseTask {
        public override string Title => "Add Door";
        public override string GeneralDescription =>
            "Adds a door between two existing rooms.";
        public override int MaxCharacters => 1;
        public override int TotalTimeToComplete => 1;

        public AddDoor(Room room) : base(room) { }

        public override BaseTask Clone() {
            var clone = new AddDoor(Room);
            clone.Characters.AddRange(Characters);
            clone.TimeSpent = TimeSpent;
            clone.SelectedOption = SelectedOption;
            return clone;
        }

        public override bool IsValid(Character character) {
            return true;
        }
        public override bool IsValid(HomeBase homeBase, int x, int y) {
            var room = homeBase.Rooms[x, y];
            if(room == null) return false;
            return CanAddNorth(homeBase, x, y) ||
                CanAddSouth(homeBase, x, y) ||
                CanAddEast(homeBase, x, y) ||
                CanAddWest(homeBase, x, y);
        }
        public override string[] GetOptions(HomeBase homeBase, int x, int y) {
            var optionList = new List<string>();
            if(CanAddNorth(homeBase, x, y)) optionList.Add("North");
            if(CanAddSouth(homeBase, x, y)) optionList.Add("South");
            if(CanAddEast(homeBase, x, y)) optionList.Add("East");
            if(CanAddWest(homeBase, x, y)) optionList.Add("West");
            return optionList.ToArray();
        }

        private bool CanAddNorth(HomeBase homeBase, int x, int y) {
            var room = homeBase.Rooms[x, y];
            return y > 0 && homeBase.Rooms[x, y - 1] != null && 
                !room.Exits.ContainsKey(Utilities.Direction.North);
        }
        private bool CanAddSouth(HomeBase homeBase, int x, int y) {
            var room = homeBase.Rooms[x, y];
            return y < homeBase.Height - 1 && 
                homeBase.Rooms[x, y + 1] != null &&
                !room.Exits.ContainsKey(Utilities.Direction.South);
        }
        private bool CanAddEast(HomeBase homeBase, int x, int y) {
            var room = homeBase.Rooms[x, y];
            return x < homeBase.Width - 1 && 
                homeBase.Rooms[x + 1, y] != null &&
                !room.Exits.ContainsKey(Utilities.Direction.East);
        }
        private bool CanAddWest(HomeBase homeBase, int x, int y) {
            var room = homeBase.Rooms[x, y];
            return x > 0 && homeBase.Rooms[x - 1, y] != null &&
                !room.Exits.ContainsKey(Utilities.Direction.West);
        }

    }
}
