using System;

namespace SatisfactoryPlanner.Modules.Factories.Application.Resources.GetResourceNodes
{
    public class ResourceNodeDto
    {
        public Guid Id { get; set; }

        public string ItemCode { get; set; }

        public string ItemName { get; set; }

        public string Purity { get; set; }

        public string Biome { get; set; }

        public decimal MapPositionX { get; set; }

        public decimal MapPositionY { get; set; }

        public decimal MapPositionZ { get; set; }
    }
}
