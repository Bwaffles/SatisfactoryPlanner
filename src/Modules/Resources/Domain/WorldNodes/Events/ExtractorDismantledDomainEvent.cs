using SatisfactoryPlanner.BuildingBlocks.Domain;

namespace SatisfactoryPlanner.Modules.Resources.Domain.WorldNodes.Events
{
    public class ExtractorDismantledDomainEvent(WorldNodeId worldNodeId) : DomainEventBase
    {
        public WorldNodeId WorldNodeId { get; } = worldNodeId;
    }
}