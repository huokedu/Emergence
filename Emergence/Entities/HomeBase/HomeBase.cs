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

        private static Tasks.BaseTaskType[] AllTaskTypes { get; } =
            new Tasks.BaseTaskType[] {
                new Tasks.AddDoor(),
                new Tasks.ClearRoom(),
                new Tasks.DigNewRoom(),
                new Tasks.RepurposeRoom()
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

        public List<Tasks.BaseTaskType> GetValidTasks(int x, int y) {
            return AllTaskTypes.Where(t => t.IsValid(Rooms[x, y])).ToList();
        }
    }
}
