using SatisfactoryPlanner.Modules.Resources.Domain.Extractors;
using System.Threading.Tasks;

namespace SatisfactoryPlanner.Modules.Resources.Infrastructure.Domain.Extractors
{
    /// <summary>
    ///     Handles the database access for the <see cref="Extractor"/> through EntityFramework.
    /// </summary>
    /// <remarks>
    ///     This is registered in the Resources.DataAccessModule by naming convention.
    /// </remarks>
    internal class ExtractorRepository : IExtractorRepository
    {
        private readonly ResourcesContext _context;

        internal ExtractorRepository(ResourcesContext context)
        {
            _context = context;
        }

        public async Task<Extractor> GetByIdAsync(ExtractorId extractorId)
        {
            return await _context.Extractors.FindAsync(extractorId);
        }
    }
}
