using SatisfactoryPlanner.BuildingBlocks.Domain;
using SatisfactoryPlanner.Modules.Resources.Domain.Extractors;
using SatisfactoryPlanner.Modules.Resources.Domain.Nodes;

namespace SatisfactoryPlanner.Modules.Resources.Domain.ResourceNodeExtractions.Rules
{
    public class NodeMustBeExtractableByTheExtractorRule : IBusinessRule
    {
        private readonly Extractor _extractor;
        private readonly Node _node;

        public NodeMustBeExtractableByTheExtractorRule(Node node, Extractor extractor)
        {
            _extractor = extractor;
            _node = node;
        }

        public string Message => "Node must be extractable by the extractor.";

        public bool IsBroken() => !_extractor.CanExtract(_node.GetResourceId());
    }
}
