using System;

namespace SatisfactoryPlanner.API.Modules.Resources.TappedNodes
{
    public class TapNodeRequest
    {
        /// <summary>
        ///     The id of the world where the node is being tapped.
        /// </summary>
        public Guid WorldId { get; set; }

        /// <summary>
        ///     The id of the node to tap.
        /// </summary>
        public Guid NodeId { get; set; }

        /// <summary>
        ///     The id of the extractor being used to extract the resources.
        /// </summary>
        public Guid ExtractorId { get; set; }

        /// <summary>
        ///     The amount of the resource to be extracted.
        /// </summary>
        public decimal AmountToExtract { get; set; }

        /// <summary>
        ///     A name to identify this (e.g. Red Forest Bauxite 1).
        /// </summary>
        public string Name { get; set; }
    }
}