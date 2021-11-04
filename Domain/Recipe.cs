using System.Collections.Generic;

namespace Domain
{
    public class Recipe
    {
        /// <summary>
        /// SF id of the recipe (ex. Recipe_CircuitBoard_C, Recipe_Alternate_ElectrodeCircuitBoard_C).
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The name of the recipe (ex. Circuit Board, Alternate: Electrode Circuit Board).
        /// </summary>
        public string Name { get; set; }

        public List<ItemOutput> Outputs { get; set; }

        public List<ItemInput> Inputs { get; set; }
        public Building ProducedIn { get; set; }
    }
}