using SatisfactoryPlanner.API.Modules.UserAccess.Users;
using System.Net;
using System.Net.Http.Json;

namespace SatisfactoryPlanner.API.IntegrationTests.Endpoints.UserAccess.Users
{
    public static class CreateCurrentUser
    {
        public static async Task<HttpResponseMessage> Execute(HttpClient client, CreateCurrentUserRequest createCurrentUserRequest)
            => await client.PostAsJsonAsync("/api/user-access/users/@me", createCurrentUserRequest);

        public static async Task<HttpResponseMessage> Execute(HttpClient client)
            => await client.PostAsJsonAsync("/api/user-access/users/@me", new CreateCurrentUserRequest { Auth0UserId = AuthenticatedUser.Auth0UserId });

        [TestFixture]
        public class Tests : IntegrationTest
        {
            [Test]
            public async Task UserCreatedSuccessfully()
            {
                var response = await Execute(Client, new CreateCurrentUserRequest
                {
                    Auth0UserId = "new-integration-test-user", // I think if the endpoint is @me it should get the auth0 user id from the access token or else you could create users other than the one making the call
                });

                response.Should().HaveStatusCode(HttpStatusCode.Created);

                var responseContent = (await response.Content.ReadFromJsonAsync<CreateCurrentUserResponse>())!;
                responseContent.UserId.Should().NotBeEmpty();
            }
        }
    }
}