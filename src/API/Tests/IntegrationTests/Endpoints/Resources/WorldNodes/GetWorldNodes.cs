using FluentAssertions.Execution;
using Microsoft.AspNetCore.Http.Extensions;
using SatisfactoryPlanner.API.IntegrationTests.Endpoints.Production.ProcessedItems;
using SatisfactoryPlanner.API.Modules.Resources.WorldNodes;
using SatisfactoryPlanner.BuildingBlocks.IntegrationTests.Probing;
using SatisfactoryPlanner.Modules.Worlds.Application.Worlds.GetCurrentPioneerWorlds;
using System.Net;

namespace SatisfactoryPlanner.API.IntegrationTests.Endpoints.UserAccess.Users
{
    public static class GetWorldNodes
    {
        public static async Task<HttpResponseMessage> Execute(HttpClient client, Guid worldId, Guid? resourceId = null)
        {
            var queryBuilder = new QueryBuilder();
            if (resourceId.HasValue)
                queryBuilder.Add("resourceId", resourceId.Value.ToString());

            return await client.GetAsync(new UriBuilder
            {
                Path = $"api/worlds/{worldId}/nodes",
                Query = queryBuilder.ToString()
            }.Uri);
        }

        [TestFixture]
        public class Tests : IntegrationTest
        {
            [Test]
            public async Task HappyPath()
            {
                // Need a user so we pass the permissions check... nearly every endpoint test will need this
                // TODO should we add a test for each endpoint that it has the right authorization? Maybe we can check its attributes?
                await CreateCurrentUser.Execute(Client);
                var currentPioneerWorlds = (await GetEventually(new GetCurrentPioneerWorlds.Probe(Client), 10000))!;
                var worldId = (await currentPioneerWorlds.ReadContentAsync<List<PioneerWorldDto>>())[0].Id;

                var response = await GetEventually(new Probe(Client, worldId), 10000);

                using (new AssertionScope())
                {
                    response.Should().HaveStatusCode(HttpStatusCode.OK);

                    var responseContent = (await response.ReadContentAsync<GetWorldNodesResponse>())!;
                    responseContent.Data.Should().NotBeNull();
                }
            }
        }

        public class Probe(HttpClient client, Guid worldId, Guid? resourceId = null) : IProbe<HttpResponseMessage>
        {
            private readonly HttpClient _client = client;
            private readonly Guid _worldId = worldId;
            private readonly Guid? _resourceId = resourceId;

            public async Task<bool> IsSatisfiedAsync(HttpResponseMessage sample)
            {
                if (sample?.IsSuccessStatusCode != true)
                    return false;

                var content = (await sample.ReadContentAsync<GetWorldNodesResponse>())!;
                return content.Data.WorldNodes.Count != 0;
            }

            public async Task<HttpResponseMessage> GetSampleAsync()
            {
                return await Execute(_client, _worldId, _resourceId);
            }

            public string DescribeFailureTo() => "Cannot get world nodes.";
        }
    }
}