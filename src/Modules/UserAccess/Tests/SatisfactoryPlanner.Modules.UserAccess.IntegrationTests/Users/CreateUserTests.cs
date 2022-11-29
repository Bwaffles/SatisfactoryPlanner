using SatisfactoryPlanner.Modules.UserAccess.Application.Users.GetCurrentUser;
using SatisfactoryPlanner.Modules.UserAccess.IntegrationTests.SeedWork;

namespace SatisfactoryPlanner.Modules.UserAccess.IntegrationTests.Users
{
    [TestFixture]
    public class CreateUserTests : TestBase
    {
        [Test]
        public async Task CreateUser_Test()
        {
            var user = await UserAccessModule.ExecuteQueryAsync(new GetCurrentUserQuery());
            user.Should().BeNull();

            // CreateNewUser

            // Check GetCurrentUserQuery again
        }
    }
}