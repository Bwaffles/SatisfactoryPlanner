using System;

namespace SatisfactoryPlanner.Modules.Resources.Application.Resources.GetResourceDetails
{
    public class ResourceDetailsDto
    {
        public Guid Id { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string ResourceForm { get; set; }

        public int? StackSize { get; set; }

        public bool CanBeDeleted { get; set; }

        public long ResourceSinkPoints { get; set; }

        public decimal? EnergyValue { get; set; }

        public decimal? RadioactiveDecay { get; set; }
    }
}
