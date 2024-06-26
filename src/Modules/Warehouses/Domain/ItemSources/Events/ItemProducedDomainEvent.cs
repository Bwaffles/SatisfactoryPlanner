using SatisfactoryPlanner.BuildingBlocks.Domain;

namespace SatisfactoryPlanner.Modules.Warehouses.Domain.ItemSources.Events
{
    public class ItemProducedDomainEvent(ItemSourceId itemSourceId, string itemId, Rate rate) : DomainEventBase
    {
        public ItemSourceId ItemSourceId { get; } = itemSourceId;

        public string ItemId { get; } = itemId;

        public Rate Rate { get; } = rate;
    }
}
