using SatisfactoryPlanner.Modules.Worlds.Domain.Pioneers;

namespace SatisfactoryPlanner.Modules.Worlds.Infrastructure.Domain.Pioneers
{
    /// <summary>
    ///     Handles the database access for the <see cref="Pioneer" /> through EntityFramework.
    /// </summary>
    /// <remarks>
    ///     This is registered in the DataAccessModule by naming convention.
    /// </remarks>
    internal class PioneersRepository : IPioneersRepository
    {
        private readonly WorldsContext _context;

        internal PioneersRepository(WorldsContext context) => _context = context;

        public async Task AddAsync(Pioneer pioneer) => await _context.Pioneers.AddAsync(pioneer);
    }
}