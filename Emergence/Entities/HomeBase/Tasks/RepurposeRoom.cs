using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emergence.Entities.Personnel;

namespace Emergence.Entities.HomeBase.Tasks {
    public class RepurposeRoom : BaseTask {
        public override string Title => "Re-purpose Room";
        public override string GeneralDescription =>
            "Sets a room up for a specific use.";
        public override int MaxCharacters => 2;
        public override int TotalTimeToComplete => 4 - Characters.Count;

        public RepurposeRoom(Room room) : base(room) { }

        public override BaseTask Clone() {
            var clone = new RepurposeRoom(Room);
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
            return room != null && room.RoomType == RoomType.Empty;
        }
        public override string[] GetOptions(HomeBase homeBase, int x, int y) {
            var roomTypes = new List<string>();
            foreach(var value in Enum.GetValues(typeof(RoomType))) {
                roomTypes.Add(((RoomType)value).GetName());
            }
            return roomTypes.ToArray();
        }
    }
}
