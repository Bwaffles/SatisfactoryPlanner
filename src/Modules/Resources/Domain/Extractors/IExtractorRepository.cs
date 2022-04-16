using System.Threading.Tasks;

namespace SatisfactoryPlanner.Modules.Resources.Domain.Extractors
{
    /// <summary>
    ///     Handles the database access for the <see cref="Extractor"/> through EntityFramework.
    /// </summary>
    public interface IExtractorRepository
    {
        Task<Extractor> GetByIdAsync(ExtractorId extractorId);
    }
}
