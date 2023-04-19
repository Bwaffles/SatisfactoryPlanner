using SatisfactoryPlanner.BuildingBlocks.Domain;
using SatisfactoryPlanner.Modules.Resources.Domain.Extractors;
using SatisfactoryPlanner.Modules.Resources.Domain.Nodes;
using SatisfactoryPlanner.Modules.Resources.Domain.Nodes.Events;
using SatisfactoryPlanner.Modules.Resources.Domain.Worlds;
using System;

namespace SatisfactoryPlanner.Modules.Resources.Domain.TappedNodes
{
    public class TappedNode : Entity, IAggregateRoot
    {
        private readonly decimal _amountToExtract;

        private readonly ExtractorId _extractorId;

        private readonly string _name;

        private readonly NodeId _nodeId;

        private readonly WorldId _worldId;

        public TappedNodeId Id { get; }

        private TappedNode(WorldId worldId, NodeId nodeId, ExtractorId extractorId)
        {
            Id = new TappedNodeId(Guid.NewGuid());
            _worldId = worldId;
            _nodeId = nodeId;
            _extractorId = extractorId;
            _amountToExtract = 0;
            _name = ""; // TODO can probably remove this since I've pre-created the names for the nodes already

            AddDomainEvent(new NodeTappedDomainEvent(Id, _worldId, _nodeId, _extractorId));
        }

        // ReSharper disable once UnusedMember.Local
        private TappedNode()
        { /* for EF */
        }

        internal static TappedNode CreateNew(WorldId worldId, NodeId nodeId, ExtractorId extractorId)
            => new(worldId, nodeId, extractorId);
    }
}