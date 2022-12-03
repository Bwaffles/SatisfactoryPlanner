using System.Threading.Tasks;

namespace SatisfactoryPlanner.Modules.UserAccess.Domain.Users
{
    public interface IUserRepository
    {
        Task AddAsync(User user);
    }
}