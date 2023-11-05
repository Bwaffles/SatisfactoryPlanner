using System.Threading.Tasks;

namespace SatisfactoryPlanner.Modules.Resources.Domain.Extractors
{
    public interface IExtractorRepository
    {
        /// <summary>
        ///     Find the <see cref="Extractor" /> with the given <paramref name="extractorId" />,
        ///     or null if not found.
        /// </summary>
        Task<Extractor?> FindByIdAsync(ExtractorId? extractorId);

        /// <summary>
        ///     Find the <see cref="Extractor" /> with the given <paramref name="extractorId" />,
        ///     or null if not found.
        /// </summary>
        Extractor? FindById(ExtractorId extractorId);
    }
}