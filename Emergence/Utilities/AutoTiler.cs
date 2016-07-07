using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emergence.Utilities {
    public static class AutoTiler {
        public static char[,] AutoTile(char[,] data, CharSet charSet) {
            var width = data.GetLength(0);
            var height = data.GetLength(1);
            var tiles = new char[width, height];
            for(var x = 0; x < width; ++x) {
                for(var y = 0; y < height; ++y) {
                    tiles[x, y] = GetTile(data, charSet, x, y);
                }
            }
            return tiles;
        }

        private static char GetCharAt(char[,] data, int x, int y) {
            if(x < 0 || x >= data.GetLength(0) || y < 0 || y >= data.GetLength(1) || char.IsLetter(data[x, y])) {
                return ' ';
            } else {
                return data[x, y];
            }
        }

        public static char GetTile(char[,] data, CharSet charSet, int x, int y) {
            if(data[x, y] == '-') {
                return charSet.HorizontalLine;
            } else if(data[x, y] == '|') {
                return charSet.VerticalLine;
            } else if(data[x, y] == ' ') {
                return charSet.Empty;
            } else {
                var top = GetCharAt(data, x, y - 1) != ' ';
                var bottom = GetCharAt(data, x, y + 1) != ' ';
                var left = GetCharAt(data, x - 1, y) != ' ';
                var right = GetCharAt(data, x + 1, y) != ' ';

                if(top && bottom && left && right) {
                    return charSet.Cross;
                } else if(!top && bottom && left && right) {
                    return charSet.STee;
                } else if(top && !bottom && left && right) {
                    return charSet.NTee;
                } else if(top && bottom && !left && right) {
                    return charSet.ETee;
                } else if(top && bottom && left && !right) {
                    return charSet.WTee;
                } else if(!top && !bottom && left && right) {
                    return charSet.HorizontalLine;
                } else if(top && bottom && !left && !right) {
                    return charSet.VerticalLine;
                } else if(!top && bottom && !left && right) {
                    return charSet.NWCorner;
                } else if(!top && bottom && left && !right) {
                    return charSet.NECorner;
                } else if(top && !bottom && !left && right) {
                    return charSet.SWCorner;
                } else if(top && !bottom && left && !right) {
                    return charSet.SECorner;
                }
                return charSet.Pillar;
            }
        }


        public class CharSet {
            public char HorizontalLine,
                        VerticalLine,
                        NECorner,
                        SECorner,
                        SWCorner,
                        NWCorner,
                        NTee,
                        ETee,
                        STee,
                        WTee,
                        Cross,
                        Pillar,
                        Empty;
            public static CharSet GetSingleLineCharSet() {
                return new CharSet() {
                    HorizontalLine = (char)196,
                    VerticalLine = (char)179,
                    NECorner = (char)191,
                    SECorner = (char)217,
                    SWCorner = (char)192,
                    NWCorner = (char)218,
                    NTee = (char)193,
                    ETee = (char)195,
                    STee = (char)194,
                    WTee = (char)180,
                    Cross = (char)197,
                    Pillar = 'O',
                    Empty = ' '
                };
            }
            public static CharSet GetDoubleLineCharSet() {
                return new CharSet() {
                    HorizontalLine = (char)205,
                    VerticalLine = (char)186,
                    NECorner = (char)187,
                    SECorner = (char)188,
                    SWCorner = (char)200,
                    NWCorner = (char)201,
                    NTee = (char)202,
                    ETee = (char)204,
                    STee = (char)203,
                    WTee = (char)185,
                    Cross = (char)206,
                    Pillar = 'O',
                    Empty = ' '
                };
            }
        }
    }
}
