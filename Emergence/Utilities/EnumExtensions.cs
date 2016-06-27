using System;

namespace Emergence.Utilities {
    public static class EnumExtensions {
        public static T PickRandom<T>(Random random) {
            var values = Enum.GetValues(typeof(T));
            return (T)values.GetValue(random.Next(values.Length));
        }
    }
}
