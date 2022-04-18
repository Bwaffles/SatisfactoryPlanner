using SatisfactoryPlanner.Modules.Resources.Domain.ResourceNodeExtractions;
using System.Threading.Tasks;

namespace SatisfactoryPlanner.Modules.Resources.Infrastructure.Domain.ResourceNodeExtractions
{
    internal class TappedNodeRepository : ITappedNodeRepository
    {
        private readonly ResourcesContext _context;

        internal TappedNodeRepository(ResourcesContext context)
        {
            _context = context;
        }

        public async Task AddAsync(TappedNode tappedNode)
        {
            await _context.TappedNodes.AddAsync(tappedNode);
        }
    }
}
