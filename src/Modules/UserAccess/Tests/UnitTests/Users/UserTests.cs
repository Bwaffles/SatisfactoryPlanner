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
        public void CreateNewPioneer_WithEmptyAuth0UserId_ThrowsException()
        {
            var usersCounter = Substitute.For<IUsersCounter>();

            Action createNewUser = () => User.CreateNewPioneer("", usersCounter);
            createNewUser.Should().Throw<ArgumentException>();
        }

        [Test]
        public void CreateNewPioneer_WithoutUniqueAuth0UserId_BreaksUserAuth0UserIdMustBeUniqueRule()
        {
            const string auth0UserId = "myAuth0UserId";

            var pioneersCounter = Substitute.For<IUsersCounter>();
            pioneersCounter.CountUsersWithAuth0UserId(auth0UserId).Returns(x => 1);

            RuleAssertions.AssertBrokenRule<UserAuth0UserIdMustBeUniqueRule>(() =>
            {
                User.CreateNewPioneer(auth0UserId, pioneersCounter);
            });
        }

        [Test]
        public void CreateNewPioneer_WithUniqueAuth0UserId_IsSuccessful()
        {
            const string auth0UserId = "myAuth0UserId";

            var pioneersCounter = Substitute.For<IUsersCounter>();
            pioneersCounter.CountUsersWithAuth0UserId(auth0UserId).Returns(x => 0);

            User user;
            Action createNewPioneer = () => user = User.CreateNewPioneer(auth0UserId, pioneersCounter);
            createNewPioneer.Should().NotThrow();
        }
    }
}
