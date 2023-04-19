using System.Threading.Tasks;

namespace SatisfactoryPlanner.Modules.Resources.Domain.Nodes
{
    public interface INodeRepository
    {
        /// <summary>
        ///     Find the <see cref="Node" /> with the given <paramref name="nodeId" />, or null if not found.
        /// </summary>
        Task<Node?> FindByIdAsync(NodeId nodeId);
    }
}