using SatisfactoryPlanner.BuildingBlocks.Domain;
using SatisfactoryPlanner.Modules.Resources.Domain.Extractors;

namespace SatisfactoryPlanner.Modules.Resources.Domain.WorldNodes.Rules
{
    public class CannotAlreadyBeTappedRule : IBusinessRule
    {
        private readonly ExtractorId? _extractorId;

        public CannotAlreadyBeTappedRule(ExtractorId? extractorId)
        {
            _extractorId = extractorId;
        }

        public string Message => "Node cannot be tapped more than once.";

        public bool IsBroken() => _extractorId is not null;
    }
}