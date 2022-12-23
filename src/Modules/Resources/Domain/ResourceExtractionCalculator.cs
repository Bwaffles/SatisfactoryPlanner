using SatisfactoryPlanner.Modules.Resources.Domain.Extractors;
using SatisfactoryPlanner.Modules.Resources.Domain.Nodes;
using System;

namespace SatisfactoryPlanner.Modules.Resources.Domain
{
    public class ResourceExtractionCalculator
    {
        public static decimal GetAmountExtractable(Extractor extractor, Node node)
        { // TODO liquids have a potential resources per minute * 1000 and they're theoretical max is 600, not 780
            // get resource form which has the max items per minute for that form
            // get the multiplier for the purity of the node
            // get the max potential of the extractor (mk1, 2, 3 can extract different amounts)
            return Math.Min(extractor.GetPotentialResourcesPerMinute() * node.GetPurityMultiplier(),
                Constants.MaxItemsPerMinute);
        }
    }
}