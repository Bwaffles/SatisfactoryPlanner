using System;

namespace SatisfactoryPlanner.Modules.Resources.Application.WorldNodes.GetWorldNodeDetails
{
    public class AvailableExtractorDto
    {
        /// <summary>
        ///     The id of the extractor.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        ///     The name of the extractor.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     The maximum number of resources that can be extracted per minute.
        ///     This assumes the extractor is overclocked to 250%, up to the max belt/pipe capacity.
        /// </summary>
        public decimal MaxExtractionRate { get; set; }
    }
}