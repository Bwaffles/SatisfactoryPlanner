using FluentAssertions.Execution;
using SatisfactoryPlanner.API.IntegrationTests.Endpoints.UserAccess.Users;
using SatisfactoryPlanner.BuildingBlocks.IntegrationTests.Probing;
using SatisfactoryPlanner.Modules.Worlds.Application.Worlds.GetCurrentPioneerWorlds;
using System.Net;

namespace SatisfactoryPlanner.API.IntegrationTests.Endpoints.Production.ProcessedItems
{
    public static class GetCurrentPioneerWorlds
    {
        public static async Task<HttpResponseMessage> Execute(HttpClient client)
            => await client.GetAsync("api/worlds/worlds/@me");

        [TestFixture]
        public class Tests : IntegrationTest
        {
            [Test]
            public async Task HappyPath()
            {
                // Need a user so we pass the permissions check... nearly every endpoint test will need this
                // TODO should we add a test for each endpoint that it has the right authorization? Maybe we can check its attributes?
                await CreateCurrentUser.Execute(Client);

                var response = await GetEventually(new Probe(Client), 10000);

                using (new AssertionScope())
                {
                    response.Should().HaveStatusCode(HttpStatusCode.OK);

                    var responseContent = (await response.ReadContentAsync<List<PioneerWorldDto>>())!;
                    responseContent.Should().HaveCount(1);

                    var starterWorld = responseContent[0];
                    starterWorld.Id.Should().NotBeEmpty();
                    starterWorld.Name.Should().NotBeNullOrEmpty();
                }
            }
        }

        public class Probe(HttpClient client) : IProbe<HttpResponseMessage>
        {
            private readonly HttpClient _client = client;

            public async Task<bool> IsSatisfiedAsync(HttpResponseMessage sample)
            {
                if (sample?.IsSuccessStatusCode != true)
                    return false;

                var content = (await sample.ReadContentAsync<List<PioneerWorldDto>>())!;
                return content.Count != 0;
            }

            public async Task<HttpResponseMessage> GetSampleAsync()
            {
                return await Execute(_client);
            }

            public string DescribeFailureTo() => "Cannot get current pioneer worlds.";
        }
    }
}