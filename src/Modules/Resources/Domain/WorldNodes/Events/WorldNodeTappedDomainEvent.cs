using SatisfactoryPlanner.BuildingBlocks.Domain;
using SatisfactoryPlanner.Modules.Resources.Domain.Extractors;

namespace SatisfactoryPlanner.Modules.Resources.Domain.WorldNodes.Events
{
    public class WorldNodeTappedDomainEvent(WorldNodeId worldNodeId, ExtractorId extractorId) : DomainEventBase
    {
        public WorldNodeId WorldNodeId { get; } = worldNodeId;

        public ExtractorId ExtractorId { get; } = extractorId;
    }
}