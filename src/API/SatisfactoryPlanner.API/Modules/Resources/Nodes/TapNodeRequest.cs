using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;

namespace SatisfactoryPlanner.API.Modules.Resources.Nodes
{
    public class TapNodeRequest
    {
        /// <summary>
        ///     The id of the extractor being used to extract the resources.
        /// </summary>
        [BindRequired]
        public Guid ExtractorId { get; set; }
    }
}