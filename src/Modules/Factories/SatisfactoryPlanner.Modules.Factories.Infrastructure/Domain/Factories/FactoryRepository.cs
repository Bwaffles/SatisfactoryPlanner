using SatisfactoryPlanner.Modules.Factories.Domain.Factories;
using System.Threading.Tasks;

namespace SatisfactoryPlanner.Modules.Factories.Infrastructure.Domain.Factories
{
    /// <summary>
    ///     Handles the database access for the <see cref="Factory"/> through EntityFramework.
    /// </summary>
    /// <remarks>
    ///     This is registered in the Factories.DataAccessModule by naming convention.
    /// </remarks>
    internal class FactoryRepository : IFactoryRepository
    {
        private readonly FactoriesContext _context;

        internal FactoryRepository(FactoriesContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Factory factory)
        {
            await _context.Factories.AddAsync(factory);
        }

        public async Task<Factory> GetByIdAsync(FactoryId factoryId)
        {
            return await _context.Factories.FindAsync(factoryId);
        }
    }
}
