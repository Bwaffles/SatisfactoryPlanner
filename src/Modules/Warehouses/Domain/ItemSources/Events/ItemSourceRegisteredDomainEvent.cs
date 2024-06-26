using SatisfactoryPlanner.BuildingBlocks.Domain;

namespace SatisfactoryPlanner.Modules.Warehouses.Domain.ItemSources.Events
{
    public class ItemSourceRegisteredDomainEvent(ItemSourceId id, WorldId worldId, Source source) : DomainEventBase
    {
        public ItemSourceId ItemSourceId { get; } = id;
        public WorldId WorldNode { get; } = worldId;
        public Source Source { get; } = source;
    }
}
