using SatisfactoryPlanner.BuildingBlocks.Domain;
using SatisfactoryPlanner.Modules.Resources.Domain.Extractors;
using SatisfactoryPlanner.Modules.Resources.Domain.Nodes;
using SatisfactoryPlanner.Modules.Resources.Domain.Worlds;

namespace SatisfactoryPlanner.Modules.Resources.Domain.WorldNodes.Events
{
    public class WorldNodeTappedDomainEvent : DomainEventBase
    {
        public WorldNodeId WorldNodeId { get; }

        public WorldId WorldId { get; }

        public NodeId NodeId { get; }

        public ExtractorId ExtractorId { get; }

        public WorldNodeTappedDomainEvent(WorldNodeId worldNodeId, WorldId worldId, NodeId nodeId,
            ExtractorId extractorId)
        {
            WorldNodeId = worldNodeId;
            WorldId = worldId;
            NodeId = nodeId;
            ExtractorId = extractorId;
        }
    }
}