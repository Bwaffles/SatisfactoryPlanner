using SatisfactoryPlanner.BuildingBlocks.Domain;

namespace SatisfactoryPlanner.Modules.Factories.Domain.ProcessedItems
{
    public class Ingredient : ValueObject
    {
        public ItemId ItemId { get; }

        private Ingredient(ItemId itemId)
        {
            ItemId = itemId;
        }

        public static Ingredient Of(ItemId itemId) => new(itemId);
    }
}