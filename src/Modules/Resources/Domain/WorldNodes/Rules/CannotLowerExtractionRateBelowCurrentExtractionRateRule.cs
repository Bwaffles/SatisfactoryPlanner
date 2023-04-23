using SatisfactoryPlanner.BuildingBlocks.Domain;

namespace SatisfactoryPlanner.Modules.Resources.Domain.WorldNodes.Rules
{
    public class CannotLowerExtractionRateBelowCurrentExtractionRateRule : IBusinessRule
    {
        private readonly ExtractionRate _newExtractionRate;
        private readonly ExtractionRate _currentExtractionRate;

        public CannotLowerExtractionRateBelowCurrentExtractionRateRule(ExtractionRate newExtractionRate, ExtractionRate currentExtractionRate)
        {
            _newExtractionRate = newExtractionRate;
            _currentExtractionRate = currentExtractionRate;
        }

        public bool IsBroken() => _newExtractionRate < _currentExtractionRate;

        public string Message => "Cannot lower the extraction rate below the current extraction rate.";
    }
}