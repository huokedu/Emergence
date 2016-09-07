using System;
using Emergence.Entities.Personnel;

namespace Emergence.Entities.HomeBase.Tasks {
    public class ClearRoom : BaseTaskType {
        public override string Title => "Clear Room";
        public override string GeneralDescription =>
            "Clears all furniture from a room, preparing it for repurposing.";
        public override int MaxCharacters => 1;
        public override int TotalTimeToComplete => 1;

        public override bool IsValid(Character character) {
            return true;
        }
        public override bool IsValid(Room room) {
            return room != null && room.RoomType != RoomType.Empty;
        }
        public override string[] GetOptions(Room room) {
            return null;
        }

        public override void Process(Task task) {
            task.Room.RoomType = RoomType.Empty;
        }
    }
}
