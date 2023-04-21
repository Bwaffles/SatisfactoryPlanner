using SatisfactoryPlanner.BuildingBlocks.Domain;

namespace SatisfactoryPlanner.Modules.Resources.Domain.TappedNodes.Events
{
    public class ExtractionRateIncreasedDomainEvent : DomainEventBase
    {
        public ExtractionRate ExtractionRate { get; }

        public TappedNodeId TappedNodeId { get; }

        public ExtractionRateIncreasedDomainEvent(TappedNodeId tappedNodeId, ExtractionRate extractionRate)
        {
            TappedNodeId = tappedNodeId;
            ExtractionRate = extractionRate;
        }
    }
}