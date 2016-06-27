namespace Emergence.Entities {
    public enum SupplyType {
        Medicinal,
        Mechanical,
        Electrical,
        Structural,
        Food,
        Fuel
    }

    public static class SupplyTypeExtensions {
        public static string GetShortName(this SupplyType supplyType) {
            switch(supplyType) {
                case SupplyType.Medicinal:
                    return "Medicl";
                case SupplyType.Mechanical:
                    return "Mchncl";
                case SupplyType.Electrical:
                    return "Elctcl";
                case SupplyType.Structural:
                    return "Strtrl";
                case SupplyType.Food:
                    return "Food";
                case SupplyType.Fuel:
                    return "Fuel";
            }
            return "ERR";
        }
    }
}
