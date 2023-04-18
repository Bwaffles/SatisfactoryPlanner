using SatisfactoryPlanner.Modules.Resources.Domain.Extractors;
using System.Threading.Tasks;

namespace SatisfactoryPlanner.Modules.Resources.Infrastructure.Domain.Extractors
{
    // ReSharper disable once UnusedMember.Global
    internal class ExtractorRepository : IExtractorRepository
    {
        private readonly ResourcesContext _context;

        public ExtractorRepository(ResourcesContext context)
        {
            _context = context;
        }

        public async Task<Extractor?> FindByIdAsync(ExtractorId extractorId)
            => await _context.Extractors.FindAsync(extractorId);
    }
}