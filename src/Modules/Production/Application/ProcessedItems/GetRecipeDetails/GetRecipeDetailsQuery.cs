using SatisfactoryPlanner.BuildingBlocks.Application;
using SatisfactoryPlanner.Modules.Production.Application.Configuration.Queries;
using SatisfactoryPlanner.Modules.Production.Application.Contracts;
using SatisfactoryPlanner.Modules.Production.Domain.ProcessedItems;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SatisfactoryPlanner.Modules.Production.Application.ProcessedItems.GetRecipeDetails
{
    /// <summary>
    /// A query to get the details of a recipe.
    /// </summary>
    /// <param name="RecipeId">The id of the recipe.</param>
    public record GetRecipeDetailsQuery(string RecipeId) : IQuery<RecipeDetailsDto>;

    internal class GetRecipeDetailsQueryHandler : IQueryHandler<GetRecipeDetailsQuery, RecipeDetailsDto>
    {
        public Task<RecipeDetailsDto> Handle(GetRecipeDetailsQuery request, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                var recipe = Recipe.FindById(request.RecipeId) ?? throw new InvalidCommandException($"Recipe '{request.RecipeId}' not found.");

                return new RecipeDetailsDto
                {
                    Id = recipe.Id,
                    Name = recipe.Name,
                    Type = recipe.Type.ToString(),
                    Ingredients = recipe.Ingredients.ConvertAll(ingredient => new IngredientDto
                    {
                        ItemId = ingredient.Item.Id,
                        ItemName = ingredient.Item.Name,
                        Amount = new AmountDto
                        {
                            AmountPerCycle = ingredient.Amount,
                            AmountPerMinute = GetAmountPerMinute(ingredient.Amount, recipe.ManufacturingTime)
                        }
                    }),
                    Products = recipe.Products.ConvertAll(product => new ProductDto
                    {
                        ItemId = product.Item.Id,
                        ItemName = product.Item.Name,
                        Amount = new AmountDto
                        {
                            AmountPerCycle = product.Amount,
                            AmountPerMinute = GetAmountPerMinute(product.Amount, recipe.ManufacturingTime)
                        }
                    }),
                    ProducedIn = [.. recipe.ProducedIn
                        .Where(building => building.ProductionMethod == ProductionMethod.Automatic)
                        .Select(building => new BuildingDto
                        {
                            Id = building.Id,
                            Name = building.Name,
                            ProductionMethod = building.ProductionMethod.ToString()
                        })]
                };
            });
        }

        private static decimal GetAmountPerMinute(decimal amountPerCycle, ManufacturingTime manufacturingTime) => amountPerCycle * (60 / manufacturingTime.Duration);
    }
}
