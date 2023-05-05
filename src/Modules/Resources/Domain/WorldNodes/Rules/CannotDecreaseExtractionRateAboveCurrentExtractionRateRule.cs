using SatisfactoryPlanner.BuildingBlocks.Domain;

namespace SatisfactoryPlanner.Modules.Resources.Domain.WorldNodes.Rules
{
    public class CannotDecreaseExtractionRateAboveCurrentExtractionRateRule : IBusinessRule
    {
        private readonly ExtractionRate _newExtractionRate;
        private readonly ExtractionRate _currentExtractionRate;

        public CannotDecreaseExtractionRateAboveCurrentExtractionRateRule(ExtractionRate newExtractionRate, ExtractionRate currentExtractionRate)
        {
            _newExtractionRate = newExtractionRate;
            _currentExtractionRate = currentExtractionRate;
        }

        public bool IsBroken() => _newExtractionRate > _currentExtractionRate;

        public string Message => "Cannot decrease the extraction rate to a rate above the current extraction rate.";
    }
}