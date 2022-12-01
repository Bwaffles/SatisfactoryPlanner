using SatisfactoryPlanner.BuildingBlocks.Domain.UnitTests;
using SatisfactoryPlanner.Modules.UserAccess.Domain.Users;
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

            Action createNewUser = () => User.CreatePioneer("", usersCounter);
            createNewUser.Should().Throw<ArgumentException>();
        }

        [Test]
        public void CreatePioneer_WithoutUniqueAuth0UserId_BreaksUserAuth0UserIdMustBeUniqueRule()
        {
            const string auth0UserId = "myAuth0UserId";

            var pioneersCounter = Substitute.For<IUsersCounter>();
            pioneersCounter.CountUsersWithAuth0UserId(auth0UserId).Returns(x => 1);

            RuleAssertions.AssertBrokenRule<UserAuth0UserIdMustBeUniqueRule>(() =>
            {
                User.CreatePioneer(auth0UserId, pioneersCounter);
            });
        }

        [Test]
        public void CreatePioneer_WithUniqueAuth0UserId_IsSuccessful()
        {
            const string auth0UserId = "myAuth0UserId";

            var pioneersCounter = Substitute.For<IUsersCounter>();
            pioneersCounter.CountUsersWithAuth0UserId(auth0UserId).Returns(x => 0);

            User user;
            Action createPioneer = () => user = User.CreatePioneer(auth0UserId, pioneersCounter);
            createPioneer.Should().NotThrow();
        }
    }
}
