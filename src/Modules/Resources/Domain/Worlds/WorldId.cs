using SatisfactoryPlanner.BuildingBlocks.Domain;
using System;

namespace SatisfactoryPlanner.Modules.Resources.Domain.Worlds
{
    public record WorldId : TypedIdValueBase
    {
        public WorldId(Guid value) : base(value) { }
    }
}