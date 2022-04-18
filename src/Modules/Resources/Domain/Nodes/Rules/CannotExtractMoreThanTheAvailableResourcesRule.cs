using SatisfactoryPlanner.BuildingBlocks.Domain;
using SatisfactoryPlanner.Modules.Resources.Domain.Extractors;
using SatisfactoryPlanner.Modules.Resources.Domain.Nodes;
using System;

namespace SatisfactoryPlanner.Modules.Resources.Domain.ResourceNodeExtractions.Rules
{
    public class CannotExtractMoreThanTheAvailableResourcesRule : IBusinessRule
    {
        private readonly Node _node;
        private readonly Extractor _extractor;
        private readonly decimal _amount;

        public CannotExtractMoreThanTheAvailableResourcesRule(Node node, Extractor extractor, decimal amount)
        {
            _node = node;
            _extractor = extractor;
            _amount = amount;
        }

        public string Message => "Cannot extract more than the available resources.";

        public bool IsBroken()
        { // TODO liquids have a potential resources per minute * 1000 and they're theoretical max is 600, not 780
            var amountExtractable = Math.Min(_extractor.GetPotentialResourcesPerMinute() * _node.GetPurityMultiplier(), Constants.MaxItemsPerMinute);
            return _amount > amountExtractable;
        }
    }
}
