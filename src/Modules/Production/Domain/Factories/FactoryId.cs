using SatisfactoryPlanner.BuildingBlocks.Domain;
using System;

namespace SatisfactoryPlanner.Modules.Production.Domain.Factories
{
    public record FactoryId : TypedIdValueBase
    {
        public FactoryId(Guid value) : base(value) { }
    }
}