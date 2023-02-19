using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;

namespace SatisfactoryPlanner.API.Modules.Resources.Nodes
{
    public class GetNodesRequest
    {
        public Guid? ResourceId { get; set; }

        [BindRequired]
        public Guid WorldId { get; set; }
    }
}