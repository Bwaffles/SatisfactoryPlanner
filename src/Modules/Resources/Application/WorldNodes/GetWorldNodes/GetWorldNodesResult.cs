using System;
using System.Collections.Generic;

namespace SatisfactoryPlanner.Modules.Resources.Application.WorldNodes.GetWorldNodes
{
    public class GetWorldNodesResult
    {
        public required List<WorldNodeDto> WorldNodes { get; set; }

        public class WorldNodeDto
        {
            public Guid Id { get; set; }

            public string Purity { get; set; } = null!;

            public string Biome { get; set; } = null!;

            public int Number { get; set; }

            public decimal MapPositionX { get; set; }

            public decimal MapPositionY { get; set; }

            public decimal MapPositionZ { get; set; }

            public bool IsTapped { get; set; }

            /// <summary>
            ///     The number of resources being extracted per minute.
            /// </summary>
            public decimal ExtractionRate { get; set; }

            /// <summary>
            ///     The maximum number of resources that can be extracted per minute.
            ///     This assumes the extractor is overclocked to 250%, up to the max belt/pipe capacity.
            /// </summary>
            public decimal MaxExtractionRate { get; set; }

            public Guid ResourceId { get; set; }

            public string ResourceName { get; set; } = null!;
        }
    }
}