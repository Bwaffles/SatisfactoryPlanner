using SatisfactoryPlanner.BuildingBlocks.Domain;
using System.Collections.Generic;

namespace SatisfactoryPlanner.Modules.UserAccess.Domain.Users
{
    public class User : Entity, IAggregateRoot
    {
        public UserId Id { get; private set; }

        private readonly string _username;

        private readonly string _password;

        private readonly string _email;

        private readonly bool _isActive;

        private readonly List<UserRole> _roles;

        private User() { /* Only for EF. */ }

        //internal static User CreateFromUserRegistration(UserRegistrationId userRegistrationId, string username, string password, string email)
        //{
        //    return new User(userRegistrationId, username, password, email);
        //}

        //private User(UserRegistrationId userRegistrationId, string username, string password, string email)
        //{
        //    this.Id = new UserId(userRegistrationId.Value);
        //    _username = username;
        //    _password = password;
        //    _email = email;

        //    _isActive = true;

        //    _roles = new List<UserRole>
        //    {
        //        UserRole.Member
        //    };

        //    this.AddDomainEvent(new UserCreatedDomainEvent(this.Id));
        //}
    }
}
