using System.Collections.Generic;

namespace SatisfactoryPlanner.Modules.Production.Application.ProcessedItems.GetItemsToProcess
{
    public class ItemRecipesDto
    {
        /// <summary>
        /// The recipes that use this item as an ingredient.
        /// </summary>
        public List<RecipeDto> IngredientRecipes { get; set; }

        /// <summary>
        /// The recipes that produce this item.
        /// </summary>
        public List<RecipeDto> ProductRecipes { get; set; }
    }
}