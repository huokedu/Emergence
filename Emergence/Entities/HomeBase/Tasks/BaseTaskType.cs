using System.Collections.Generic;
using Emergence.Entities.Personnel;

namespace Emergence.Entities.HomeBase.Tasks {
    public abstract class BaseTaskType {
        public abstract string Title { get; }
        public abstract string GeneralDescription { get; }
        public abstract int MaxCharacters { get; }
        public abstract int TotalTimeToComplete { get; }

        public abstract bool IsValid(Room room);
        public abstract bool IsValid(Character character);
        public abstract string[] GetOptions(Room room);

        public Task CreateTask(Room room, string selectedOption) {
            return new Task(this, room, selectedOption);
        }

        public override string ToString() {
            return Title;
        }

        public abstract void Process(Task task);
    }
}
