using SatisfactoryPlanner.BuildingBlocks.Domain;
using SatisfactoryPlanner.Modules.Factories.Domain.ResourceExtractors;
using SatisfactoryPlanner.Modules.Factories.Domain.ResourceNodes;

namespace SatisfactoryPlanner.Modules.Factories.Domain.ResourceNodeExtractions.Rules
{
    internal class ResourceNodeMustBeExtractableByTheExtractorRule : IBusinessRule
    {
        private readonly ResourceExtractor resourceExtractor;
        private readonly ResourceNode resourceNode;

        public ResourceNodeMustBeExtractableByTheExtractorRule(ResourceNode resourceNode, ResourceExtractor resourceExtractor)
        {
            this.resourceExtractor = resourceExtractor;
            this.resourceNode = resourceNode;
        }

        public string Message => "Resource node must be extractable by the extractor.";

        public bool IsBroken() => !resourceExtractor.CanExtract(resourceNode.GetResourceId());
    }
}
