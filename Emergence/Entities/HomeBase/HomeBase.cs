using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emergence.Entities.HomeBase {
    public class HomeBase {
        public Room[,] Rooms { get; set; }
        public int Width { get { return Rooms.GetLength(0); } }
        public int Height { get { return Rooms.GetLength(1); } }

        private static Tasks.BaseTask[] AllTasks { get; } =
            new Tasks.BaseTask[] {
                new Tasks.AddDoor(null),
                new Tasks.ClearRoom(null),
                new Tasks.DigNewRoom(null),
                new Tasks.RepurposeRoom(null)
            };

        public HomeBase(int width, int height) {
            Rooms = new Room[width, height];
        }

        public void Render(int xOffset, int yOffset, bool renderLabels) {
            for(int x = 0; x < Width; ++x) {
                for(int y = 0; y < Height; ++y) {
                    if(Rooms[x, y] != null) {
                        Rooms[x, y].Render((x * 11) + xOffset, (y * 11) + yOffset);
                        if(renderLabels) {
                            Rooms[x, y].RenderLabel((x * 11) + xOffset, (y * 11) + yOffset);
                        }
                    }
                }
            }
        }

        public List<Tasks.BaseTask> GetValidTasks(int x, int y) {
            var room = Rooms[x, y];
            var tasks = new List<Tasks.BaseTask>();
            foreach(var task in AllTasks) {
                if(task.IsValid(this, x, y)) {
                    tasks.Add(task.Clone());
                }
            }
            return tasks;
        }
    }
}
