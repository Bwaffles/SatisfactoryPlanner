using SatisfactoryPlanner.Modules.Factories.Application.Configuration.Queries;
using System;

namespace SatisfactoryPlanner.Modules.Factories.Application.Resources.GetResourceDetails
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
