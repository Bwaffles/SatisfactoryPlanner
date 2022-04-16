using System;

namespace SatisfactoryPlanner.API.Modules.Resources.Extractions
{
    public class ExtractResourceRequest
    {
        /// <summary>
        ///     The id of the resource node to extract.
        /// </summary>
        public Guid ResourceNodeId { get; set; }

        /// <summary>
        ///     The id of the resource extractor being used to extract the resource node.
        /// </summary>
        public Guid ResourceExtractorId { get; set; }

        /// <summary>
        ///     The amount of the resource to be extracted.
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        ///     A name to identify this extraction (e.g. Red Forest Bauxite 1).
        /// </summary>
        public string Name { get; set; }
    }
}