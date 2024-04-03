namespace SatisfactoryPlanner.Modules.Production.Domain.ProductionLines
{
    public interface IProductionLineCounter
    {
        int CountProductionLinesWithName(WorldId worldId, ProductionLineName name);
    }
}