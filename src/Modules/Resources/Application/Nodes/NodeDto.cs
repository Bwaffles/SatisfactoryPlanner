using System;

namespace SatisfactoryPlanner.Modules.Resources.Application.Nodes
{
    public class NodeDto
    {
        public Guid Id { get; set; }

        public string Purity { get; set; } = null!;

        public string Biome { get; set; } = null!;

        public decimal MapPositionX { get; set; }

        public decimal MapPositionY { get; set; }

        public decimal MapPositionZ { get; set; }

        public Guid ResourceId { get; set; }

        public string ResourceName { get; set; } = null!;
    }
}
