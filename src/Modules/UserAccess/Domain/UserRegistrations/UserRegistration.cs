﻿using SatisfactoryPlanner.BuildingBlocks.Domain;
using SatisfactoryPlanner.Modules.UserAccess.Domain.UserRegistrations.Events;
using SatisfactoryPlanner.Modules.UserAccess.Domain.UserRegistrations.Rules;
using System;

namespace SatisfactoryPlanner.Modules.UserAccess.Domain.UserRegistrations
{
    public class UserRegistration : Entity, IAggregateRoot
    {
        public UserRegistrationId Id { get; }

        private readonly string _login;

        private readonly string _password;

        private readonly string _email;

        private readonly DateTime _registerDate;

        private readonly UserRegistrationStatus _status;

        private readonly DateTime? _confirmedDate;

        private UserRegistration() { /* Only EF. */ }

        public static UserRegistration RegisterNewUser(
            string login,
            string password,
            string email,
            IUsersCounter usersCounter,
            string confirmLink)
        {
            return new UserRegistration(login, password, email, usersCounter, confirmLink);
        }

        private UserRegistration(
            string login,
            string password,
            string email,
            IUsersCounter usersCounter,
            string confirmLink)
        {
            CheckRule(new UserLoginMustBeUniqueRule(usersCounter, login));

            Id = new UserRegistrationId(Guid.NewGuid());
            _login = login;
            _password = password;
            _email = email;
            _registerDate = DateTime.UtcNow;
            _status = UserRegistrationStatus.WaitingForConfirmation;

            AddDomainEvent(new NewUserRegisteredDomainEvent(
                Id,
                _login,
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

        //public void Confirm()
        //{
        //    this.CheckRule(new UserRegistrationCannotBeConfirmedMoreThanOnceRule(_status));
        //    this.CheckRule(new UserRegistrationCannotBeConfirmedAfterExpirationRule(_status));

        //    _status = UserRegistrationStatus.Confirmed;
        //    _confirmedDate = DateTime.UtcNow;

        //    this.AddDomainEvent(new UserRegistrationConfirmedDomainEvent(Id));
        //}

        //public void Expire()
        //{
        //    this.CheckRule(new UserRegistrationCannotBeExpiredMoreThanOnceRule(_status));

        //    _status = UserRegistrationStatus.Expired;

        //    this.AddDomainEvent(new UserRegistrationExpiredDomainEvent(Id));
        //}
    }
}