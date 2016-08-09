using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using libtcod;

namespace Emergence.Utilities {
    public static class TCODConsoleExtensions {
        public static void hline(this TCODConsole console, int x, int y, int l, char c) {
            for(int i = 0; i < l; ++i) {
                console.putChar(x + i, y, c);
            }
        }
        public static void vline(this TCODConsole console, int x, int y, int l, char c) {
            for(int i = 0; i < l; ++i) {
                console.putChar(x, y + i, c);
            }
        }
        public static void doubleLineBox(this TCODConsole console, int x, int y, int w, int h) {
            TCODConsole.root.hline(x, y, w, (char)TCODSpecialCharacter.DoubleHorzLine);
            TCODConsole.root.hline(x, y + h - 1, w, (char)TCODSpecialCharacter.DoubleHorzLine);
            TCODConsole.root.vline(x, y, h, (char)TCODSpecialCharacter.DoubleVertLine);
            TCODConsole.root.vline(x + w - 1, y, h, (char)TCODSpecialCharacter.DoubleVertLine);
            TCODConsole.root.putChar(x, y, (char)TCODSpecialCharacter.DoubleNW);
            TCODConsole.root.putChar(x, y + h - 1, (char)TCODSpecialCharacter.DoubleSW);
            TCODConsole.root.putChar(x + w - 1, y, (char)TCODSpecialCharacter.DoubleNE);
            TCODConsole.root.putChar(x + w - 1, y + h  - 1, (char)TCODSpecialCharacter.DoubleSE);
        }

        public static void fillRect(this TCODConsole console, int x, int y, int w, int h, char c) {
            for(int i = 0; i < h; ++i) {
                console.hline(x, y + i, w, c);
            }
        }
    }
}
