namespace SatisfactoryPlanner.Modules.Warehouses.Domain.ItemSources;

public interface IItemSourcesRepository
{
    Task AddAsync(ItemSource itemSource);
}
