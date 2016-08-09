using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emergence.Utilities {
    public enum Direction {
        North = 0,
        NorthEast,
        East,
        SouthEast,
        South,
        SouthWest,
        West,
        NorthWest
    }

    public static class DirectionExtensions {
        public static bool IsCardinal(this Direction direction) {
            return (int)direction % 2 == 0;
        }
        public static bool IsOrdinal(this Direction direction) {
            return !IsCardinal(direction);
        }
        public static IEnumerable<Direction> GetAllDirections() {
            foreach(var direction in Enum.GetValues(typeof(Direction))) {
                yield return (Direction)direction;
            }
        }
        public static IEnumerable<Direction> GetCardinalDirections() {
            foreach(var direction in Enum.GetValues(typeof(Direction))) {
                if(IsCardinal((Direction)direction)) {
                    yield return (Direction)direction;
                }
            }
        }
        public static IEnumerable<Direction> GetOrdinalDirections() {
            foreach(var direction in Enum.GetValues(typeof(Direction))) {
                if(IsOrdinal((Direction)direction)) {
                    yield return (Direction)direction;
                }
            }
        }

        public static Direction GetOpposite(this Direction direction) {
            return (Direction)(((int)direction + 4) % 8);
        }
    }
}
