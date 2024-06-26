using SatisfactoryPlanner.Modules.Warehouses.Domain.ItemSources;

namespace SatisfactoryPlanner.Modules.Warehouses.Infrastructure.Domain.ItemSources;

internal class ItemSourcesRepository(WarehousesContext context) : IItemSourcesRepository
{
    private readonly WarehousesContext _context = context;

    public async Task AddAsync(ItemSource itemSource) => await _context.ItemSources.AddAsync(itemSource);
}
