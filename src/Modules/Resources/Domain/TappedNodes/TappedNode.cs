using SatisfactoryPlanner.BuildingBlocks.Domain;
using SatisfactoryPlanner.Modules.Resources.Domain.Extractors;
using SatisfactoryPlanner.Modules.Resources.Domain.Nodes;
using SatisfactoryPlanner.Modules.Resources.Domain.Worlds;
using System;

namespace SatisfactoryPlanner.Modules.Resources.Domain.TappedNodes
{
    public class TappedNode : Entity, IAggregateRoot
    {
        public ResourceNodeExtractionId Id { get; private set; }

        private readonly WorldId _worldId;

        private readonly NodeId _nodeId;

        private readonly ExtractorId _extractorId;

        private readonly decimal _amountToExtract;

        private readonly string _name;

        public static TappedNode CreateNew(WorldId worldId, NodeId nodeId, ExtractorId extractorId, decimal amount,
            string name)
            => new(worldId, nodeId, extractorId, amount, name);

        private TappedNode(WorldId worldId, NodeId nodeId, ExtractorId extractorId, decimal amountToExtract, string name)
        {
            Id = new ResourceNodeExtractionId(Guid.NewGuid());
            _worldId = worldId;
            _nodeId = nodeId;
            _extractorId = extractorId;
            _amountToExtract = amountToExtract;
            _name = name;
        }

        private TappedNode() { /* for EF */ }
    }
}
