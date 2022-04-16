using SatisfactoryPlanner.Modules.Resources.Application.Configuration.Queries;
using System;
using System.Collections.Generic;

namespace SatisfactoryPlanner.Modules.Resources.Application.Extractors.GetExtractors
{
    public class GetExtractorsQuery : QueryBase<List<ExtractorDto>>
    {
        public GetExtractorsQuery(Guid? resourceId)
        {
            ResourceId = resourceId;
        }

        public Guid? ResourceId { get; }
    }
}
