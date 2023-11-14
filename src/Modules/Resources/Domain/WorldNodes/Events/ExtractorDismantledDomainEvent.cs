using SatisfactoryPlanner.BuildingBlocks.Domain;

namespace SatisfactoryPlanner.Modules.Resources.Domain.WorldNodes.Events
{
    public class ExtractorDismantledDomainEvent : DomainEventBase
    {
        public WorldNodeId WorldNodeId { get; }

        public ExtractorDismantledDomainEvent(WorldNodeId worldNodeId)
        {
            WorldNodeId = worldNodeId;
        }
    }
}