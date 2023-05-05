using SatisfactoryPlanner.BuildingBlocks.Domain;

namespace SatisfactoryPlanner.Modules.Resources.Domain.WorldNodes.Events
{
    /// <summary>
    ///     An event triggered when the extraction rate of a world node has been decreased
    ///     from its current extraction rate.
    /// </summary>
    public class ExtractionRateDecreasedDomainEvent : DomainEventBase
    {
        public ExtractionRate ExtractionRate { get; }

        public WorldNodeId WorldNodeId { get; }

        public ExtractionRateDecreasedDomainEvent(WorldNodeId worldNodeId, ExtractionRate extractionRate)
        {
            WorldNodeId = worldNodeId;
            ExtractionRate = extractionRate;
        }
    }
}