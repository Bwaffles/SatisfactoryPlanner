using Microsoft.EntityFrameworkCore;
using SatisfactoryPlanner.Modules.Warehouses.Domain.ItemSources;

namespace SatisfactoryPlanner.Modules.Warehouses.Infrastructure.Domain.ItemSources;

internal class ItemSourcesRepository(WarehousesContext context) : IItemSourcesRepository
{
    private readonly WarehousesContext _context = context;

    public async Task AddAsync(ItemSource itemSource) => await _context.ItemSources.AddAsync(itemSource);

    public async Task<ItemSource?> FindAsync(WorldId worldId, SourceId sourceId) => await _context.ItemSources
        .SingleOrDefaultAsync(worldNode =>
            EF.Property<WorldId>(worldNode, "_worldId") == worldId &&
            EF.Property<Source>(worldNode, "_source").Id == sourceId

        );
}
