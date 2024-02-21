using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace SatisfactoryPlanner.API.Modules.Resources.WorldNodes
{
    public class UpgradeExtractorRequest
    {
        /// <summary>
        ///     The id of the extractor to upgrade to.
        /// </summary>
        [BindRequired]
        public Guid ExtractorId { get; set; }
    }
}