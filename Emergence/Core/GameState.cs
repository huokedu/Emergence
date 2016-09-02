using System;
using System.Collections.Generic;
using Emergence.Entities;
using Emergence.Entities.Personnel;

namespace Emergence.Core {
    public class GameState {
        public int MaxPersonnel { get; set; }
        public List<Character> Personnel { get; set; }

        public Dictionary<SupplyType, int> MaxSupplies { get; set; }
        public Dictionary<SupplyType, int> Supplies { get; set; }

        public GameState() {
            MaxPersonnel = 5;
            Personnel = new List<Character>();
            MaxSupplies = new Dictionary<SupplyType, int>();
            Supplies = new Dictionary<SupplyType, int>();

            foreach(var supplyType in Enum.GetValues(typeof(SupplyType))) {
                MaxSupplies.Add((SupplyType)supplyType, 30);
                Supplies.Add((SupplyType)supplyType, 15);
            }
        }
    }
}
