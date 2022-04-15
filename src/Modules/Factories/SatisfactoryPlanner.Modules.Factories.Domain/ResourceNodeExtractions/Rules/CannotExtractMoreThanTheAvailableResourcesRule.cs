using SatisfactoryPlanner.BuildingBlocks.Domain;
using SatisfactoryPlanner.Modules.Factories.Domain.ResourceExtractors;
using SatisfactoryPlanner.Modules.Factories.Domain.ResourceNodes;
using System;

namespace SatisfactoryPlanner.Modules.Factories.Domain.ResourceNodeExtractions.Rules
{
    internal class CannotExtractMoreThanTheAvailableResourcesRule : IBusinessRule
    {
        private readonly ResourceNode _resourceNode;
        private readonly ResourceExtractor _resourceExtractor;
        private readonly decimal _amount;

        public CannotExtractMoreThanTheAvailableResourcesRule(ResourceNode resourceNode, ResourceExtractor resourceExtractor, decimal amount)
        {
            _resourceNode = resourceNode;
            _resourceExtractor = resourceExtractor;
            _amount = amount;
        }

        public string Message => "Cannot extract more than the available resources.";

        public bool IsBroken()
        {
            var amountExtractable = Math.Min(_resourceExtractor.GetPotentialItemsPerMinute() * _resourceNode.GetPurityMultiplier(), Constants.MaxItemsPerMinute);
            return _amount > amountExtractable;
        }
    }
}
