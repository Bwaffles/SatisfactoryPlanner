using SatisfactoryPlanner.API.Modules.UserAccess.Users;
using SatisfactoryPlanner.Modules.UserAccess.Application.Users.GetCurrentUser;
using System.Net;

namespace SatisfactoryPlanner.API.IntegrationTests.Endpoints.UserAccess.Users
{
    public static class GetCurrentUser
    {
        public static async Task<HttpResponseMessage> Execute(HttpClient client, string auth0UserId)
        {
            // Since this endpoint gets the current authenticated user we need to manipulate this value before making the call
            AuthenticatedUser.Auth0UserId = auth0UserId;

            return await client.GetAsync("/api/user-access/users/@me");
        }

        [TestFixture]
        public class Tests : IntegrationTest
        {
            [Test]
            public async Task UserDoesNotExist()
            {
                var response = await Execute(Client, "GetCurrentUserTests.UserDoesNotExist");
                response.Should().HaveStatusCode(HttpStatusCode.NoContent);
            }

            [Test]
            public async Task UserFound()
            {
                const string auth0UserId = "GetCurrentUserTests.UserFound";

                var createCurrentUserResponse = await CreateCurrentUser.Execute(Client, new CreateCurrentUserRequest
                {
                    Auth0UserId = auth0UserId,
                });

                var createCurrentUserResponseContent = (await createCurrentUserResponse.ReadContentAsync<CreateCurrentUserResponse>())!;
                var userId = createCurrentUserResponseContent.UserId;

                var getCurrentUserResponse = await Execute(Client, auth0UserId);
                getCurrentUserResponse.Should().HaveStatusCode(HttpStatusCode.OK);

                var responseContent = await getCurrentUserResponse.ReadContentAsync<CurrentUserDto>();
                responseContent.Auth0UserId.Should().Be(auth0UserId);
                responseContent.Id.Should().Be(userId);
                responseContent.Roles.Should().NotBeEmpty();
            }
        }
    }
}