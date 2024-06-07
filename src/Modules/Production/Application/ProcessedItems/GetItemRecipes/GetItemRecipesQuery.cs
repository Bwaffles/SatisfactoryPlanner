using SatisfactoryPlanner.BuildingBlocks.Application;
using SatisfactoryPlanner.Modules.GameData.GameData;
using SatisfactoryPlanner.Modules.Production.Application.Configuration.Queries;
using SatisfactoryPlanner.Modules.Production.Application.Contracts;
using SatisfactoryPlanner.Modules.Production.Domain.ProcessedItems;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SatisfactoryPlanner.Modules.Production.Application.ProcessedItems.GetItemsToProcess
{
    public record GetItemRecipesQuery(string ItemId) : IQuery<ItemRecipesDto>;

    internal class GetItemRecipesQueryHandler : IQueryHandler<GetItemRecipesQuery, ItemRecipesDto>
    {
        public async Task<ItemRecipesDto> Handle(GetItemRecipesQuery request, CancellationToken cancellationToken)
        {
            if (!Item.All.Any(item => item.Id == request.ItemId))
                throw new InvalidCommandException($"Item '{request.ItemId}' not found.");

            return await GetItemRecipes(request.ItemId);
        }

        private static Task<ItemRecipesDto> GetItemRecipes(string itemId)
        {
            var automatableRecipes = Recipe.All
                .Where(recipe => recipe.CanBeAutomated())
                .ToList();

            return Task.Run(() => new ItemRecipesDto()
            {
                ProductRecipes = automatableRecipes
                    .Where(recipe => recipe.Produces(itemId))
                    .Select(ConvertToDto)
                    .ToList(),
                IngredientRecipes = automatableRecipes
                    .Where(recipe => recipe.ConsumesIngredient(itemId))
                    .Select(ConvertToDto)
                    .ToList()
            });
        }

        private static RecipeDto ConvertToDto(Recipe recipe) => new()
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
            })
        };

        private static decimal GetAmountPerMinute(decimal amountPerCycle, ManufacturingTime manufacturingTime) => amountPerCycle * (60 / manufacturingTime.Duration);
    }
}
