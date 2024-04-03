using SatisfactoryPlanner.BuildingBlocks.Domain;
using System;

namespace SatisfactoryPlanner.Modules.Production.Domain.ProductionLines
{
    public record ProductionLineId : TypedIdValueBase
    {
        public ProductionLineId(Guid value) : base(value) { }
    }
}