using SatisfactoryPlanner.Modules.Resources.Application.Configuration.Queries;
using System;
using System.Collections.Generic;

namespace SatisfactoryPlanner.Modules.Resources.Application.WorldNodes.GetWorldNodes
{
    public class GetWorldNodesQuery : QueryBase<List<WorldNodeDto>>
    {
        public Guid WorldId { get; set; }

        public Guid? ResourceId { get; set; }

        public GetWorldNodesQuery(Guid worldId, Guid? resourceId)
        {
            WorldId = worldId;
            ResourceId = resourceId;
        }
    }
}