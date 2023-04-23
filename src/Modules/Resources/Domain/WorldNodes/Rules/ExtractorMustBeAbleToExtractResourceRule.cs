using SatisfactoryPlanner.BuildingBlocks.Domain;
using SatisfactoryPlanner.Modules.Resources.Domain.Extractors;
using SatisfactoryPlanner.Modules.Resources.Domain.Resources;

namespace SatisfactoryPlanner.Modules.Resources.Domain.WorldNodes.Rules
{
    public class ExtractorMustBeAbleToExtractResourceRule : IBusinessRule
    {
        private readonly Extractor _extractor;
        private readonly ResourceId _resourceId;

        public ExtractorMustBeAbleToExtractResourceRule(Extractor extractor, ResourceId resourceId)
        {
            _extractor = extractor;
            _resourceId = resourceId;
        }

        public string Message => "Extractor must be able to extract the resource.";

        public bool IsBroken() => !_extractor.CanExtract(_resourceId);
    }
}
