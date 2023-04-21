using SatisfactoryPlanner.Modules.Resources.Domain.Worlds;
using System.Threading.Tasks;

namespace SatisfactoryPlanner.Modules.Resources.Domain.TappedNodes
{
    /// <summary>
    ///     Handles the database access for the <see cref="TappedNode" /> through EntityFramework.
    /// </summary>
    public interface ITappedNodeRepository
    {
        Task AddAsync(TappedNode tappedNode);

        /// <summary>
        ///     Find the <see cref="TappedNode" /> with the given <paramref name="tappedNodeId" />
        ///     that exists in the world, or null if not found.
        /// </summary>
        Task<TappedNode?> FindAsync(WorldId worldId, TappedNodeId tappedNodeId);
    }
}