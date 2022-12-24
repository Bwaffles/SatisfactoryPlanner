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

        private Node(NodeId id, NodePurity purity, ResourceId resourceId)
        {
            Id = id;
            _purity = purity;
            _resourceId = resourceId;
        }

        public TappedNode Tap(WorldId worldId, Extractor extractor, decimal amountToExtract, string name,
            ITappedNodeExistenceChecker tappedNodeExistenceChecker)
        {
            CheckRule(new NodeCannotAlreadyBeTappedRule(Id, tappedNodeExistenceChecker));
            CheckRule(new ExtractorMustBeAbleToExtractResourceRule(extractor, _resourceId));
            // TODO create domain service for calculating the max potentional resources for the node/extractor combo
            CheckRule(new CannotExtractMoreThanTheAvailableResourcesRule(this, extractor, amountToExtract));

            return TappedNode.CreateNew(worldId, Id, extractor.Id, amountToExtract, name);
        }

        public decimal GetPurityMultiplier() => _purity.GetMultiplier();
    }
}
