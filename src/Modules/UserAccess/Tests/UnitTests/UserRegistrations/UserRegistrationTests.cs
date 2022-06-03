using FluentAssertions;
using Moq;
using SatisfactoryPlanner.Modules.UserAccess.Domain.UserRegistrations;
using SatisfactoryPlanner.Modules.UserAccess.Domain.UserRegistrations.Events;
using SatisfactoryPlanner.Modules.UserAccess.Domain.UserRegistrations.Rules;
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
                "test@email",
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
                    "test@email",
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
                "test@email",
                usersCounter.Object,
                "confirmLink");

            userRegistration.Confirm();

            var userRegistrationConfirmedDomainEvent =
                DomainEvents.AssertPublishedEvent<UserRegistrationConfirmedDomainEvent>(userRegistration);

            userRegistrationConfirmedDomainEvent.UserRegistrationId
                .Should().BeEquivalentTo(userRegistration.Id);
        }

        [Fact]
        public void UserRegistration_WhenAlreadyConfirmed_CannotBeConfirmedAgain()
        {
            var usersCounter = new Mock<IUsersCounter>();
            usersCounter
                .Setup(_ => _.CountUsersWithUsername("username"))
                .Returns(0);

            var userRegistration = UserRegistration.RegisterNewUser(
                "username",
                "password",
                "test@email",
                usersCounter.Object,
                "confirmLink");

            userRegistration.Confirm();

            Rules.AssertBrokenRule<UserRegistrationCannotBeConfirmedMoreThanOnceRule>(() =>
            {
                userRegistration.Confirm();
            });
        }
    }
}