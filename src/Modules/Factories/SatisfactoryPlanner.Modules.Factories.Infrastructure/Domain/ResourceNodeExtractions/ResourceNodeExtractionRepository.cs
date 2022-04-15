using Microsoft.EntityFrameworkCore;
using SatisfactoryPlanner.Modules.Factories.Domain.ResourceNodeExtractions;
using SatisfactoryPlanner.Modules.Factories.Domain.ResourceNodes;
using System.Threading.Tasks;

namespace SatisfactoryPlanner.Modules.Factories.Infrastructure.Domain.ResourceNodeExtractions
{
    internal class ResourceNodeExtractionRepository : IResourceNodeExtractionRepository
    {
        private readonly FactoriesContext _context;

        internal ResourceNodeExtractionRepository(FactoriesContext context)
        {
            _context = context;
        }

        public async Task AddAsync(ResourceNodeExtraction resourceNodeExtraction)
        {
            await _context.ResourceNodeExtractions.AddAsync(resourceNodeExtraction);
        }

        public async Task<ResourceNodeExtraction> GetByResourceNodeIdAsync(ResourceNodeId resourceNodeId)
        {
            return await _context.ResourceNodeExtractions
                .SingleOrDefaultAsync(c => EF.Property<ResourceNodeId>(c, "_resourceNodeId") == resourceNodeId);
        }
    }
}
