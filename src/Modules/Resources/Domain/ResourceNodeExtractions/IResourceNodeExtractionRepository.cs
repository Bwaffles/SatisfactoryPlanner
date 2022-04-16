using SatisfactoryPlanner.Modules.Resources.Domain.ResourceNodes;
using System.Threading.Tasks;

namespace SatisfactoryPlanner.Modules.Resources.Domain.ResourceNodeExtractions
{
    /// <summary>
    ///     Handles the database access for the <see cref="ResourceNodeExtraction"/> through EntityFramework.
    /// </summary>
    public interface IResourceNodeExtractionRepository
    {
        Task AddAsync(ResourceNodeExtraction resourceNodeExtraction);

        Task<ResourceNodeExtraction> GetByResourceNodeIdAsync(ResourceNodeId resourceNodeId);
    }
}
