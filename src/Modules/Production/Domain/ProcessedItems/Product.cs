using SatisfactoryPlanner.BuildingBlocks.Domain;

namespace SatisfactoryPlanner.Modules.Production.Domain.ProcessedItems
{
    public class Product: ValueObject
    {
        public decimal Amount { get; }

        public Item Item { get; }

        private Product(decimal amount, Item item)
        {
            Item = item;
            Amount = amount;
        }

        public static Product As(decimal amount, Item item) => new(amount, item);
    }
}