using SatisfactoryPlanner.Modules.Pioneers.Domain.Worlds;

namespace SatisfactoryPlanner.Modules.Pioneers.Infrastructure.Domain.Worlds
{
    public class WorldRepository : IWorldRepository
    {
        private readonly PioneersContext _context;

        public WorldRepository(PioneersContext context)
        {
            _context = context;
        }

        public async Task AddAsync(World world) => await _context.Worlds.AddAsync(world);
    }
}