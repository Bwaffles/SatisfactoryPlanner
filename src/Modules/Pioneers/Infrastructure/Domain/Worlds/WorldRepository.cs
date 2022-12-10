using SatisfactoryPlanner.Modules.Worlds.Domain.Worlds;

namespace SatisfactoryPlanner.Modules.Worlds.Infrastructure.Domain.Worlds
{
    public class WorldRepository : IWorldRepository
    {
        private readonly WorldsContext _context;

        public WorldRepository(WorldsContext context)
        {
            _context = context;
        }

        public async Task AddAsync(World world) => await _context.Worlds.AddAsync(world);
    }
}