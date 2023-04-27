using SatisfactoryPlanner.Modules.Resources.Domain.Nodes;
using SatisfactoryPlanner.Modules.Resources.Domain.Worlds;
using System.Threading.Tasks;

namespace SatisfactoryPlanner.Modules.Resources.Domain.WorldNodes
{
    /// <summary>
    ///     Handles the database access for the <see cref="WorldNode" /> through EntityFramework.
    /// </summary>
    public interface IWorldNodeRepository
    {
        Task AddAsync(WorldNode worldNode);

        /// <summary>
        ///     Find the <see cref="WorldNode" /> for the given <paramref name="nodeId" />
        ///     that exists in the world, or null if not found.
        /// </summary>
        Task<WorldNode?> FindAsync(WorldId worldId, NodeId nodeId);
    }
}