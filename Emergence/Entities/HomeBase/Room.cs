using System.Collections.Generic;
using Emergence.Utilities;
using libtcod;

namespace Emergence.Entities.HomeBase {
    public class Room {
        public RoomType? RoomType { get; set; }
        public Dictionary<Direction, Room> Exits { get; set; }

        public Room(RoomType? roomType = null) {
            RoomType = roomType;
            Exits = new Dictionary<Direction, Room>();
        }

        public void Render(int x, int y) {
            TCODConsole.root.setForegroundColor(TCODColor.black);
            TCODConsole.root.setBackgroundColor(TCODColor.grey);

            TCODConsole.root.doubleLineBox(x, y, 11, 11);
            TCODConsole.root.doubleLineBox(x + 1, y + 1, 9, 9);
            TCODConsole.root.fillRect(x + 2, y + 2, 7, 7, ' ');

            #region Exits
            if(Exits.ContainsKey(Direction.North) && Exits[Direction.North] != null) {
                TCODConsole.root.putChar(x + 2, y, (char)TCODSpecialCharacter.DoubleSE);
                TCODConsole.root.putChar(x + 3, y, (char)TCODSpecialCharacter.DoubleVertLine);
                TCODConsole.root.putChar(x + 3, y + 1, (char)TCODSpecialCharacter.DoubleSE);
                TCODConsole.root.putChar(x + 7, y, (char)TCODSpecialCharacter.DoubleVertLine);
                TCODConsole.root.putChar(x + 7, y + 1, (char)TCODSpecialCharacter.DoubleSW);
                TCODConsole.root.putChar(x + 8, y, (char)TCODSpecialCharacter.DoubleSW);
                TCODConsole.root.hline(x + 4, y, 3, ' ');
                TCODConsole.root.hline(x + 4, y + 1, 3, ' ');
            }
            if(Exits.ContainsKey(Direction.South) && Exits[Direction.South] != null) {
                TCODConsole.root.putChar(x + 2, y + 10, (char)TCODSpecialCharacter.DoubleNE);
                TCODConsole.root.putChar(x + 3, y + 10, (char)TCODSpecialCharacter.DoubleVertLine);
                TCODConsole.root.putChar(x + 3, y + 9, (char)TCODSpecialCharacter.DoubleNE);
                TCODConsole.root.putChar(x + 7, y + 10, (char)TCODSpecialCharacter.DoubleVertLine);
                TCODConsole.root.putChar(x + 7, y + 9, (char)TCODSpecialCharacter.DoubleNW);
                TCODConsole.root.putChar(x + 8, y + 10, (char)TCODSpecialCharacter.DoubleNW);
                TCODConsole.root.hline(x + 4, y + 9, 3, ' ');
                TCODConsole.root.hline(x + 4, y + 10, 3, ' ');
            }
            if(Exits.ContainsKey(Direction.East) && Exits[Direction.East] != null) {
                TCODConsole.root.putChar(x + 10, y + 2, (char)TCODSpecialCharacter.DoubleSW);
                TCODConsole.root.putChar(x + 10, y + 3, (char)TCODSpecialCharacter.DoubleHorzLine);
                TCODConsole.root.putChar(x + 9, y + 3, (char)TCODSpecialCharacter.DoubleSW);
                TCODConsole.root.putChar(x + 10, y + 7, (char)TCODSpecialCharacter.DoubleHorzLine);
                TCODConsole.root.putChar(x + 9, y + 7, (char)TCODSpecialCharacter.DoubleNW);
                TCODConsole.root.putChar(x + 10, y + 8, (char)TCODSpecialCharacter.DoubleNW);
                TCODConsole.root.vline(x + 9, y + 4, 3, ' ');
                TCODConsole.root.vline(x + 10, y + 4, 3, ' ');
            }
            if(Exits.ContainsKey(Direction.West) && Exits[Direction.West] != null) {
                TCODConsole.root.putChar(x, y + 2, (char)TCODSpecialCharacter.DoubleSE);
                TCODConsole.root.putChar(x, y + 3, (char)TCODSpecialCharacter.DoubleHorzLine);
                TCODConsole.root.putChar(x + 1, y + 3, (char)TCODSpecialCharacter.DoubleSE);
                TCODConsole.root.putChar(x, y + 7, (char)TCODSpecialCharacter.DoubleHorzLine);
                TCODConsole.root.putChar(x + 1, y + 7, (char)TCODSpecialCharacter.DoubleNE);
                TCODConsole.root.putChar(x, y + 8, (char)TCODSpecialCharacter.DoubleNE);
                TCODConsole.root.vline(x, y + 4, 3, ' ');
                TCODConsole.root.vline(x + 1, y + 4, 3, ' ');
            }
            #endregion
        }
        public void RenderLabel(int x, int y) {
            var labelLines = RoomType.HasValue
                ? RoomType.Value.GetLabel().Split('\n')
                : new string[] { "Empty" };
            if(labelLines.Length > 1) {
                y -= 1;
            }
            for(int i = 0; i < labelLines.Length; ++i) {
                TCODConsole.root.printEx(x + 5, y + 5 + i,
                    TCODBackgroundFlag.None,
                    TCODAlignment.CenterAlignment,
                    labelLines[i]);
            }
        }

        public void AddExit(Direction direction, Room room) {
            Exits.Add(direction, room);
            room.Exits.Add(direction.GetOpposite(), this);
        }
    }
}
