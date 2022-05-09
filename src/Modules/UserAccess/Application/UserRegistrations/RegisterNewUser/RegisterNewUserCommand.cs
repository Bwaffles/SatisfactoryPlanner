using SatisfactoryPlanner.Modules.UserAccess.Application.Contracts;
using System;

namespace SatisfactoryPlanner.Modules.UserAccess.Application.UserRegistrations.RegisterNewUser
{
    public class RegisterNewUserCommand : CommandBase<Guid>
    {
        public RegisterNewUserCommand(
            string login,
            string password,
            string email,
            string confirmLink)
        {
            Login = login;
            Password = password;
            Email = email;
            ConfirmLink = confirmLink;
        }

        public string Login { get; }

        public string Password { get; }

        public string Email { get; }

        public string ConfirmLink { get; }
    }
}
