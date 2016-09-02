using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emergence.Entities.Personnel;

namespace Emergence.Entities.HomeBase.Tasks {
    public class ClearRoom : BaseTask {
        public override string Title => "Clear Room";
        public override string GeneralDescription =>
            "Clears all furniture from a room, preparing it for repurposing.";
        public override int MaxCharacters => 1;
        public override int TotalTimeToComplete => 1;

        public ClearRoom(Room room) : base(room) { }
        public override BaseTask Clone() {
            var clone = new ClearRoom(Room);
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
            return room != null && room.RoomType != RoomType.Empty;
        }
        public override string[] GetOptions(HomeBase homeBase, int x, int y) {
            return null;
        }
    }
}
