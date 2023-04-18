using SatisfactoryPlanner.BuildingBlocks.Domain;
using SatisfactoryPlanner.Modules.Resources.Domain.Extractors;
using SatisfactoryPlanner.Modules.Resources.Domain.Nodes.Rules;
using SatisfactoryPlanner.Modules.Resources.Domain.Resources;
using SatisfactoryPlanner.Modules.Resources.Domain.TappedNodes;
using SatisfactoryPlanner.Modules.Resources.Domain.Worlds;

namespace SatisfactoryPlanner.Modules.Resources.Domain.Nodes
{
    public class Node : Entity, IAggregateRoot
    {
        public NodeId Id { get; }

        private readonly NodePurity _purity;

        private readonly ResourceId _resourceId;

        public static Node CreateNew(NodeId id, NodePurity purity, ResourceId resourceId)
        {
            return new(id, purity, resourceId);
        }

        private Node() { }

        private Node(NodeId id, NodePurity purity, ResourceId resourceId)
        {
            Id = id;
            _purity = purity;
            _resourceId = resourceId;
        }

        public TappedNode Tap(WorldId worldId, Extractor extractor, ITappedNodeExistenceChecker tappedNodeExistenceChecker)
        {
            CheckRule(new NodeCannotAlreadyBeTappedRule(Id, tappedNodeExistenceChecker));
            CheckRule(new ExtractorMustBeAbleToExtractResourceRule(extractor, _resourceId));

            return TappedNode.CreateNew(worldId, Id, extractor.Id);
        }

        public decimal GetPurityMultiplier() => _purity.GetMultiplier();
    }
}
