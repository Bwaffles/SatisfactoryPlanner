using SatisfactoryPlanner.BuildingBlocks.Domain;
using System;

namespace SatisfactoryPlanner.Modules.Factories.Domain.ResourceNodes
{
    public class ResourceNodeId : TypedIdValueBase
    {
        public ResourceNodeId(Guid value) : base(value) { }
    }
}
