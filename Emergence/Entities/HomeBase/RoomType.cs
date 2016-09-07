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
        private static Dictionary<RoomType, string> Labels => 
            new Dictionary<RoomType, string> {
                { RoomType.LivingQuarters, "Living\nQrtrs" },
                { RoomType.FiringRange, "Firing\nRange" },
                { RoomType.Hospital, "Hsptl" },
                { RoomType.Generator, "Genratr" },
                { RoomType.Workshop, "Wrkshop" },
            };
        private static Dictionary<RoomType, string> DisplayNames =>
            new Dictionary<RoomType, string> {
                { RoomType.LivingQuarters, "Living Quarters" },
                { RoomType.FiringRange, "Firing Range" },
            };

        public static string GetLabel(this RoomType roomType) {
            return Labels.ContainsKey(roomType)
                ? Labels[roomType] : roomType.ToString();
        }

        public static string GetName(this RoomType roomType) {
            return DisplayNames.ContainsKey(roomType)
                ? DisplayNames[roomType] : roomType.ToString();
        }

        public static RoomType FromName(string roomTypeName) {
            if(DisplayNames.ContainsValue(roomTypeName)) {
                var keyValuePair =
                    DisplayNames.FirstOrDefault(kvp => kvp.Value == roomTypeName);
                return keyValuePair.Key;
            }
            return (RoomType)Enum.Parse(typeof(RoomType), roomTypeName);
        }
    }
}
