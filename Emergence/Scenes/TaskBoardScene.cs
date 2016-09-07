using System;
using System.Collections.Generic;
using Emergence.Core;
using Emergence.Entities.HomeBase.Tasks;
using Emergence.Entities.Personnel;
using Emergence.Utilities;
using libtcod;

namespace Emergence.Scenes {
    public class TaskBoardScene : BaseScene {
        BaseScene PreviousScene { get; set; }
        List<Character> Characters { get; set; }
        List<Task> Tasks { get; set; }
        Ui.UiList<Task> TaskList { get; set; }

        public TaskBoardScene(Game game, BaseScene previousScene, List<Character> characters, List<Task> tasks) : base(game) {
            PreviousScene = previousScene;
            Characters = characters;
            Tasks = tasks;
            TaskList = new Ui.UiList<Task>(Tasks, RenderTaskListItem);
        }

        public override void Update(float deltaTime) { }

        public override void Render(float deltaTime) {
            TCODConsole.root.setForegroundColor(TCODColor.white);
            TCODConsole.root.setBackgroundColor(TCODColor.darkerGrey);

            RenderTaskList();
        }

        private void RenderTaskList() {
            var offset = new Point(0, 0);
            TaskList.Render(offset);

            offset = new Point(49, 0);
            TaskList.RenderScrollBar(offset, 21);

            offset = new Point(0, 21);
            TCODConsole.root.setForegroundColor(TCODColor.white);
            TCODConsole.root.print(offset.X, offset.Y,
                $"Tasks {(char)TCODSpecialCharacter.ArrowSouth}/{(char)TCODSpecialCharacter.ArrowNorth}");
        }
        private void RenderTaskListItem(Point point, Task task, bool isSelected) {
            var taskName = task.TaskType.Title;
            if(!string.IsNullOrWhiteSpace(task.SelectedOption)) {
                taskName += $" => {task.SelectedOption}";
            }
            if(isSelected) {
                TCODConsole.root.setForegroundColor(TCODColor.white);
                TCODConsole.root.putChar(point.X, point.Y, (char)TCODSpecialCharacter.ArrowEast);
                TCODConsole.root.putChar(point.X + taskName.Length + 1, point.Y, (char)TCODSpecialCharacter.ArrowWest);
            } else {
                TCODConsole.root.setForegroundColor(TCODColor.grey);
            }
            TCODConsole.root.print(point.X + 1, point.Y, taskName);
        }

        public override void KeyPressed(TCODKey keyData) {
            if(keyData.KeyCode == TCODKeyCode.Up) {
                TaskList.SelectedIndex -= 1;
            } else if(keyData.KeyCode == TCODKeyCode.Down) {
                TaskList.SelectedIndex += 1;
            } else if(keyData.KeyCode == TCODKeyCode.Escape) {
                Game.ChangeScene(PreviousScene);
            }
        }
    }
}
