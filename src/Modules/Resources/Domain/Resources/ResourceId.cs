using SatisfactoryPlanner.BuildingBlocks.Domain;
using System;

namespace SatisfactoryPlanner.Modules.Resources.Domain.Resources
{
    public class ResourceId : TypedIdValueBase
    {
        public ResourceId(Guid value) : base(value) { }
    }
}