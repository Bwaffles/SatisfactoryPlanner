using SatisfactoryPlanner.Modules.UserAccess.Domain.Users;
using System.Threading.Tasks;

namespace SatisfactoryPlanner.Modules.UserAccess.Infrastructure.Domain.Users
{
    internal class UserRepository : IUserRepository
    {
        private readonly UserAccessContext _userAccessContext;

        public UserRepository(UserAccessContext userAccessContext) => _userAccessContext = userAccessContext;

        public async Task AddAsync(User user) => await _userAccessContext.AddAsync(user);
    }
}