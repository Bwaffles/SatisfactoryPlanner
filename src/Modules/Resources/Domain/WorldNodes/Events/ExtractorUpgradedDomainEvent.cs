using SatisfactoryPlanner.BuildingBlocks.Domain;
using SatisfactoryPlanner.Modules.Resources.Domain.Extractors;

namespace SatisfactoryPlanner.Modules.Resources.Domain.WorldNodes.Events
{
    /// <summary>
    ///     An event triggered when the extractor of a world node has been upgraded to a faster extractor.
    /// </summary>
    public class ExtractorUpgradedDomainEvent : DomainEventBase
    {
        public WorldNodeId WorldNodeId { get; }

        public ExtractorId ExtractorId { get; }

        public ExtractorUpgradedDomainEvent(WorldNodeId worldNodeId, ExtractorId extractorId)
        {
            WorldNodeId = worldNodeId;
            ExtractorId = extractorId;
        }
    }
}