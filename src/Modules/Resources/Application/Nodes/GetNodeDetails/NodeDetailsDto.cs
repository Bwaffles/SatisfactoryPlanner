using System;
using System.Collections.Generic;

namespace SatisfactoryPlanner.Modules.Resources.Application.Nodes.GetNodeDetails
{
    public class NodeDetailsDto
    {
        public Guid Id { get; set; }

        public string Purity { get; set; } = null!;

        public string Biome { get; set; } = null!;

        public int Number { get; set; }

        public bool IsTapped { get; set; }

        /// <summary>
        ///     The number of resources being extracted per minute.
        /// </summary>
        public decimal ExtractionRate { get; set; }

        public Guid ResourceId { get; set; }

        public string ResourceName { get; set; } = null!;

        /// <summary>
        ///     The extractors that are available to tap this node.
        /// </summary>
        public IEnumerable<AvailableExtractorDto> AvailableExtractors { get; set; }
    }
}