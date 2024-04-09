using SatisfactoryPlanner.Modules.Production.Application.Contracts;
using SatisfactoryPlanner.Modules.Production.Application.ProductionLines.GetProductionLineDetails;
using System;

namespace SatisfactoryPlanner.Modules.Production.Application.ProductionLines.GetProductionLines
{
    public class GetProductionLineDetailsQuery(Guid worldId, Guid productionLineId) : IQuery<ProductionLineDetailsDto?>
    {
        public Guid WorldId { get; } = worldId;
        public Guid ProductionLineId { get; } = productionLineId;
    }
}