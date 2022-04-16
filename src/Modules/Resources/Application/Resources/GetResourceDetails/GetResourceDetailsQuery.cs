using SatisfactoryPlanner.Modules.Resources.Application.Configuration.Queries;
using System;

namespace SatisfactoryPlanner.Modules.Resources.Application.Resources.GetResourceDetails
{
    public class GetResourceDetailsQuery : QueryBase<ResourceDetailsDto>
    {
        public GetResourceDetailsQuery(Guid resourceId)
        {
            ResourceId = resourceId;
        }

        public Guid ResourceId { get; set; }
    }
}
