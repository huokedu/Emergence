using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emergence.Entities.Personnel;

namespace Emergence.Entities.HomeBase.Tasks {
    public class DigNewRoom :BaseTask {
        public override string Title => "Dig New Room";
        public override string GeneralDescription =>
            "Carves out a new room adjacent to this one.";
        public override int MaxCharacters => 3;
        public override int TotalTimeToComplete => 6 - Characters.Count;

        public DigNewRoom(Room room) : base(room) { }

        public override BaseTask Clone() {
            var clone = new DigNewRoom(Room);
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
            return CanDigNorth(homeBase, x, y) ||
                CanDigSouth(homeBase, x, y) ||
                CanDigEast(homeBase, x, y) ||
                CanDigWest(homeBase, x, y);
        }
        public override string[] GetOptions(HomeBase homeBase, int x, int y) {
            var optionList = new List<string>();
            if(CanDigNorth(homeBase, x, y)) optionList.Add("North");
            if(CanDigSouth(homeBase, x, y)) optionList.Add("South");
            if(CanDigEast(homeBase, x, y)) optionList.Add("East");
            if(CanDigWest(homeBase, x, y)) optionList.Add("West");
            return optionList.ToArray();
        }

        private bool CanDigNorth(HomeBase homeBase, int x, int y) {
            var room = homeBase.Rooms[x, y];
            return y > 0 && homeBase.Rooms[x, y - 1] == null;
        }
        private bool CanDigSouth(HomeBase homeBase, int x, int y) {
            var room = homeBase.Rooms[x, y];
            return y < homeBase.Height - 1 &&
                homeBase.Rooms[x, y + 1] == null;
        }
        private bool CanDigEast(HomeBase homeBase, int x, int y) {
            var room = homeBase.Rooms[x, y];
            return x < homeBase.Width - 1 &&
                homeBase.Rooms[x + 1, y] == null;
        }
        private bool CanDigWest(HomeBase homeBase, int x, int y) {
            var room = homeBase.Rooms[x, y];
            return x > 0 && homeBase.Rooms[x - 1, y] == null;
        }
    }
}
