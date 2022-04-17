using SatisfactoryPlanner.Modules.Resources.Application.Configuration.Queries;
using System;
using System.Collections.Generic;

namespace SatisfactoryPlanner.Modules.Resources.Application.Nodes.GetNodes
{
    public class GetNodesQuery : QueryBase<List<NodeDto>>
    {
        public GetNodesQuery(Guid? resourceId)
        {
            ResourceId = resourceId;
        }

        public Guid? ResourceId { get; set; }
    }
}
