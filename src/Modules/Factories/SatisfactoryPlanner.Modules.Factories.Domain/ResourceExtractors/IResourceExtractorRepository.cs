using System.Threading.Tasks;

namespace SatisfactoryPlanner.Modules.Factories.Domain.ResourceExtractors
{
    /// <summary>
    ///     Handles the database access for the <see cref="ResourceExtractor"/> through EntityFramework.
    /// </summary>
    public interface IResourceExtractorRepository
    {
        Task<ResourceExtractor> GetByIdAsync(ResourceExtractorId resourceExtractorId);
    }
}
