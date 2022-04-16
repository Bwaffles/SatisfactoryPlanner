using SatisfactoryPlanner.BuildingBlocks.Domain;
using SatisfactoryPlanner.Modules.Resources.Domain.Extractors;
using SatisfactoryPlanner.Modules.Resources.Domain.ResourceNodes;

namespace SatisfactoryPlanner.Modules.Resources.Domain.ResourceNodeExtractions.Rules
{
    public class ResourceNodeMustBeExtractableByTheExtractorRule : IBusinessRule
    {
        private readonly Extractor _extractor;
        private readonly ResourceNode _resourceNode;

        public ResourceNodeMustBeExtractableByTheExtractorRule(ResourceNode resourceNode, Extractor extractor)
        {
            _extractor = extractor;
            _resourceNode = resourceNode;
        }

        public string Message => "Resource node must be extractable by the extractor.";

        public bool IsBroken() => !_extractor.CanExtract(_resourceNode.GetResourceId());
    }
}
