using SatisfactoryPlanner.BuildingBlocks.Domain;

namespace SatisfactoryPlanner.Modules.Production.Domain.ProcessedItems
{
    public class IngredientOld : ValueObject
    {
        public ItemId ItemId { get; }

        private IngredientOld(ItemId itemId)
        {
            ItemId = itemId;
        }

        public static IngredientOld Of(ItemId itemId) => new(itemId);
    }
}