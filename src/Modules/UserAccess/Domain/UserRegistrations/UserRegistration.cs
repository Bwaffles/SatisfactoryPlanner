using SatisfactoryPlanner.BuildingBlocks.Domain;
using SatisfactoryPlanner.Modules.UserAccess.Domain.UserRegistrations.Events;
using SatisfactoryPlanner.Modules.UserAccess.Domain.UserRegistrations.Rules;
using System;

namespace SatisfactoryPlanner.Modules.UserAccess.Domain.UserRegistrations
{
    public class UserRegistration : Entity, IAggregateRoot
    {
        public UserRegistrationId Id { get; }

        private readonly string _username;

        private readonly string _password;

        private readonly string _email;

        private readonly DateTime _registerDate;

        private UserRegistrationStatus _status;

        private DateTime? _confirmedDate;

        private UserRegistration() { /* Only EF. */ }

        public static UserRegistration RegisterNewUser(
            string username,
            string password,
            string email,
            IUsersCounter usersCounter,
            string confirmLink)
        {
            return new UserRegistration(username, password, email, usersCounter, confirmLink);
        }

        private UserRegistration(
            string username,
            string password,
            string email,
            IUsersCounter usersCounter,
            string confirmLink)
        {
            CheckRule(new UserUsernameMustBeUniqueRule(usersCounter, username));

            Id = new UserRegistrationId(Guid.NewGuid());
            _username = username;
            _password = password;
            _email = email;
            _registerDate = DateTime.UtcNow;
            _status = UserRegistrationStatus.WaitingForConfirmation;

            AddDomainEvent(new NewUserRegisteredDomainEvent(
                Id,
                _username,
                _email,
                _registerDate,
                confirmLink));
        }

        //public User CreateUser()
        //{
        //    this.CheckRule(new UserCannotBeCreatedWhenRegistrationIsNotConfirmedRule(_status));

        //    return User.CreateFromUserRegistration(
        //        Id,
        //        _login,
        //        _password,
        //        _email);
        //}

        public void Confirm()
        {
            CheckRule(new UserRegistrationCannotBeConfirmedMoreThanOnceRule(_status));

            _status = UserRegistrationStatus.Confirmed;
            _confirmedDate = DateTime.UtcNow;

            AddDomainEvent(new UserRegistrationConfirmedDomainEvent(Id));
        }
    }
}
