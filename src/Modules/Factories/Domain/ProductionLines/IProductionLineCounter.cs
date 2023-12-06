namespace SatisfactoryPlanner.Modules.Factories.Domain.ProductionLines
{
    public interface IProductionLineCounter
    {
        int CountProductionLinesWithName(WorldId worldId, ProductionLineName name);
    }
}