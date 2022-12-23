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

        /// <summary>
        ///     The total number of resources per minute available to extract on the map.
        /// </summary>
        public decimal TotalResources { get; set; }
    }
}
