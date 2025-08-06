using FluentAssertions.Execution;
using SatisfactoryPlanner.API.Endpoints.Production.ProcessedItems;
using SatisfactoryPlanner.API.IntegrationTests.Endpoints.UserAccess.Users;
using System.Net;

namespace SatisfactoryPlanner.API.IntegrationTests.Endpoints.Production.ProcessedItems
{
    public static class GetRecipeDetails
    {
        public static async Task<HttpResponseMessage> Execute(HttpClient client, string recipeId)
            => await client.GetAsync($"api/processed-items/recipes/{recipeId}");

        [TestFixture]
        public class Tests : IntegrationTest
        {
            [Test]
            public async Task HappyPath()
            {
                // Need a user so we pass the permissions check... nearly every endpoint test will need this
                // TODO should we add a test for each endpoint that it has the right authorization? Maybe we can check its attributes?
                await CreateCurrentUser.Execute(Client);

                var response = await Execute(Client, "PureIronIngot");

                using (new AssertionScope())
                {
                    response.Should().HaveStatusCode(HttpStatusCode.OK);

                    var responseContent = (await response.ReadContentAsync<GetRecipeDetailsResponse>())!;
                    var data = responseContent.Data;
                    data.Should().NotBeNull();
                }
            }
        }
    }
}