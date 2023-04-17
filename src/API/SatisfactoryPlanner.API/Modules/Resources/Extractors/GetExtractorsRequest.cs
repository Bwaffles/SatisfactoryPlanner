using System;

namespace SatisfactoryPlanner.API.Modules.Resources.Extractors
{
    public class GetExtractorsRequest
    {
        /// <summary>
        ///     The id of the resource that the extractors must be capable of tapping, or null to return all extractors.
        /// </summary>
        public Guid? ResourceId { get; set; }
    }
}
