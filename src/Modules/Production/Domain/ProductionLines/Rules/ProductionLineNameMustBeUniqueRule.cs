using SatisfactoryPlanner.BuildingBlocks.Domain;

namespace SatisfactoryPlanner.Modules.Production.Domain.ProductionLines.Rules
{
    public class ProductionLineNameMustBeUniqueRule(
        WorldId worldId,
        ProductionLineName name,
        IProductionLineCounter productionLineCounter)
        : IBusinessRule
    {
        public string Message => "Production line name must be unique.";

        public bool IsBroken() => productionLineCounter.CountProductionLinesWithName(worldId, name) > 0;
    }
}