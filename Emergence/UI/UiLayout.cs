using System;
using System.Collections.Generic;
using System.IO;
using Emergence.Utilities;
using libtcod;

namespace Emergence.Ui {
    public class UiLayout {
        char[,] glyphs { get; set; }
        Dictionary<string, Point> points { get; set; }

        private UiLayout() { }

        public void Render(int xOffset, int yOffset) {
            int width = glyphs.GetLength(0);
            int height = glyphs.GetLength(1);
            for(int x = 0; x < width; ++x) {
                for(int y = 0; y < height; ++y) {
                    if(glyphs[x, y] != ' ') {
                        TCODConsole.root.putChar(x + xOffset, y + yOffset, glyphs[x, y]);
                    }
                }
            }
        }

        public Point GetPoint(string name) {
            if(points.ContainsKey(name)) {
                return points[name];
            } else {
                return null;
            }
        } 

        public static UiLayout Load(string filePath) {
            var uiLayout = new UiLayout();
            uiLayout.points = new Dictionary<string, Point>();

            using(var reader = new StreamReader(filePath)) {
                var tagList = ReadPointNames(reader);
                var characterData = ReadCharacterData(reader);

                int width = characterData.GetLength(0);
                int height = characterData.GetLength(1);
                var charSet = AutoTiler.CharSet.GetSingleLineCharSet();

                uiLayout.glyphs = new char[width, height];
                char currentCharacter;
                for(int y = 0; y < height; ++y) {
                    for(int x = 0; x < width; ++x) {
                        currentCharacter = characterData[x, y];
                        if(char.IsLetter(currentCharacter)) {
                            uiLayout.glyphs[x, y] = charSet.Empty;
                            uiLayout.points.Add(tagList[currentCharacter], new Point(x, y));
                        } else {
                            uiLayout.glyphs[x, y] = AutoTiler.GetTile(characterData, charSet, x, y);
                        }
                    }
                }
            }

            return uiLayout;
        }

        private static Dictionary<char, string> ReadPointNames(StreamReader reader) {
            var tagsLine = reader.ReadLine().Split(':');
            if(tagsLine.Length != 2 || tagsLine[0].Trim().ToLower() != "tags") {
                throw new Exception("Error loading ui layout - first line must be tags count.");
            }
            int tagsCount;
            if(!int.TryParse(tagsLine[1], out tagsCount)) {
                throw new Exception($"Error loading ui layout - {tagsLine[1]} is not a valid tag count.");
            }

            var tagList = new Dictionary<char, string>();
            for(int i = 0; i < tagsCount; ++i) {
                var tagLine = reader.ReadLine();
                var tagLineParts = tagLine.Split(new string[] { "=>" }, StringSplitOptions.RemoveEmptyEntries);
                if(tagLineParts.Length != 2) {
                    throw new Exception($"Error loading ui layout - invalid tag definition '{tagLine}'.");
                }
                tagList.Add(tagLineParts[0].Trim()[0], tagLineParts[1].Trim());
            }

            return tagList;
        }

        private static char[,] ReadCharacterData(StreamReader reader) {
            var sizeLine = reader.ReadLine().Split(':', ',');
            if(sizeLine.Length != 3 || sizeLine[0].Trim().ToLower() != "size") {
                throw new Exception("Error loading ui layout - expected size after tag definitions.");
            }
            int width, height;
            if(!int.TryParse(sizeLine[1], out width) || !int.TryParse(sizeLine[2], out height)) {
                throw new Exception("Error loading ui layout - expected size after tag definitions.");
            }
            
            var characterData = new char[width, height];
            for(int y = 0; y < height; ++y) {
                var glyphLine = reader.ReadLine();
                for(int x = 0; x < width; ++x) {
                    characterData[x, y] = glyphLine[x];
                }
            }

            return characterData;
        }
    }
}
