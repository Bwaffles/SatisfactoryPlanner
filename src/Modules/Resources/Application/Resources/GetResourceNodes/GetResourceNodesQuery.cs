using SatisfactoryPlanner.Modules.Resources.Application.Configuration.Queries;
using System;
using System.Collections.Generic;

namespace SatisfactoryPlanner.Modules.Resources.Application.Resources.GetResourceNodes
{
    public class GetResourceNodesQuery : QueryBase<List<ResourceNodeDto>>
    {
        public GetResourceNodesQuery(Guid resourceId)
        {
            ResourceId = resourceId;
        }

        public Guid ResourceId { get; set; }
    }
}
