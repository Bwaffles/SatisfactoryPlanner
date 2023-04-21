using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;

namespace SatisfactoryPlanner.API.Modules.Resources.TappedNodes
{
    public class IncreaseExtractionRateRequest
    {
        /// <summary>
        ///     The id of the world where the node is being tapped.
        /// </summary>
        [BindRequired]
        public Guid WorldId { get; set; }

        [BindRequired]
        public decimal NewExtractionRate { get; set; }
    }
}