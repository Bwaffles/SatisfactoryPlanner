using SatisfactoryPlanner.BuildingBlocks.Domain;

namespace SatisfactoryPlanner.Modules.Resources.Domain.WorldNodes.Events
{
    public class ExtractionRateIncreasedDomainEvent(WorldNodeId worldNodeId, decimal extractionRate) : DomainEventBase
    {
        public WorldNodeId WorldNodeId { get; } = worldNodeId;
        public decimal ExtractionRate { get; } = extractionRate;
    }
}