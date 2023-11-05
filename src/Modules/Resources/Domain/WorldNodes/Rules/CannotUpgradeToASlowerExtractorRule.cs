using SatisfactoryPlanner.BuildingBlocks.Domain;
using SatisfactoryPlanner.Modules.Resources.Domain.Extractors;

namespace SatisfactoryPlanner.Modules.Resources.Domain.WorldNodes.Rules
{
    public class CannotUpgradeToASlowerExtractorRule : IBusinessRule
    {
        private readonly Extractor _currentExtractor;
        private readonly Extractor _extractor;

        public CannotUpgradeToASlowerExtractorRule(Extractor extractor, Extractor currentExtractor)
        {
            _extractor = extractor;
            _currentExtractor = currentExtractor;
        }

        public bool IsBroken() => _extractor.GetPotentialResourcesPerMinute() <
                                  _currentExtractor.GetPotentialResourcesPerMinute();

        public string Message => "Cannot upgrade to a slower extractor.";
    }
}