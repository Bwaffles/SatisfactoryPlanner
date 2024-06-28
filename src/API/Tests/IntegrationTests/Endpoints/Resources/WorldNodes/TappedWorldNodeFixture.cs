using SatisfactoryPlanner.API.IntegrationTests.Endpoints.UserAccess.Users;
using SatisfactoryPlanner.API.IntegrationTests.Endpoints.Worlds;
using static SatisfactoryPlanner.Modules.Resources.Application.WorldNodes.GetWorldNodeDetails.WorldNodeDetailsResult;

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
        public async Task<Settings> Create(string resourceName = "Iron Ore", string extractorName = "Miner Mk.1")
        {
            _settings.WorldId = await new WorldFixture(_client).CreateEmptyWorld();

            _settings.NodeId = (await new WorldNodeFixture(_client)
                    .FindWorldNode(_settings.WorldId, worldNode => worldNode.ResourceName == resourceName))
                    .Id;

            _settings.AvailableExtractors = (await GetWorldNodeDetails.GetResult(_client, _settings.WorldId, _settings.NodeId))
                .Details.AvailableExtractors.ToList();

            _settings.CurrentExtractor = _settings.AvailableExtractors
                .First(nodeDetail => nodeDetail.Name == extractorName);

            await TapWorldNode.Execute(_client, _settings.WorldId, _settings.NodeId, _settings.CurrentExtractor.Id);

            return _settings;
        }

        public class Settings
        {
            public Guid WorldId { get; set; }

            public Guid NodeId { get; set; }

            public AvailableExtractor CurrentExtractor { get; set; } = null!;

            public List<AvailableExtractor> AvailableExtractors { get; set; } = null!;

            public void Deconstruct(out Guid worldId, out Guid nodeId)
            {
                worldId = WorldId;
                nodeId = NodeId;
            }
        }
    }
}