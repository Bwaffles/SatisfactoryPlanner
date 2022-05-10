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
        public void NewUserRegistration_WithUniqueLogin_IsSuccessful()
        {
            var usersCounter = new Mock<IUsersCounter>();
            usersCounter
                .Setup(_ => _.CountUsersWithLogin("login"))
                .Returns(0);

            var userRegistration = UserRegistration.RegisterNewUser(
                "login",
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
        public void NewUserRegistration_WithoutUniqueLogin_BreaksUserLoginMustBeUniqueRule()
        {
            var usersCounter = new Mock<IUsersCounter>();
            usersCounter
                .Setup(_ => _.CountUsersWithLogin("login"))
                .Returns(1);

            Rules.AssertBrokenRule<UserLoginMustBeUniqueRule>(() =>
            {
                UserRegistration.RegisterNewUser(
                    "login",
                    "password",
                    "test@email",
                    usersCounter.Object,
                    "confirmLink");
            });
        }
    }
}