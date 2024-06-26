using SatisfactoryPlanner.API.IntegrationTests.Endpoints.UserAccess.Users;
using SatisfactoryPlanner.API.IntegrationTests.Endpoints.Worlds;

namespace SatisfactoryPlanner.API.IntegrationTests.Endpoints.Resources.WorldNodes
{
    public class TappedWorldNodeFixture(HttpClient client)
    {
        private readonly HttpClient _client = client;
        private readonly Settings _settings = new();

        /// <summary>
        /// Create a new tapped node.
        /// </summary>
        /// <returns>The settings that were used to create the tapped node.</returns>
        public async Task<Settings> Create(string extractorName = "Miner Mk.1")
        {
            _settings.WorldId = await new WorldFixture(_client).CreateEmptyWorld();

            _settings.NodeId = (await new WorldNodeFixture(_client)
                    .FindWorldNode(_settings.WorldId, worldNode => worldNode.ResourceName == "Iron Ore"))
                    .Id;

            _settings.ExtractorId = (await GetWorldNodeDetails
                .GetResult(_client, _settings.WorldId, _settings.NodeId))
                .Details.AvailableExtractors
                .First(nodeDetail => nodeDetail.Name == extractorName).Id;

            await TapWorldNode.Execute(_client, _settings.WorldId, _settings.NodeId, _settings.ExtractorId);

            return _settings;
        }

        public class Settings
        {
            public Guid WorldId { get; set; }

            public Guid NodeId { get; set; }

            public Guid ExtractorId { get; set; }

            public void Deconstruct(out Guid worldId, out Guid nodeId)
            {
                worldId = WorldId;
                nodeId = NodeId;
            }

            public void Deconstruct(out Guid worldId, out Guid nodeId, out Guid extractorId)
            {
                worldId = WorldId;
                nodeId = NodeId;
                extractorId = ExtractorId;
            }
        }
    }
}