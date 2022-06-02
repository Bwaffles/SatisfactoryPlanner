using Dapper;
using SatisfactoryPlanner.BuildingBlocks.Application.Data;
using SatisfactoryPlanner.Modules.UserAccess.Domain.UserRegistrations;

namespace SatisfactoryPlanner.Modules.UserAccess.Application.UserRegistrations.RegisterNewUser
{
    public class UsersCounter : IUsersCounter
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;

        public UsersCounter(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        public int CountUsersWithUsername(string username)
        {
            var connection = _dbConnectionFactory.GetOpenConnection();
            const string sql = "SELECT COUNT(*) " +
                               "  FROM users.users AS \"user\" " +
                               " WHERE \"user\".username = @username";

            return connection.QuerySingle<int>(
                sql,
                new
                {
                    username
                });
        }
    }
}
