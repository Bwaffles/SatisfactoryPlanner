using SatisfactoryPlanner.BuildingBlocks.Domain;
using System;

namespace SatisfactoryPlanner.Modules.Resources.Domain.ResourceExtractors
{
    public class ResourceExtractorAllowedResourceId : TypedIdValueBase
    {
        public ResourceExtractorAllowedResourceId(Guid value) : base(value)
        {
        }
    }
}
