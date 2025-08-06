using System.Collections.Generic;

namespace SatisfactoryPlanner.Modules.Production.Application.ProcessedItems.GetItemsToProcess
{
    public class RecipeDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public List<IngredientDto> Ingredients { get; set; }
        public List<ProductDto> Products { get; set; }
    }
}