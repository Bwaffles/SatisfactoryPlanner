using SatisfactoryPlanner.Modules.Resources.Domain.ResourceExtractors;
using System.Threading.Tasks;

namespace SatisfactoryPlanner.Modules.Resources.Infrastructure.Domain.ResourceExtractors
{
    /// <summary>
    ///     Handles the database access for the <see cref="ResourceExtractor"/> through EntityFramework.
    /// </summary>
    /// <remarks>
    ///     This is registered in the Resources.DataAccessModule by naming convention.
    /// </remarks>
    internal class ResourceExtractorRepository : IResourceExtractorRepository
    {
        private readonly ResourcesContext _context;

        internal ResourceExtractorRepository(ResourcesContext context)
        {
            _context = context;
        }

        public async Task<ResourceExtractor> GetByIdAsync(ResourceExtractorId resourceExtractorId)
        {
            return await _context.ResourceExtractors.FindAsync(resourceExtractorId);
        }
    }
}
