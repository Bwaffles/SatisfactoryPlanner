using SatisfactoryPlanner.BuildingBlocks.Domain;
using SatisfactoryPlanner.Modules.Resources.Domain.Extractors;

namespace SatisfactoryPlanner.Modules.Resources.Domain.TappedNodes.Rules
{
    public class NodeCannotAlreadyBeTappedRule : IBusinessRule
    {
        private readonly ExtractorId? _extractorId;

        public NodeCannotAlreadyBeTappedRule(ExtractorId? extractorId)
        {
            _extractorId = extractorId;
        }

        public string Message => "Node cannot be tapped more than once.";

        public bool IsBroken() => _extractorId != null;
    }
}