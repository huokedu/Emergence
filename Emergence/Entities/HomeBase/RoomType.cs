using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emergence.Entities.HomeBase {
    public enum RoomType {
        LivingQuarters,
        Kitchen,
        FiringRange,
        Hostpital,
        Generator,
        Gym,
        Library,
        Workshop
    }

    public static class RoomTypeExtensions {
        public static string GetLabel(this RoomType roomType) {
            switch(roomType) {
                case RoomType.LivingQuarters:
                    return "Living\nQrtrs";
                case RoomType.FiringRange:
                    return "Firing\nRange";
                case RoomType.Hostpital:
                    return "Hstptl";
                case RoomType.Generator:
                    return "Genratr";
                case RoomType.Workshop:
                    return "Wrkshop";
            }
            return roomType.ToString();
        }
    }
}
