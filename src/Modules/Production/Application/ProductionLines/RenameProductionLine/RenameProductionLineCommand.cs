using SatisfactoryPlanner.Modules.Production.Application.Contracts;
using System;

namespace SatisfactoryPlanner.Modules.Production.Application.ProductionLines.RenameProductionLine
{
    public class RenameProductionLineCommand(Guid worldId, Guid productionLineId, string name) : CommandBase
    {
        public Guid WorldId { get; } = worldId;

        public Guid ProductionLineId { get; } = productionLineId;

        public string Name { get; } = name;
    }
}