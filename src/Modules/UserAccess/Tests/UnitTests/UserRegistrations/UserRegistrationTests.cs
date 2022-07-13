using FluentAssertions;
using Moq;
using SatisfactoryPlanner.Modules.UserAccess.Domain;
using SatisfactoryPlanner.Modules.UserAccess.Domain.UserRegistrations;
using SatisfactoryPlanner.Modules.UserAccess.Domain.UserRegistrations.Events;
using SatisfactoryPlanner.Modules.UserAccess.Domain.UserRegistrations.Rules;
using System;
using Xunit;

namespace SatisfactoryPlanner.Modules.UserAccess.UnitTests.UserRegistrations
{
    public class UserRegistrationTests
    {
        [Fact]
        public void NewUserRegistration_WithUniqueUsername_IsSuccessful()
        {
            var usersCounter = new Mock<IUsersCounter>();
            usersCounter
                .Setup(_ => _.CountUsersWithUsername("username"))
                .Returns(0);

            var userRegistration = UserRegistration.RegisterNewUser(
                "username",
                "password",
                "test@email.com",
                usersCounter.Object,
                "confirmLink");

            var newUserRegisteredDomainEvent =
                DomainEvents.AssertPublishedEvent<NewUserRegisteredDomainEvent>(userRegistration);

            newUserRegisteredDomainEvent.UserRegistrationId
                .Should().BeEquivalentTo(userRegistration.Id);
        }

        [Fact]
        public void NewUserRegistration_WithoutUniqueUsername_BreaksUserUsernameMustBeUniqueRule()
        {
            var usersCounter = new Mock<IUsersCounter>();
            usersCounter
                .Setup(_ => _.CountUsersWithUsername("username"))
                .Returns(1);

            Rules.AssertBrokenRule<UserUsernameMustBeUniqueRule>(() =>
            {
                UserRegistration.RegisterNewUser(
                    "username",
                    "password",
                    "test@email.com",
                    usersCounter.Object,
                    "confirmLink");
            });
        }

        [Fact]
        public void ConfirmingUserRegistration_WhenWaitingForConfirmation_IsSuccessful()
        {
            var usersCounter = new Mock<IUsersCounter>();
            usersCounter
                .Setup(_ => _.CountUsersWithUsername("username"))
                .Returns(0);

            var userRegistration = UserRegistration.RegisterNewUser(
                "username",
                "password",
                "test@email.com",
                usersCounter.Object,
                "confirmLink");

            var date = DateTime.UtcNow;
            SystemClock.Set(date);

            userRegistration.Confirm();

            var userRegistrationConfirmed = DomainEvents.AssertPublishedEvent<UserRegistrationConfirmedDomainEvent>(userRegistration);
            userRegistrationConfirmed.UserRegistrationId.Should().Be(userRegistration.Id);
            userRegistrationConfirmed.ConfirmedDate.Should().Be(date);
        }

        [Fact]
        public void ConfirmingUserRegistration_WhenAlreadyConfirmed_IsSuccessfulAndDoesNotPublishEvent()
        {
            var usersCounter = new Mock<IUsersCounter>();
            usersCounter
                .Setup(_ => _.CountUsersWithUsername("username"))
                .Returns(0);

            var userRegistration = UserRegistration.RegisterNewUser(
                "username",
                "password",
                "test@email.com",
                usersCounter.Object,
                "confirmLink");

            userRegistration.Confirm();
            DomainEvents.AssertPublishedEvent<UserRegistrationConfirmedDomainEvent>(userRegistration);

            userRegistration.ClearDomainEvents();
            
            userRegistration.Confirm();
            DomainEvents.AssertEventIsNotPublished<UserRegistrationConfirmedDomainEvent>(userRegistration,
                "because the registration confirmation event was already triggered when the registration was originally confirmed.");
        }
    }
}