using System;

namespace SatisfactoryPlanner.Modules.Resources.Application.Resources.GetResourceDetails
{
    public class ResourceDetailsDto
    {
        public Guid Id { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}
