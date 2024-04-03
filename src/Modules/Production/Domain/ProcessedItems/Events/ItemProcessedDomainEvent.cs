using SatisfactoryPlanner.BuildingBlocks.Domain;
using SatisfactoryPlanner.Modules.Production.Domain.ProductionLines;

namespace SatisfactoryPlanner.Modules.Production.Domain.ProcessedItems.Events
{
    public class ItemProcessedDomainEvent(
        ProcessedItemId processedItemId,
        ProductionLineId productionLineId,
        ItemId itemId,
        Recipe recipe) : DomainEventBase
    {
        public ProductionLineId ProductionLineId { get; } = productionLineId;

        public ProcessedItemId ProcessedItemId { get; } = processedItemId;

        public ItemId ItemId { get; } = itemId;

        public Recipe Recipe { get; } = recipe;
    }
}