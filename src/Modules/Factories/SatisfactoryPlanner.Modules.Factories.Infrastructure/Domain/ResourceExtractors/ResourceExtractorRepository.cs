using SatisfactoryPlanner.Modules.Factories.Domain.ResourceExtractors;
using System.Threading.Tasks;

namespace SatisfactoryPlanner.Modules.Factories.Infrastructure.Domain.ResourceExtractors
{
    /// <summary>
    ///     Handles the database access for the <see cref="ResourceExtractor"/> through EntityFramework.
    /// </summary>
    /// <remarks>
    ///     This is registered in the Factories.DataAccessModule by naming convention.
    /// </remarks>
    internal class ResourceExtractorRepository : IResourceExtractorRepository
    {
        private readonly FactoriesContext _context;

        internal ResourceExtractorRepository(FactoriesContext context)
        {
            _context = context;
        }

        public async Task<ResourceExtractor> GetByIdAsync(ResourceExtractorId resourceExtractorId)
        {
            return await _context.ResourceExtractors.FindAsync(resourceExtractorId);
        }
    }
}
