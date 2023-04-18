using SatisfactoryPlanner.Modules.Resources.Domain.Nodes;
using System.Threading.Tasks;

namespace SatisfactoryPlanner.Modules.Resources.Infrastructure.Domain.Nodes
{
    // ReSharper disable once UnusedMember.Global
    internal class NodeRepository : INodeRepository
    {
        private readonly ResourcesContext _context;

        public NodeRepository(ResourcesContext context)
        {
            _context = context;
        }

        public async Task<Node?> FindByIdAsync(NodeId nodeId)
            => await _context.Nodes.FindAsync(nodeId);
    }
}