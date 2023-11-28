using SatisfactoryPlanner.BuildingBlocks.Domain;
using SatisfactoryPlanner.Modules.Factories.Domain.ProductionLines;

namespace SatisfactoryPlanner.Modules.Factories.Domain.ProcessedItems.Events
{
    public class ItemProcessedDomainEvent(ProcessedItemId processedItemId, ProductionLineId productionLineId, Item item,
        Recipe recipe) : DomainEventBase
    {
        public ProductionLineId ProductionLineId { get; } = productionLineId;

        public ProcessedItemId ProcessedItemId { get; } = processedItemId;

        public Item Item { get; } = item;

        public Recipe Recipe { get; } = recipe;
    }
}