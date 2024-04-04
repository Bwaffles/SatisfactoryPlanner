using SatisfactoryPlanner.Modules.Production.Application.Contracts;
using System;
using System.Collections.Generic;

namespace SatisfactoryPlanner.Modules.Production.Application.ProductionLines.GetProductionLines
{
    public class GetProductionLinesQuery(Guid worldId) : IQuery<List<ProductionLineDto>>
    {
        public Guid WorldId { get; } = worldId;
    }
}