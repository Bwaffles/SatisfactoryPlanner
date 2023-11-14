using Dapper;
using SatisfactoryPlanner.BuildingBlocks.Application;
using SatisfactoryPlanner.BuildingBlocks.Application.Data;
using SatisfactoryPlanner.Modules.UserAccess.Application.Configuration.Queries;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SatisfactoryPlanner.Modules.UserAccess.Application.Users.GetCurrentUser
{
    internal class GetCurrentUserQueryHandler : IQueryHandler<GetCurrentUserQuery, CurrentUserDto?>
    {
        private readonly IDbConnectionFactory _connectionFactory;
        private readonly IExecutionContextAccessor _executionContextAccessor;

        public GetCurrentUserQueryHandler(IDbConnectionFactory connectionFactory,
            IExecutionContextAccessor executionContextAccessor)
        {
            _connectionFactory = connectionFactory;
            _executionContextAccessor = executionContextAccessor;
        }

        public async Task<CurrentUserDto?> Handle(GetCurrentUserQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var connection = _connectionFactory.GetOpenConnection();
                const string userSql = $"SELECT \"user\".id AS {nameof(CurrentUserDto.Id)}, " +
                                       $"       \"user\".auth0_user_id AS {nameof(CurrentUserDto.Auth0UserId)} " +
                                       "   FROM users.users AS \"user\" " +
                                       $" WHERE \"user\".id = @{nameof(IExecutionContextAccessor.UserId)}";

                var userParam = new
                {
                    _executionContextAccessor.UserId
                };

                var user = await connection.QuerySingleOrDefaultAsync<CurrentUserDto>(userSql, userParam);
                if (user == null)
                    return user;

                const string rolesSql = $"SELECT user_role.role_code AS {nameof(UserRoleDto.RoleCode)} " +
                                        "   FROM users.user_roles AS user_role " +
                                        $" WHERE user_role.user_id = @{nameof(IExecutionContextAccessor.UserId)}";

                var rolesParam = new
                {
                    _executionContextAccessor.UserId
                };
                user.Roles = await connection.QueryAsync<UserRoleDto>(rolesSql, rolesParam);

                return user;
            }
            catch (ApplicationException)
            {
                return null;
            }
        }
    }
}