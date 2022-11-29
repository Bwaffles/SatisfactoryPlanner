using Dapper;
using SatisfactoryPlanner.BuildingBlocks.Application.Data;
using SatisfactoryPlanner.Modules.UserAccess.Application.Configuration.Queries;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SatisfactoryPlanner.Modules.UserAccess.Application.Users.GetUsers
{
    internal class GetUsersQueryHandler : IQueryHandler<GetUsersQuery, List<UserDto>>
    {
        private readonly IDbConnectionFactory _connectionFactory;

        public GetUsersQueryHandler(IDbConnectionFactory connectionFactory) => _connectionFactory = connectionFactory;

        public async Task<List<UserDto>> Handle(GetUsersQuery query, CancellationToken cancellationToken)
        {
            var connection = _connectionFactory.GetOpenConnection();

            const string sql = $"SELECT \"user\".id AS {nameof(UserDto.Id)}, " +
                               $"       \"user\".auth0_user_id AS {nameof(UserDto.Auth0UserId)} " +
                               "   FROM users.users AS \"user\" " +
                               $" WHERE \"user\".auth0_user_id = @{nameof(GetUsersQuery.Auth0UserId)}";

            var param = new
            {
                query.Auth0UserId
            };

            return (await connection.QueryAsync<UserDto>(sql, param)).ToList();
        }
    }
}