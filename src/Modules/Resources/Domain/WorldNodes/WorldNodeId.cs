using SatisfactoryPlanner.BuildingBlocks.Domain;
using System;

namespace SatisfactoryPlanner.Modules.Resources.Domain.WorldNodes
{
    public record WorldNodeId : TypedIdValueBase
    {
        public WorldNodeId(Guid value) : base(value) { }
    }
}