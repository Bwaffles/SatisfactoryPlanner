using SatisfactoryPlanner.Modules.Resources.Application.Configuration.Queries;
using System;
using System.Collections.Generic;

namespace SatisfactoryPlanner.Modules.Resources.Application.Resources.GetResourceExtractors
{
    public class GetResourceExtractorsQuery : QueryBase<List<ResourceExtractorDto>>
    {
        public GetResourceExtractorsQuery(Guid resourceId)
        {
            ResourceId = resourceId;
        }

        public Guid ResourceId { get; }
    }
}
