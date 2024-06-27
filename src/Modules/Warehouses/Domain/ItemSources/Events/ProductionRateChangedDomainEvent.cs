using SatisfactoryPlanner.BuildingBlocks.Domain;

namespace SatisfactoryPlanner.Modules.Warehouses.Domain.ItemSources.Events
{
    public class ProductionRateChangedDomainEvent(ItemSourceId itemSourceId, string itemId, decimal rate) : DomainEventBase
    {
        public ItemSourceId ItemSourceId { get; } = itemSourceId;

        public string ItemId { get; } = itemId;

        public decimal Rate { get; } = rate;
    }
}
