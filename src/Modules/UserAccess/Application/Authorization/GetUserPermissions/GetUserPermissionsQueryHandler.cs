using Dapper;
using SatisfactoryPlanner.BuildingBlocks.Application.Data;
using SatisfactoryPlanner.Modules.UserAccess.Application.Configuration.Queries;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SatisfactoryPlanner.Modules.UserAccess.Application.Authorization.GetUserPermissions
{
    // ReSharper disable once UnusedMember.Global
    internal class GetUserPermissionsQueryHandler : IQueryHandler<GetUserPermissionsQuery, List<UserPermissionDto>>
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;

        public GetUserPermissionsQueryHandler(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        public async Task<List<UserPermissionDto>> Handle(GetUserPermissionsQuery request,
            CancellationToken cancellationToken)
        {
            var connection = _dbConnectionFactory.GetOpenConnection();

            const string sql = $"SELECT permission.code AS {nameof(UserPermissionDto.Code)} " +
                               "   FROM users.permissions AS permission " +
                               "   JOIN users.role_permissions AS role_permission ON role_permission.permission_code = permission.code " +
                               "   JOIN users.user_roles AS user_role ON user_role.role_code = role_permission.role_code " +
                               $" WHERE user_role.user_id = @{nameof(GetUserPermissionsQuery.UserId)}";
            var param = new
            {
                request.UserId
            };
            return (await connection.QueryAsync<UserPermissionDto>(sql, param)).AsList();
        }
    }
}