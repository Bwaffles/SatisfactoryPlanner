using SatisfactoryPlanner.BuildingBlocks.Domain;

namespace SatisfactoryPlanner.Modules.Resources.Domain.WorldNodes.Events
{
    public class ExtractionRateIncreasedDomainEvent(WorldNodeId worldNodeId, ExtractionRate extractionRate) : DomainEventBase
    {
        public ExtractionRate ExtractionRate { get; } = extractionRate;

        public WorldNodeId WorldNodeId { get; } = worldNodeId;
    }
}