using Microsoft.EntityFrameworkCore;
using SatisfactoryPlanner.Modules.Resources.Domain.TappedNodes;
using SatisfactoryPlanner.Modules.Resources.Domain.Worlds;
using System.Threading.Tasks;

namespace SatisfactoryPlanner.Modules.Resources.Infrastructure.Domain.TappedNodes
{
    // ReSharper disable once UnusedMember.Global
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

        public async Task<TappedNode?> FindAsync(WorldId worldId, TappedNodeId tappedNodeId)
        {
            return await _context.TappedNodes
                .SingleOrDefaultAsync(tappedNode =>
                    tappedNode.Id == tappedNodeId &&
                    EF.Property<WorldId>(tappedNode, "_worldId") == worldId
                );
        }
    }
}