using SatisfactoryPlanner.Modules.Resources.Domain.Extractors;
using SatisfactoryPlanner.Modules.Resources.Domain.Nodes;
using SatisfactoryPlanner.Modules.Resources.Domain.TappedNodes;

namespace SatisfactoryPlanner.Modules.Resources.Domain
{
    public interface IExtractionRateCalculator
    {
        /// <summary>
        ///     Get the maximum extraction rate of the resources from the node using the given extractor.
        ///     This assumes the extractor is overclocked to 250%, up to the max belt/pipe capacity.
        /// </summary>
        ExtractionRate GetMaxExtractionRate(NodeId nodeId, ExtractorId extractorId);
    }
}