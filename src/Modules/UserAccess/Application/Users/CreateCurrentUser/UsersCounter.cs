using Dapper;
using SatisfactoryPlanner.BuildingBlocks.Application.Data;
using SatisfactoryPlanner.Modules.UserAccess.Domain.Users;

namespace SatisfactoryPlanner.Modules.UserAccess.Application.Users.CreateCurrentUser
{
    public class UsersCounter : IUsersCounter
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;

        public UsersCounter(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        public int CountUsersWithAuth0UserId(string auth0UserId)
        {
            var connection = _dbConnectionFactory.GetOpenConnection(); 
            string sql = "SELECT COUNT(*) " + 
                         "  FROM users.users AS \"user\" " + 
                         $"WHERE \"user\".auth0_user_id = @{nameof(auth0UserId)}";
            var param = new
            {
                auth0UserId
            };

            return connection.QuerySingle<int>(sql, param);
        }
    }
}
