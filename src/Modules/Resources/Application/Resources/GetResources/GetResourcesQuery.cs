using SatisfactoryPlanner.Modules.Resources.Application.Configuration.Queries;
using System;
using System.Collections.Generic;

namespace SatisfactoryPlanner.Modules.Resources.Application.Resources.GetResources
{
    public class GetResourcesQuery : QueryBase<List<ResourceDto>>
    {
        public Guid WorldId { get; }

        public GetResourcesQuery(Guid worldId)
        {
            WorldId = worldId;
        }
    }
}