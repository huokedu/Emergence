using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emergence.Entities.HomeBase {
    public enum RoomType {
        Empty,
        LivingQuarters,
        Kitchen,
        FiringRange,
        Hospital,
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
                case RoomType.Hospital:
                    return "Hsptl";
                case RoomType.Generator:
                    return "Genratr";
                case RoomType.Workshop:
                    return "Wrkshop";
            }
            return roomType.ToString();
        }

        public static string GetName(this RoomType roomType) {
            switch(roomType) {
                case RoomType.LivingQuarters:
                    return "Living Quarters";
                case RoomType.FiringRange:
                    return "Firing Range";
                case RoomType.Hospital:
                    return "Hospital";
                case RoomType.Generator:
                    return "Generator";
                case RoomType.Workshop:
                    return "Workshop";
            }
            return roomType.ToString();
        }
    }
}
