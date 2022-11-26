using SatisfactoryPlanner.Modules.Pioneers.Domain.Pioneers;

namespace SatisfactoryPlanner.Modules.Pioneers.Infrastructure.Domain.Pioneers
{
    /// <summary>
    ///     Handles the database access for the <see cref="Pioneer" /> through EntityFramework.
    /// </summary>
    /// <remarks>
    ///     This is registered in the Factories.DataAccessModule by naming convention.
    /// </remarks>
    internal class PioneersRepository : IPioneersRepository
    {
        private readonly PioneersContext _context;

        internal PioneersRepository(PioneersContext context) => _context = context;

        public async Task AddAsync(Pioneer pioneer) => await _context.Pioneers.AddAsync(pioneer);

        public async Task<Pioneer> GetByIdAsync(PioneerId pioneerId) => await _context.Pioneers.FindAsync(pioneerId);
    }
}