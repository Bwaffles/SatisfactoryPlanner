namespace SatisfactoryPlanner.Modules.Production.Domain.ProductionLines
{
    public interface IProductionLineRepository
    {
        Task AddAsync(ProductionLine productionLine);

        Task<ProductionLine?> FindAsync(WorldId worldId, ProductionLineId productionLineId);
    }
}
