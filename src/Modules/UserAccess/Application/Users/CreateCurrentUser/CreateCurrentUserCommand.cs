using SatisfactoryPlanner.Modules.UserAccess.Application.Contracts;
using System;

namespace SatisfactoryPlanner.Modules.UserAccess.Application.Users.CreateCurrentUser
{
    public class CreateCurrentUserCommand : CommandBase<Guid>
    {
        public string Auth0UserId { get; }

        public CreateCurrentUserCommand(string auth0UserId)
        {
            Auth0UserId = auth0UserId;
        }
    }
}