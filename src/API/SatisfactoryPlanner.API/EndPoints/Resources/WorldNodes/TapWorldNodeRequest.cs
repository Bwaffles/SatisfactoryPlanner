using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace SatisfactoryPlanner.API.Modules.Resources.WorldNodes
{
    public class TapWorldNodeRequest
    {
        /// <summary>
        ///     The id of the extractor being used to extract the resources.
        /// </summary>
        [BindRequired]
        public Guid ExtractorId { get; set; }
    }
}