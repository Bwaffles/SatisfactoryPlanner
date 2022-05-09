using SatisfactoryPlanner.Modules.Resources.Domain.TappedNodes;
using System.Threading.Tasks;

namespace SatisfactoryPlanner.Modules.Resources.Infrastructure.Domain.TappedNodes
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
