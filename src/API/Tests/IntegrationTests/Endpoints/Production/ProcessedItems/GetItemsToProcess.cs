using SatisfactoryPlanner.API.Endpoints.Production.ProcessedItems;
using SatisfactoryPlanner.API.IntegrationTests.Endpoints.UserAccess.Users;
using System.Net;
using System.Net.Http.Json;

namespace SatisfactoryPlanner.API.IntegrationTests.Endpoints.Production.ProcessedItems
{
    public static class GetItemsToProcess
    {
        public static async Task<HttpResponseMessage> Execute(HttpClient client)
            => await client.GetAsync("api/processed-items/items-to-process");

        [TestFixture]
        public class Tests : IntegrationTest
        {
            [Test]
            public async Task CanGetItemsSuccessfully()
            {
                // Need a user so we pass the permissions check... nearly every endpoint test will need this
                // TODO should we add a test for each endpoint that it has the right authorization? Maybe we can check its attributes?
                await CreateCurrentUser.Execute(Client);

                var response = await Execute(Client);

                response.Should().HaveStatusCode(HttpStatusCode.OK);

                var responseContent = (await response.Content.ReadFromJsonAsync<GetItemsToProcessReponse>())!;
                var items = responseContent.Items;
                items.Should().NotBeEmpty();
            }
        }
    }
}