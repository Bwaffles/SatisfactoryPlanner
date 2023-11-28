using SatisfactoryPlanner.BuildingBlocks.Domain;
using System.Collections.Generic;

namespace SatisfactoryPlanner.Modules.Factories.Domain.ProcessedItems
{
    public class Recipe : ValueObject
    {
        private readonly List<Item> _ingredients;

        private Recipe(List<Item> ingredients)
        {
            _ingredients = ingredients;
        }

        public bool HasIngredient(Item item) => _ingredients.Contains(item);

        public static Recipe As(List<Item> items) => new(items);
    }
}