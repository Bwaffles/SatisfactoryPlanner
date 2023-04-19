using System;

namespace SatisfactoryPlanner.Modules.Resources.Application.Nodes.GetNodeDetails
{
    public class AvailableExtractorDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        /// <summary>
        ///     The maximum amount extractable from the node using this extractor.
        ///     This assumes the extractor is overclocked to 250%.
        /// </summary>
        public decimal MaxAmountExtractable { get; set; }
    }
}
