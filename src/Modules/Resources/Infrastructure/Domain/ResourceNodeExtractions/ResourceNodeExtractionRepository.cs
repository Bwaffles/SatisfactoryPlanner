using Microsoft.EntityFrameworkCore;
using SatisfactoryPlanner.Modules.Resources.Domain.Nodes;
using SatisfactoryPlanner.Modules.Resources.Domain.ResourceNodeExtractions;
using System.Threading.Tasks;

namespace SatisfactoryPlanner.Modules.Resources.Infrastructure.Domain.ResourceNodeExtractions
{
    internal class ResourceNodeExtractionRepository : IResourceNodeExtractionRepository
    {
        private readonly ResourcesContext _context;

        internal ResourceNodeExtractionRepository(ResourcesContext context)
        {
            _context = context;
        }

        public async Task AddAsync(ResourceNodeExtraction resourceNodeExtraction)
        {
            await _context.ResourceNodeExtractions.AddAsync(resourceNodeExtraction);
        }

        public async Task<ResourceNodeExtraction> GetByNodeIdAsync(NodeId nodeId)
        {
            return await _context.ResourceNodeExtractions
                .SingleOrDefaultAsync(c => EF.Property<NodeId>(c, "_nodeId") == nodeId);
        }
    }
}
