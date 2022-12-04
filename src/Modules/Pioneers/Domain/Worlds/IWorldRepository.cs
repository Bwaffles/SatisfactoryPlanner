namespace SatisfactoryPlanner.Modules.Pioneers.Domain.Worlds
{
    /// <summary>
    ///     Handles the database access for the <see cref="World" /> through EntityFramework.
    /// </summary>
    public interface IWorldRepository
    {
        Task AddAsync(World world);
    }
}