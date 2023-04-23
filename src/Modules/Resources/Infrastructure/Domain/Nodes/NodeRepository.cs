using Microsoft.EntityFrameworkCore;
using SatisfactoryPlanner.Modules.Resources.Domain.Nodes;
using System.Collections.Generic;
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

        public Node? FindById(NodeId nodeId)
            => _context.Nodes.Find(nodeId);

        public async Task<List<Node>> GetAllAsync()
            => await _context.Nodes.ToListAsync();

        public async Task<Node> GetByIdAsync(NodeId nodeId)
            => await _context.Nodes.SingleAsync(node => node.Id == nodeId);
    }
}