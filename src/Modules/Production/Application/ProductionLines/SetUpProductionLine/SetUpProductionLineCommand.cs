using SatisfactoryPlanner.Modules.Production.Application.Contracts;
using System;

namespace SatisfactoryPlanner.Modules.Production.Application.ProductionLines.SetUpProductionLine
{
    public class SetUpProductionLineCommand(Guid worldId, string name) : CommandBase
    {
        public Guid WorldId { get; } = worldId;

        public string Name { get; } = name;
    }
}