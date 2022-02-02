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

        /// <summary>
        /// The ingredients required to produce this recipe.
        /// </summary>
        public IEnumerable<Ingredient> Ingredients { get; set; }

        /// <summary>
        /// The products that are created from this recipe.
        /// </summary>
        public IEnumerable<Product> Products { get; set; }

        /// <summary>
        /// The building that this recipe is produced in.
        /// </summary>
        public Building ProducedIn { get; set; }
    }
}