using System;
using System.Collections.Generic;

namespace SatisfactoryPlanner.Modules.Resources.Application.WorldNodes.GetWorldNodeDetails
{
    public class WorldNodeDetailsResult
    {
        public required WorldNodeDetails Details { get; set; }

        public class WorldNodeDetails
        {
            public Guid NodeId { get; set; }

            public required string NodeName { get; set; }

            public required string Purity { get; set; }

            public required string Biome { get; set; }

            public int Number { get; set; }

            public Guid ResourceId { get; set; }

            public required string ResourceName { get; set; }

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
            public IEnumerable<AvailableExtractor> AvailableExtractors { get; set; } = [];
        }

        public class AvailableExtractor
        {
            /// <summary>
            ///     The id of the extractor.
            /// </summary>
            public Guid Id { get; set; }

            /// <summary>
            ///     The name of the extractor.
            /// </summary>
            public required string Name { get; set; }

            /// <summary>
            ///     The maximum number of resources that can be extracted per minute.
            ///     This assumes the extractor is overclocked to 250%, up to the max belt/pipe capacity.
            /// </summary>
            public decimal MaxExtractionRate { get; set; }
        }
    }

}