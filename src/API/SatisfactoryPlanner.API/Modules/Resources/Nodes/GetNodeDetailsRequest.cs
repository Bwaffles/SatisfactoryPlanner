using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;

namespace SatisfactoryPlanner.API.Modules.Resources.Nodes
{
    public class GetNodeDetailsRequest
    {
        [BindRequired]
        public Guid WorldId { get; set; }
    }
}
