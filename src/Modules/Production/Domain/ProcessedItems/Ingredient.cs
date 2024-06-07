using SatisfactoryPlanner.BuildingBlocks.Domain;
using SatisfactoryPlanner.Modules.GameData.GameData;

namespace SatisfactoryPlanner.Modules.Production.Domain.ProcessedItems
{
    public class Ingredient : ValueObject
    {
        public decimal Amount { get; }

        public Item Item { get; }

        private Ingredient(decimal amount, Item item)
        {
            Amount = amount;
            Item = item;
        }

        public static Ingredient As(decimal amount, Item item) => new(amount, item);
    }
}