using SatisfactoryPlanner.BuildingBlocks.Domain;
using System;

namespace SatisfactoryPlanner.Modules.Factories.Domain.Factories
{
    public record FactoryId : TypedIdValueBase
    {
        public FactoryId(Guid value) : base(value) { }
    }
}