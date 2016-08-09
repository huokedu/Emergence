using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emergence.Utilities;
using libtcod;

namespace Emergence.Entities.ExcursionMap {
    public class ExcursionMap {
        public int Width { get { return Tiles.GetLength(0); } }
        public int Height { get { return Tiles.GetLength(1); } }
        public char[,] Tiles { get; set; }

        public ExcursionMap(int width, int height) {
            Generate(width, height);
        }

        public void Generate(int width, int height) {
            Tiles = new char[width, height];
            var random = new Random();
            var rectangles = new Rectangle[3];
            for(int i = 0; i < rectangles.Length; ++i) {
                rectangles[i] = GenerateRandomRectangle(
                    (Width / 2) + 1, (Width * 3) / 4,
                    (Height / 2) + 1, (Height * 3) / 4,
                    random
                );
            }
            foreach(var rectangle in rectangles) {
                rectangle.Fill((x, y) => Tiles[x, y] = '.');
            }
            Outline('.', '#');
        }

        public void Render(int xOffset, int yOffset) {
            for(int x = 0; x < Width; ++x) {
                for(int y = 0; y < Height; ++y) {
                    TCODConsole.root.putChar(x + xOffset, y + yOffset, Tiles[x, y]);
                }
            }
        }

        private void Outline(char toOutline, char outlineChar) {
            for(int x = 0; x < Width; ++x) {
                for(int y = 0; y < Height; ++y) {
                    if(Tiles[x, y] != toOutline && HasAdjacent(x, y, toOutline)) {
                        Tiles[x, y] = outlineChar;
                    }
                }
            }
        }

        private void Clear() {
            for(int x = 0; x < Width; ++x) {
                for(int y = 0; y < Height; ++y) {
                    Tiles[x, y] = ' ';
                }
            }
        }

        private bool HasAdjacent(int x, int y, char adjacentCheck) {
            var n = y > 0 && Tiles[x, y - 1] == adjacentCheck;
            var ne = y > 0 && x < Width - 1 && Tiles[x + 1, y - 1] == adjacentCheck;
            var e = x < Width - 1 && Tiles[x + 1, y] == adjacentCheck;
            var se = y < Height - 1 && x < Width - 1 && Tiles[x + 1, y + 1] == adjacentCheck;
            var s = y < Height - 1 && Tiles[x, y + 1] == adjacentCheck;
            var sw = y < Height - 1 && x > 0 && Tiles[x - 1, y + 1] == adjacentCheck;
            var w = x > 0 && Tiles[x - 1, y] == adjacentCheck;
            var nw = y > 0 && x > 0 && Tiles[x - 1, y - 1] == adjacentCheck;

            return n || ne || e || se || s || sw || w || nw;
        }

        private Rectangle GenerateRandomRectangle(int minWidth, int maxWidth, int minHeight, int maxHeight, Random random) {
            var width = minWidth + random.Next(maxWidth - minWidth);
            var height = minHeight + random.Next(maxHeight - minHeight);
            var maxX = (Width - width) - 2;
            var maxY = (Height - height) - 2;
            return new Rectangle(
                1 + random.Next(maxX), 1 + random.Next(maxY),
                width, height
            );
        }
    }
}
