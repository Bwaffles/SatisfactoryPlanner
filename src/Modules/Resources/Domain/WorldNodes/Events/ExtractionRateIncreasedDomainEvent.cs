using SatisfactoryPlanner.BuildingBlocks.Domain;

namespace SatisfactoryPlanner.Modules.Resources.Domain.WorldNodes.Events
{
    public class ExtractionRateIncreasedDomainEvent : DomainEventBase
    {
        public ExtractionRate ExtractionRate { get; }

        public WorldNodeId WorldNodeId { get; }

        public ExtractionRateIncreasedDomainEvent(WorldNodeId worldNodeId, ExtractionRate extractionRate)
        {
            WorldNodeId = worldNodeId;
            ExtractionRate = extractionRate;
        }
    }
}