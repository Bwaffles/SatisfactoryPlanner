using SatisfactoryPlanner.Modules.Resources.Application.Contracts;
using SatisfactoryPlanner.Modules.Resources.Application.WorldNodes.GetWorldNodeDetails;
using SatisfactoryPlanner.Modules.Resources.Application.WorldNodes.GetWorldNodes;
using SatisfactoryPlanner.Modules.Resources.Application.WorldNodes.TapWorldNode;

namespace SatisfactoryPlanner.Modules.Resources.IntegrationTests.WorldsNodes
{
    public class TappedWorldNodeFixture
    {
        private readonly Settings _settings = new();

        /// <summary>
        ///     Create a new tapped node.
        /// </summary>
        /// <returns>Returns the settings that were used to create the tapped node.</returns>
        public async Task<Settings> Create(IResourcesModule resourcesModule)
        {
            _settings.WorldId = await new WorldFixture().Create(resourcesModule);

            _settings.NodeId =
                (await resourcesModule.ExecuteQueryAsync(new GetWorldNodesQuery(_settings.WorldId, null)))
                .First(node => node.ResourceName == "Bauxite").Id;

            var extractor =
                (await resourcesModule.ExecuteQueryAsync(new GetWorldNodeDetailsQuery(_settings.WorldId,
                    _settings.NodeId)))
                .AvailableExtractors.First(nodeDetail => nodeDetail.Name == "Miner Mk.1");

            await resourcesModule.ExecuteCommandAsync(new TapWorldNodeCommand(_settings.WorldId, _settings.NodeId,
                extractor.Id));

            return _settings;
        }

        public class Settings
        {
            public Guid WorldId { get; set; }

            public Guid NodeId { get; set; }

            public void Deconstruct(out Guid worldId, out Guid nodeId)
            {
                worldId = WorldId;
                nodeId = NodeId;
            }
        }
    }
}