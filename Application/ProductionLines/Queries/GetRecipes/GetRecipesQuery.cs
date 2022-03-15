using Services.SFGame;
using System.Collections.Generic;
using System.Linq;

namespace Application.ProductionLines.Queries.GetRecipes
{
    public class GetRecipesQuery
    {
        public IEnumerable<object> Execute(string itemId)
        {
            var gameData = new SFGameService()
                .GetGameData();

            return gameData
                .Recipes
                .Where(recipe => recipe.Products.Any(product => product.Item?.ClassName == itemId))
                .Select(recipe => new
                {
                    Id = recipe.FullName,
                    Name = recipe.DisplayName,
                    Ingredients = recipe.Ingredients.Select(ingredient => new
                    {
                        Id = ingredient.Item.ClassName,
                        Name = ingredient.Item.DisplayName,
                        Amount = ingredient.Amount,
                        ItemsPerMinute = (60 / recipe.ManufacturingDuration) * ingredient.Amount
                    }),
                    Products = recipe.Products.Select(product => new
                    {
                        Id = product.Item.ClassName,
                        Name = product.Item.DisplayName,
                        Amount = product.Amount,
                        ItemsPerMinute = (60 / recipe.ManufacturingDuration) * product.Amount
                    })
                });
        }
    }
}
