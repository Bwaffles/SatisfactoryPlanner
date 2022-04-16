using SatisfactoryPlanner.BuildingBlocks.Domain;
using SatisfactoryPlanner.Modules.Resources.Domain.ResourceExtractors;
using SatisfactoryPlanner.Modules.Resources.Domain.ResourceNodes;

namespace SatisfactoryPlanner.Modules.Resources.Domain.ResourceNodeExtractions.Rules
{
    public class ResourceNodeMustBeExtractableByTheExtractorRule : IBusinessRule
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
