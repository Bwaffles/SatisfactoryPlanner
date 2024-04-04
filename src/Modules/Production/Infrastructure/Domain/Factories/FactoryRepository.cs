using SatisfactoryPlanner.Modules.Production.Domain.Factories;
using System.Threading.Tasks;

namespace SatisfactoryPlanner.Modules.Production.Infrastructure.Domain.Factories
{
    /// <summary>
    ///     Handles the database access for the <see cref="Factory" /> through EntityFramework.
    /// </summary>
    /// <remarks>
    ///     This is registered in the DataAccessModule by naming convention.
    /// </remarks>
    internal class FactoryRepository : IFactoryRepository
    {
        private readonly ProductionContext _context;

        internal FactoryRepository(ProductionContext context)
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