using SatisfactoryPlanner.BuildingBlocks.Domain;
using SatisfactoryPlanner.Modules.Resources.Domain.Extractors;

namespace SatisfactoryPlanner.Modules.Resources.Domain.WorldNodes.Rules
{
    public class CannotDowngradeToAFasterExtractorRule : IBusinessRule
    {
        private readonly Extractor _currentExtractor;
        private readonly Extractor _extractor;

        public CannotDowngradeToAFasterExtractorRule(Extractor extractor, Extractor currentExtractor)
        {
            _extractor = extractor;
            _currentExtractor = currentExtractor;
        }

        public bool IsBroken() => _extractor.GetPotentialResourcesPerMinute() >
                                  _currentExtractor.GetPotentialResourcesPerMinute();

        public string Message => "Cannot downgrade to a faster extractor.";
    }
}