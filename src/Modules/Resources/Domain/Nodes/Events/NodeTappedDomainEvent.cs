using SatisfactoryPlanner.BuildingBlocks.Domain;
using SatisfactoryPlanner.Modules.Resources.Domain.Extractors;
using SatisfactoryPlanner.Modules.Resources.Domain.TappedNodes;
using SatisfactoryPlanner.Modules.Resources.Domain.Worlds;

namespace SatisfactoryPlanner.Modules.Resources.Domain.Nodes.Events
{
    public class NodeTappedDomainEvent : DomainEventBase
    {
        public TappedNodeId TappedNodeId { get; }

        public WorldId WorldId { get; }

        public NodeId NodeId { get; }

        public ExtractorId ExtractorId { get; }

        public NodeTappedDomainEvent(TappedNodeId tappedNodeId, WorldId worldId, NodeId nodeId, ExtractorId extractorId)
        {
            TappedNodeId = tappedNodeId;
            WorldId = worldId;
            NodeId = nodeId;
            ExtractorId = extractorId;
        }
    }
}