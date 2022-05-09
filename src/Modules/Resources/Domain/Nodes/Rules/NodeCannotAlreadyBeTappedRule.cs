using SatisfactoryPlanner.BuildingBlocks.Domain;
using SatisfactoryPlanner.Modules.Resources.Domain.TappedNodes;

namespace SatisfactoryPlanner.Modules.Resources.Domain.Nodes.Rules
{
    public class NodeCannotAlreadyBeTappedRule : IBusinessRule
    {
        private readonly NodeId _nodeId;
        private readonly ITappedNodeExistenceChecker _tappedNodeExistenceChecker;

        public NodeCannotAlreadyBeTappedRule(NodeId nodeId, ITappedNodeExistenceChecker tappedNodeExistenceChecker)
        {
            _nodeId = nodeId;
            _tappedNodeExistenceChecker = tappedNodeExistenceChecker;
        }

        public string Message => "Node cannot be tapped more than once.";

        public bool IsBroken() => _tappedNodeExistenceChecker.IsTapped(_nodeId.Value);
    }
}
