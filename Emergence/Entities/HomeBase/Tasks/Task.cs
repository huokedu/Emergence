using System.Collections.Generic;
using Emergence.Entities.Personnel;

namespace Emergence.Entities.HomeBase.Tasks {
    public class Task {
        public BaseTaskType TaskType { get; private set; }
        public Room Room { get; private set; }
        public string SelectedOption { get; private set; }
        public List<Character> Characters { get; private set; }
        public int Progress { get; private set; }
        public bool Done { get; private set; }

        public Task(BaseTaskType taskType, Room room, string selectedOption) {
            TaskType = taskType;
            Room = room;
            SelectedOption = selectedOption;
            Characters = new List<Character>();
            Progress = 0;
        }

        public void Update() {
            if(!Done) {
                Progress += Characters.Count;
                if(Progress >= TaskType.TotalTimeToComplete) {
                    TaskType.Process(this);
                    Done = true;
                }
            }
        }
    }
}
