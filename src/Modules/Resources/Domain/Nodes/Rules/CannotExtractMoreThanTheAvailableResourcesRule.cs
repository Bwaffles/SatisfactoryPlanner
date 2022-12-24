using SatisfactoryPlanner.BuildingBlocks.Domain;
using SatisfactoryPlanner.Modules.Resources.Domain.Extractors;

namespace SatisfactoryPlanner.Modules.Resources.Domain.Nodes.Rules
{
    public class CannotExtractMoreThanTheAvailableResourcesRule : IBusinessRule
    {
        private readonly decimal _amount;
        private readonly Extractor _extractor;
        private readonly Node _node;

        public CannotExtractMoreThanTheAvailableResourcesRule(Node node, Extractor extractor, decimal amount)
        {
            _node = node;
            _extractor = extractor;
            _amount = amount;
        }

        public string Message => "Cannot extract more than the available resources.";

        public bool IsBroken()
        {
            var amountExtractable = ResourceExtractionCalculator.GetMaxAmountExtractable(_extractor, _node);
            return _amount > amountExtractable;
        }
    }
}