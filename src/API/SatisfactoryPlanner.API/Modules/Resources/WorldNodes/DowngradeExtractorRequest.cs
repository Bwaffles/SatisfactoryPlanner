using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;

namespace SatisfactoryPlanner.API.Modules.Resources.WorldNodes
{
    public class DowngradeExtractorRequest
    {
        /// <summary>
        ///     The id of the extractor to downgrade to.
        /// </summary>
        [BindRequired]
        public Guid ExtractorId { get; set; }
    }
}