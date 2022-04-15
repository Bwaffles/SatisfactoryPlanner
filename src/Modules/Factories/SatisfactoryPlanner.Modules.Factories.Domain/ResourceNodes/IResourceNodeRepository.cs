using System.Threading.Tasks;

namespace SatisfactoryPlanner.Modules.Factories.Domain.ResourceNodes
{
    /// <summary>
    ///     Handles the database access for the <see cref="ResourceNode"/> through EntityFramework.
    /// </summary>
    public interface IResourceNodeRepository
    {
        Task<ResourceNode> GetByIdAsync(ResourceNodeId resourceNodeId);
    }
}
