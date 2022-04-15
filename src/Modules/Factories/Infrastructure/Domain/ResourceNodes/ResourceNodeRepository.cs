using SatisfactoryPlanner.Modules.Factories.Domain.ResourceNodes;
using System.Threading.Tasks;

namespace SatisfactoryPlanner.Modules.Factories.Infrastructure.Domain.ResourceNodes
{
    internal class ResourceNodeRepository : IResourceNodeRepository
    {
        private readonly FactoriesContext _context;

        internal ResourceNodeRepository(FactoriesContext context)
        {
            _context = context;
        }

        public async Task<ResourceNode> GetByIdAsync(ResourceNodeId resourceNodeId)
        {
            return await _context.ResourceNodes.FindAsync(resourceNodeId);
        }
    }
}
