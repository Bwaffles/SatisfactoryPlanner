using SatisfactoryPlanner.BuildingBlocks.Domain;
using System.Collections.Generic;
using System.Linq;

namespace SatisfactoryPlanner.Modules.Factories.Domain.ProcessedItems
{
    public class Recipe : ValueObject
    {
        private readonly List<Ingredient> _ingredients;

        private Recipe(List<Ingredient> ingredients)
        {
            _ingredients = ingredients;
        }

        public bool HasIngredient(ItemId itemId) => _ingredients.Any(ingredient => ingredient.ItemId == itemId);

        public static Recipe As(List<Ingredient> items) => new(items);
    }
}