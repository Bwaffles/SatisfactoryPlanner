using System;

namespace SatisfactoryPlanner.Modules.Resources.Application.Nodes.GetNodeDetails
{
    public class AvailableExtractorDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        /// <summary>
        ///     The maximum number of resources that can be extracted per minute.
        ///     This assumes the extractor is overclocked to 250%, up to the max belt/pipe capacity.
        /// </summary>
        public decimal MaxExtractionRate { get; set; }
    }
}
