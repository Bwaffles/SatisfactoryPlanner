using Microsoft.EntityFrameworkCore;
using SatisfactoryPlanner.Modules.Resources.Domain.Nodes;
using SatisfactoryPlanner.Modules.Resources.Domain.WorldNodes;
using SatisfactoryPlanner.Modules.Resources.Domain.Worlds;
using System.Threading.Tasks;

namespace SatisfactoryPlanner.Modules.Resources.Infrastructure.Domain.WorldNodes
{
    // ReSharper disable once UnusedMember.Global
    internal class WorldNodeRepository : IWorldNodeRepository
    {
        private readonly ResourcesContext _context;

        internal WorldNodeRepository(ResourcesContext context)
        {
            _context = context;
        }

        public async Task AddAsync(WorldNode worldNode)
        {
            await _context.WorldNodes.AddAsync(worldNode);
        }

        public async Task<WorldNode?> FindAsync(WorldId worldId, NodeId nodeId)
        {
            return await _context.WorldNodes
                .SingleOrDefaultAsync(worldNode =>
                    EF.Property<NodeId>(worldNode, "_nodeId") == nodeId &&
                    EF.Property<WorldId>(worldNode, "_worldId") == worldId
                );
        }
    }
}