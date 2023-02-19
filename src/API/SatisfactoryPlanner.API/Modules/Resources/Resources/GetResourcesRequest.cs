using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;

namespace SatisfactoryPlanner.API.Modules.Resources.Resources
{
    public class GetResourcesRequest
    {
        /// <summary>
        ///     The id of the world where the resources are located. Required
        /// </summary>
        /// <remarks>Making this required because it returns the amount of extracted resources in the world.</remarks>
        [BindRequired]
        public Guid WorldId { get; set; }
    }
}