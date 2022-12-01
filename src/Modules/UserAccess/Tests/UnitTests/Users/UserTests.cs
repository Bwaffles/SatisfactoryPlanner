using SatisfactoryPlanner.BuildingBlocks.Domain.UnitTests;
using SatisfactoryPlanner.Modules.UserAccess.Domain.Users;
using SatisfactoryPlanner.Modules.UserAccess.Domain.Users.Events;
using SatisfactoryPlanner.Modules.UserAccess.Domain.Users.Rules;
using System;

namespace SatisfactoryPlanner.Modules.UserAccess.UnitTests.Users
{
    [TestFixture]
    public class UserTests
    {
        [Test]
        public void CreatePioneer_WithEmptyAuth0UserId_ThrowsException()
        {
            var usersCounter = Substitute.For<IUsersCounter>();

            Action createPioneer = () => User.CreatePioneer("", usersCounter);
            createPioneer.Should().Throw<ArgumentException>();
        }

        [Test]
        public void CreatePioneer_WithoutUniqueAuth0UserId_BreaksUserAuth0UserIdMustBeUniqueRule()
        {
            const string auth0UserId = "myAuth0UserId";

            var usersCounter = Substitute.For<IUsersCounter>();
            usersCounter.CountUsersWithAuth0UserId(auth0UserId).Returns(x => 1);

            RuleAssertions.AssertBrokenRule<UserAuth0UserIdMustBeUniqueRule>(() =>
            {
                User.CreatePioneer(auth0UserId, usersCounter);
            });
        }

        [Test]
        public void CreatePioneer_WithUniqueAuth0UserId_IsSuccessful()
        {
            const string auth0UserId = "myAuth0UserId";

            var pioneersCounter = Substitute.For<IUsersCounter>();
            pioneersCounter.CountUsersWithAuth0UserId(auth0UserId).Returns(x => 0);

            var user = User.CreatePioneer(auth0UserId, pioneersCounter);

            var pioneerUserCreateDomainEvent = DomainEventAssertions
                .AssertPublishedEvent<PioneerUserCreatedDomainEvent>(user);

            pioneerUserCreateDomainEvent.UserId
                .Should().BeEquivalentTo(user.Id);
        }
    }
}