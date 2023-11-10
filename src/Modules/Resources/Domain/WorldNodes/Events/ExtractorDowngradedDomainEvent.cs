using SatisfactoryPlanner.BuildingBlocks.Domain;
using SatisfactoryPlanner.Modules.Resources.Domain.Extractors;

namespace SatisfactoryPlanner.Modules.Resources.Domain.WorldNodes.Events
{
    /// <summary>
    ///     An event triggered when the extractor of a world node has been downgraded to a slower extractor.
    /// </summary>
    public class ExtractorDowngradedDomainEvent : DomainEventBase
    {
        public WorldNodeId WorldNodeId { get; }

        public ExtractorId ExtractorId { get; }

        public ExtractorDowngradedDomainEvent(WorldNodeId worldNodeId, ExtractorId extractorId)
        {
            WorldNodeId = worldNodeId;
            ExtractorId = extractorId;
        }
    }
}