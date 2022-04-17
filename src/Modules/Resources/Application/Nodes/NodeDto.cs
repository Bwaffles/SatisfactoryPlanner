using System;

namespace SatisfactoryPlanner.Modules.Resources.Application.Nodes
{
    public class NodeDto
    {
        public Guid Id { get; set; }

        public Guid ResourceId { get; set; }

        public string ResourceName { get; set; }

        public string Purity { get; set; }

        public string Biome { get; set; }

        public decimal MapPositionX { get; set; }

        public decimal MapPositionY { get; set; }

        public decimal MapPositionZ { get; set; }
    }
}
