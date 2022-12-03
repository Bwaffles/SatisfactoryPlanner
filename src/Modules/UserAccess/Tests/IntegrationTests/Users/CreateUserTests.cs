using Npgsql;
using SatisfactoryPlanner.Modules.UserAccess.Application.Users.CreateCurrentUser;
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
            ExecutionContext.UserId = Guid.Empty;

            var user = await UserAccessModule.ExecuteQueryAsync(new GetCurrentUserQuery());
            user.Should().BeNull();

            var newUserId = await UserAccessModule.ExecuteCommandAsync(new CreateCurrentUserCommand(
                "myAuth0UserId"));

            ExecutionContext.UserId = newUserId;

            user = await UserAccessModule.ExecuteQueryAsync(new GetCurrentUserQuery());

            user.Should().NotBeNull();
            user!.Id.Should().Be(newUserId);
            user.Auth0UserId.Should().Be("myAuth0UserId");

            var connection = new NpgsqlConnection(ConnectionString);
            var messagesList = await OutboxMessagesHelper.GetOutboxMessages(connection);

            messagesList.Count.Should().Be(1);
        }
    }
}