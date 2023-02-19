using System;

namespace SatisfactoryPlanner.Modules.Resources.Application.Nodes
{
    public class NodeDto
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
        public decimal AmountToExtract { get; set; }
        
        /// <summary>
        ///     The total number of resources per minute available to extract from this node.
        /// </summary>
        public decimal TotalResources { get; set; }

        public Guid ResourceId { get; set; }

        public string ResourceName { get; set; } = null!;
    }
}
