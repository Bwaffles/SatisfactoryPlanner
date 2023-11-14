using SatisfactoryPlanner.BuildingBlocks.Domain;

namespace SatisfactoryPlanner.Modules.Resources.Domain.WorldNodes.Rules
{
    public class CannotIncreaseExtractionRateBelowCurrentExtractionRateRule : IBusinessRule
    {
        private readonly ExtractionRate _currentExtractionRate;
        private readonly ExtractionRate _newExtractionRate;

        public CannotIncreaseExtractionRateBelowCurrentExtractionRateRule(ExtractionRate newExtractionRate,
            ExtractionRate currentExtractionRate)
        {
            _newExtractionRate = newExtractionRate;
            _currentExtractionRate = currentExtractionRate;
        }

        public bool IsBroken() => _newExtractionRate < _currentExtractionRate;

        public string Message => "Cannot increase the extraction rate to a rate below the current extraction rate.";
    }
}