using Services.SFGame;
using System.Collections.Generic;
using System.Linq;

namespace Application.ProductionLines.Queries.GetItems
{
    /// <summary>
    /// Get items that can be produced in a production line.
    /// </summary>
    public class GetItemsQuery
    {
        public IEnumerable<ItemViewModel> Execute()
        {
            var gameData = new SFGameService()
                .GetGameData();

            var recipeProducts = gameData
                .Recipes
                .SelectMany(recipe => recipe.Products);

            var producableItems = gameData
                .Items
                .Where(item => recipeProducts.Any(product => product.Item.ClassName == item.ClassName))
                .ToList();

            var items = new List<ItemViewModel>();
            items.AddRange(producableItems
                .Where(item => item.Category == ItemCategory.Ingot)
                .OrderBy(item => item.ResourceSinkPoints)
                .Select(ViewModel));

            items.AddRange(producableItems
                .Where(item => item.Category == ItemCategory.Part || item.Category == ItemCategory.Fluid)
                .OrderBy(item => item.Category)
                .ThenBy(item => item.DisplayName)
                .Select(ViewModel));

            items.AddRange(producableItems
                .Where(item => item.Category == ItemCategory.ProjectAssembly)
                .OrderBy(item => item.ClassName)
                .Select(ViewModel));

            return items;
        }

        private static ItemViewModel ViewModel(Item item)
        {
            return new ItemViewModel { Category = item.Category.ToString(), Id = item.ClassName, Name = item.DisplayName };
        }
    }
}
