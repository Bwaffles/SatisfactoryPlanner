using SatisfactoryPlanner.Modules.Resources.Application.Contracts;
using SatisfactoryPlanner.Modules.Resources.Application.WorldNodes.SpawnWorldNodes;

namespace SatisfactoryPlanner.Modules.Resources.IntegrationTests.WorldNodes
{
    internal class WorldFixture
    {
        public async Task<Guid> Create(IResourcesModule resourcesModule)
        {
            var worldId = Guid.NewGuid();

            await resourcesModule.ExecuteCommandAsync(new SpawnWorldNodesCommand(Guid.NewGuid(), worldId));

            return worldId;
        }
    }
}