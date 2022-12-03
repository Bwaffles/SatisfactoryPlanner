using SatisfactoryPlanner.Modules.UserAccess.Application.Configuration.Queries;
using System.Collections.Generic;

namespace SatisfactoryPlanner.Modules.UserAccess.Application.Users.GetUsers
{
    public class GetUsersQuery : QueryBase<List<UserDto>>
    {
        public string Auth0UserId { get; }

        public GetUsersQuery(string auth0UserId) => Auth0UserId = auth0UserId;
    }
}