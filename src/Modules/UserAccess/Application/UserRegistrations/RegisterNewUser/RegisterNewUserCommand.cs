using SatisfactoryPlanner.Modules.UserAccess.Application.Contracts;
using System;

namespace SatisfactoryPlanner.Modules.UserAccess.Application.UserRegistrations.RegisterNewUser
{
    public class RegisterNewUserCommand : CommandBase<Guid>
    {
        public RegisterNewUserCommand(
            string username,
            string password,
            string email,
            string confirmLink)
        {
            Username = username;
            Password = password;
            Email = email;
            ConfirmLink = confirmLink;
        }

        public string Username { get; }

        public string Password { get; }

        public string Email { get; }

        public string ConfirmLink { get; }
    }
}
