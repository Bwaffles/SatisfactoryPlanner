using System.Threading.Tasks;

namespace SatisfactoryPlanner.Modules.Resources.Domain.TappedNodes
{
    /// <summary>
    ///     Handles the database access for the <see cref="TappedNode"/> through EntityFramework.
    /// </summary>
    public interface ITappedNodeRepository
    {
        Task AddAsync(TappedNode tappedNode);
    }
}
