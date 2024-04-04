using System.Threading.Tasks;

namespace SatisfactoryPlanner.Modules.Production.Domain.Factories
{
    /// <summary>
    ///     Handles the database access for the <see cref="Factory" /> through EntityFramework.
    /// </summary>
    public interface IFactoryRepository
    {
        Task AddAsync(Factory factory);

        Task<Factory> GetByIdAsync(FactoryId factoryId);
    }
}