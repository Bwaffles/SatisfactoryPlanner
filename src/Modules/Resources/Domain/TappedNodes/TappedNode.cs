using SatisfactoryPlanner.BuildingBlocks.Domain;
using SatisfactoryPlanner.Modules.Resources.Domain.Extractors;
using SatisfactoryPlanner.Modules.Resources.Domain.Nodes;
using System;

namespace SatisfactoryPlanner.Modules.Resources.Domain.ResourceNodeExtractions
{
    public class TappedNode : Entity, IAggregateRoot
    {
        public ResourceNodeExtractionId Id { get; private set; }

        private readonly NodeId _nodeId;

        private readonly ExtractorId _extractorId;

        private readonly decimal _amountToExtract;

        private readonly string _name;

        public static TappedNode CreateNew(NodeId nodeId, ExtractorId extractorId, decimal amount, string name)
            => new(nodeId, extractorId, amount, name);

        private TappedNode(NodeId nodeId, ExtractorId extractorId, decimal amountToExtract, string name)
        {
            Id = new ResourceNodeExtractionId(Guid.NewGuid());
            _nodeId = nodeId;
            _extractorId = extractorId;
            _amountToExtract = amountToExtract;
            _name = name;
        }

        private TappedNode() { /* for EF */ }
    }
}
