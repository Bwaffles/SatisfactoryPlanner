using SatisfactoryPlanner.Modules.Warehouses.Domain.ItemSources;

namespace SatisfactoryPlanner.Modules.Warehouses.UnitTests.ItemSources
{
    public class ItemSourceFixture
    {
        public ItemSource CreateForNode()
        {
            var worldId = new WorldId(Guid.NewGuid());
            var source = Source.Node(new SourceId(Guid.NewGuid()), "Name of the resource node");
            return ItemSource.Register(worldId, source);
        }
    }
}
