using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using libtcod;

namespace Emergence.Ui {
    public class BlockingOptionModal {
        public TCODColor Foreground { get; set; }
        public TCODColor Background { get; set; }
        public string Message { get; set; }
        public string[] Options { get; set; }

        private int LongestLine { get; set; } = 0;
        private int X, Y;
        private int Width, Height;
        private int SelectedIndex { get; set; }

        public string Show() {
            LongestLine = Math.Max(Message.Length, Options.Max(line => line.Length));
            X = (TCODConsole.root.getWidth() / 2) - (Message.Length / 2 + 2);
            Width = Math.Max(13, LongestLine + 4); // Two for padding and two for the border
            Y = (TCODConsole.root.getHeight() / 2) - 3;
            Height = Options.Length + 6; // 2 for border, 2 for padding, 2 for title w/ padding

            TCODKey key = null;
            Render();
            do {
                key = TCODConsole.waitForKeypress(true);
                if(key.KeyCode == TCODKeyCode.Up) {
                    SelectedIndex -= 1;
                    if(SelectedIndex < 0) SelectedIndex = Options.Length - 1;
                } else if (key.KeyCode == TCODKeyCode.Down) {
                    SelectedIndex += 1;
                    if(SelectedIndex >= Options.Length) SelectedIndex = 0;
                }
                Render();
            } while(key.KeyCode != TCODKeyCode.Enter && key.KeyCode != TCODKeyCode.Escape);

            return key.KeyCode == TCODKeyCode.Enter ? Options[SelectedIndex] : null;
        }

        private void Render() {
            TCODConsole.root.setForegroundColor(Foreground);
            TCODConsole.root.setBackgroundColor(Background);
            TCODConsole.root.printFrame(X, Y, Width, Height, true, TCODBackgroundFlag.Set);
            TCODConsole.root.print(X + 2, Y + 2, Message);
            for(int i = 0; i < Options.Length; ++i) {
                TCODConsole.root.print(X + 2, Y + 4 + i, Options[i]);
                if(i == SelectedIndex) {
                    TCODConsole.root.putChar(X + 1, Y + 4 + i, (char)TCODSpecialCharacter.ArrowEast);
                    TCODConsole.root.putChar(X + Width - 2, Y + 4 + i, (char)TCODSpecialCharacter.ArrowWest);
                }
            }
            TCODConsole.flush();
        }
    }
}
