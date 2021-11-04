using System.Collections.Generic;

namespace Services.SFGame
{
    public class Recipe
    {
        public string Name { get; set; }

        /// <summary>
        /// The full name of the recipe.
        /// </summary>
        /// <example>
        /// "BlueprintGeneratedClass /Game/FactoryGame/Recipes/Assembler/Recipe_CircuitBoard.Recipe_CircuitBoard_C"
        /// </example>
        public string FullName { get; set; }
        public string DisplayName { get; internal set; }
        public List<Product> Product { get; internal set; }
    }
}
