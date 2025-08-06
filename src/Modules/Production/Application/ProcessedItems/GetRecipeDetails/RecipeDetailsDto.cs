using System.Collections.Generic;

namespace SatisfactoryPlanner.Modules.Production.Application.ProcessedItems.GetRecipeDetails
{
    public class RecipeDetailsDto
    {
        /// <summary>
        /// The id of the recipe.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The name of the recipe.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The type of the recipe, whether it's the standard or an alternate recipe.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// The ingredients processed in this recipe.
        /// </summary>
        public List<IngredientDto> Ingredients { get; set; }

        /// <summary>
        /// The products produced by this recipe.
        /// </summary>
        public List<ProductDto> Products { get; set; }

        /// <summary>
        /// The buildings that this recipe can be produced in.
        /// </summary>
        public List<BuildingDto> ProducedIn { get; set; }
    }
}
