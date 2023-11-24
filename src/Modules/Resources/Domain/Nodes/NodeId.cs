using SatisfactoryPlanner.BuildingBlocks.Domain;
using System;

namespace SatisfactoryPlanner.Modules.Resources.Domain.Nodes
{
    public record NodeId : TypedIdValueBase
    {
        public NodeId(Guid value) : base(value) { }
    }
}