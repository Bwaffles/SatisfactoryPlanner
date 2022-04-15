using SatisfactoryPlanner.BuildingBlocks.Domain;
using System;

namespace SatisfactoryPlanner.Modules.Factories.Domain.Factories
{
    public class FactoryId : TypedIdValueBase
    {
        public FactoryId(Guid value) : base(value) { }
    }
}
