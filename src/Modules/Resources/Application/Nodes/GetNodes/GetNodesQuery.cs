using SatisfactoryPlanner.Modules.Resources.Application.Configuration.Queries;
using System;
using System.Collections.Generic;

namespace SatisfactoryPlanner.Modules.Resources.Application.Nodes.GetNodes
{
    public class GetNodesQuery : QueryBase<List<NodeDto>>
    {
        public Guid WorldId { get; set; }

        public Guid? ResourceId { get; set; }

        public GetNodesQuery(Guid worldId, Guid? resourceId)
        {
            WorldId = worldId;
            ResourceId = resourceId;
        }
    }
}