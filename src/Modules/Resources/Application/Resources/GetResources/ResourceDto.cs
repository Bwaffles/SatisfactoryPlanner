using System;

namespace SatisfactoryPlanner.Modules.Resources.Application.Resources.GetResources
{
    public class ResourceDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        /// <summary>
        ///     The number of resources being extracted per minute.
        /// </summary>
        public decimal ExtractedResources { get; set; }
    }
}
