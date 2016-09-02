using System.Collections.Generic;
using Emergence.Entities.Personnel;

namespace Emergence.Entities.HomeBase.Tasks {
    public abstract class BaseTask {
        public Room Room { get; set; }
        public List<Character> Characters { get; set; }
        public int TimeSpent { get; set; }
        public string SelectedOption { get; set; }

        public abstract string Title { get; }
        public abstract string GeneralDescription { get; }
        public abstract int MaxCharacters { get; }
        public abstract int TotalTimeToComplete { get; }

        public abstract bool IsValid(HomeBase homeBase, int x, int y);
        public abstract bool IsValid(Character character);
        public abstract string[] GetOptions(HomeBase homeBase, int x, int y);

        public abstract BaseTask Clone();

        public BaseTask(Room room) {
            Room = room;
            Characters = new List<Character>();
            TimeSpent = 0;
            SelectedOption = null;
        }

        public override string ToString() {
            return Title;
        }
    }
}
