using SatisfactoryPlanner.Modules.Factories.Domain.ResourceNodes;
using System.Threading.Tasks;

namespace SatisfactoryPlanner.Modules.Factories.Domain.ResourceNodeExtractions
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
