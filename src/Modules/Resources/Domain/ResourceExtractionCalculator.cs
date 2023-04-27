using SatisfactoryPlanner.Modules.Resources.Domain.Extractors;
using SatisfactoryPlanner.Modules.Resources.Domain.Nodes;
using System;

namespace SatisfactoryPlanner.Modules.Resources.Domain
{
    public class ResourceExtractionCalculator
    {
        /// <summary>
        ///     Get the maximum amount extractable from the <paramref name="node" /> using the <paramref name="extractor" />.
        ///     This assumes the extractor is overclocked to 250%.
        /// </summary>
        public static decimal GetMaxExtractionRate(Extractor extractor, Node node)
        { // TODO liquids have a potential resources per minute * 1000 and their theoretical max is 600, not 780
            // TODO need resource well extractors

            var maxItemsPerMinute = Constants.MaxItemsPerMinute;

            var potentialResourcesPerMinute = extractor.GetPotentialResourcesPerMinute();
            if (potentialResourcesPerMinute > 1200)
            {
                potentialResourcesPerMinute /= 1000;
                maxItemsPerMinute = 600;
            }

            return Math.Min(potentialResourcesPerMinute * node.GetPurityMultiplier(), maxItemsPerMinute);
        }
    }
}