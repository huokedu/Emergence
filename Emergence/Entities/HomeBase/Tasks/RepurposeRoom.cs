using System;
using System.Collections.Generic;
using Emergence.Entities.Personnel;

namespace Emergence.Entities.HomeBase.Tasks {
    public class RepurposeRoom : BaseTaskType {
        public override string Title => "Re-purpose Room";
        public override string GeneralDescription =>
            "Sets a room up for a specific use.";
        public override int MaxCharacters => 2;
        public override int TotalTimeToComplete => 4;

        public override bool IsValid(Character character) {
            return true;
        }
        public override bool IsValid(Room room) {
            return room != null && room.RoomType == RoomType.Empty;
        }
        public override string[] GetOptions(Room room) {
            var roomTypes = new List<string>();
            foreach(var value in Enum.GetValues(typeof(RoomType))) {
                roomTypes.Add(((RoomType)value).GetName());
            }
            return roomTypes.ToArray();
        }

        public override void Process(Task task) {
            task.Room.RoomType =
                RoomTypeExtensions.FromName(task.SelectedOption);
        }
    }
}
