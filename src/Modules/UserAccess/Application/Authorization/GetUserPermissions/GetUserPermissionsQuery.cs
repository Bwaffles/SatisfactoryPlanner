using SatisfactoryPlanner.Modules.UserAccess.Application.Configuration.Queries;
using System;
using System.Collections.Generic;

namespace SatisfactoryPlanner.Modules.UserAccess.Application.Authorization.GetUserPermissions
{
    public class GetUserPermissionsQuery : QueryBase<List<UserPermissionDto>>
    {
        public Guid UserId { get; }

        public GetUserPermissionsQuery(Guid userId)
        {
            UserId = userId;
        }
    }
}