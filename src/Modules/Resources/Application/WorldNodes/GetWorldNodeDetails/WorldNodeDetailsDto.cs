using System;
using System.Collections.Generic;

namespace SatisfactoryPlanner.Modules.Resources.Application.WorldNodes.GetWorldNodeDetails
{
    public class WorldNodeDetailsDto
    {
        public Guid NodeId { get; set; }

        public string Purity { get; set; } = null!;

        public string Biome { get; set; } = null!;

        public int Number { get; set; }

        public Guid ResourceId { get; set; }

        public string ResourceName { get; set; } = null!;

        public bool IsTapped { get; set; }

        /// <summary>
        ///     The id of the extractor that was used to tap the node, or null if node is untapped.
        /// </summary>
        public Guid? ExtractorId { get; set; }

        /// <summary>
        ///     The number of resources being extracted per minute.
        /// </summary>
        public decimal ExtractionRate { get; set; }

        /// <summary>
        ///     The extractors that are available to tap this node.
        /// </summary>
        public IEnumerable<AvailableExtractorDto> AvailableExtractors { get; set; }
    }
}