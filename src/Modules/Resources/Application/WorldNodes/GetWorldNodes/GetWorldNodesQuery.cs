using SatisfactoryPlanner.Modules.Resources.Application.Configuration.Queries;
using System;
using System.Collections.Generic;

namespace SatisfactoryPlanner.Modules.Resources.Application.WorldNodes.GetWorldNodes
{
    public class GetWorldNodesQuery : QueryBase<List<WorldNodeDto>>
    {
        public Guid WorldId { get; }

        public Guid? ResourceId { get; }

        public GetWorldNodesQuery(Guid worldId, Guid? resourceId)
        {
            WorldId = worldId;
            ResourceId = resourceId;
        }
    }
}