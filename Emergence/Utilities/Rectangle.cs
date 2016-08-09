using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emergence.Utilities {
    public class Rectangle {
        public int Left { get; set; }
        public int Right { get; set; }
        public int Top { get; set; }
        public int Bottom { get; set; }
        public int Width { get { return (Right - Left) + 1; } }
        public int Height { get { return (Bottom - Top) + 1; } }

        public Rectangle(int left, int top, int right, int bottom) {
            Left = left;
            Top = top;
            Right = right;
            Bottom = bottom;
        }

        public bool Intersects(Rectangle other) {
            var intersectsHorizontally = 
                (Left >= other.Left && Left <= other.Right) ||
                (Right >= other.Left && Right <= other.Right);
            var intersectsVertically =
                (Top >= other.Top && Top <= other.Bottom) ||
                (Bottom >= other.Top && Bottom <= other.Bottom);
            return intersectsHorizontally && intersectsVertically;
        }

        public void Fill(Action<int, int> fillAction) {
            for(int x = Left; x <= Right; ++x) {
                for(int y = Top; y <= Bottom; ++y) {
                    fillAction(x, y);
                }
            }
        }
    }
}
