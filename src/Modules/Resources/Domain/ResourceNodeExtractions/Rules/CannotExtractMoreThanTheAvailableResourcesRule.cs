using SatisfactoryPlanner.BuildingBlocks.Domain;
using SatisfactoryPlanner.Modules.Resources.Domain.Extractors;
using SatisfactoryPlanner.Modules.Resources.Domain.ResourceNodes;
using System;

namespace SatisfactoryPlanner.Modules.Resources.Domain.ResourceNodeExtractions.Rules
{
    public class CannotExtractMoreThanTheAvailableResourcesRule : IBusinessRule
    {
        private readonly ResourceNode _resourceNode;
        private readonly Extractor _extractor;
        private readonly decimal _amount;

        public CannotExtractMoreThanTheAvailableResourcesRule(ResourceNode resourceNode, Extractor extractor, decimal amount)
        {
            _resourceNode = resourceNode;
            _extractor = extractor;
            _amount = amount;
        }

        public string Message => "Cannot extract more than the available resources.";

        public bool IsBroken()
        {
            var amountExtractable = Math.Min(_extractor.GetPotentialResourcesPerMinute() * _resourceNode.GetPurityMultiplier(), Constants.MaxItemsPerMinute);
            return _amount > amountExtractable;
        }
    }
}
