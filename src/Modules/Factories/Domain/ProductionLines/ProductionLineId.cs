using SatisfactoryPlanner.BuildingBlocks.Domain;
using System;

namespace SatisfactoryPlanner.Modules.Factories.Domain.ProductionLines
{
    public record ProductionLineId : TypedIdValueBase
    {
        public ProductionLineId(Guid value) : base(value) { }
    }
}