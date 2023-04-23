using SatisfactoryPlanner.BuildingBlocks.Domain;
using SatisfactoryPlanner.Modules.Resources.Domain.Extractors;
using SatisfactoryPlanner.Modules.Resources.Domain.Nodes;

namespace SatisfactoryPlanner.Modules.Resources.Domain.WorldNodes.Rules
{
    public class CannotIncreaseExtractionRateAboveTheMaxExtractionRateRule : IBusinessRule
    {
        private readonly ExtractionRate _extractionRate;
        private readonly IExtractionRateCalculator _extractionRateCalculator;
        private readonly ExtractorId _extractorId;
        private readonly NodeId _nodeId;

        public CannotIncreaseExtractionRateAboveTheMaxExtractionRateRule(ExtractionRate extractionRate,
            NodeId nodeId, ExtractorId extractorId, IExtractionRateCalculator extractionRateCalculator)
        {
            _extractionRate = extractionRate;
            _nodeId = nodeId;
            _extractorId = extractorId;
            _extractionRateCalculator = extractionRateCalculator;
        }

        public string Message => "Cannot increase the extraction rate above the max extraction rate.";

        public bool IsBroken()
        {
            var maxExtractionRate = _extractionRateCalculator.GetMaxExtractionRate(_nodeId, _extractorId);
            return _extractionRate > maxExtractionRate;
        }
    }
}