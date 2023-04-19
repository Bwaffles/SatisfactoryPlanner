using SatisfactoryPlanner.BuildingBlocks.Domain;
using SatisfactoryPlanner.Modules.Resources.Domain.TappedNodes;
using SatisfactoryPlanner.Modules.Resources.Domain.Worlds;

namespace SatisfactoryPlanner.Modules.Resources.Domain.Nodes.Rules
{
    public class NodeCannotAlreadyBeTappedRule : IBusinessRule
    {
        private readonly NodeId _nodeId;
        private readonly ITappedNodeExistenceChecker _tappedNodeExistenceChecker;
        private readonly WorldId _worldId;

        public NodeCannotAlreadyBeTappedRule(NodeId nodeId, WorldId worldId,
            ITappedNodeExistenceChecker tappedNodeExistenceChecker)
        {
            _nodeId = nodeId;
            _worldId = worldId;
            _tappedNodeExistenceChecker = tappedNodeExistenceChecker;
        }

        public string Message => "Node cannot be tapped more than once.";

        public bool IsBroken() => _tappedNodeExistenceChecker.IsTapped(_worldId.Value, _nodeId.Value);
    }
}