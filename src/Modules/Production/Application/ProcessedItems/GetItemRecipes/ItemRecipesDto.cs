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

    public class RecipeDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public List<IngredientDto> Ingredients { get; set; }
        public List<ProductDto> Products { get; set; }
    }

    public class IngredientDto
    {
        public string ItemId { get; set; }
        public string ItemName { get; set; }
        public AmountDto Amount { get; set; }
    }

    public class ProductDto
    {
        public string ItemId { get; set; }
        public string ItemName { get; set; }
        public AmountDto Amount { get; set; }
    }

    public class AmountDto
    {
        public decimal AmountPerCycle { get; set; }
        public decimal AmountPerMinute { get; set; }
    }
}