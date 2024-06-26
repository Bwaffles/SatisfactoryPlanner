using FluentAssertions;
using SatisfactoryPlanner.Modules.Warehouses.Domain.ItemSources;

namespace SatisfactoryPlanner.Modules.Warehouses.UnitTests.ItemSources
{
    public class SourceTests : UnitTest
    {
        [Test]
        public void CanCreateNodeSource()
        {
            var sourceId = new SourceId(Guid.NewGuid());
            var source = Source.Node(sourceId, "Iron Ore - Grassy Fields 1");

            AssertAll(() =>
            {
                source.Id.Should().Be(sourceId);
                source.Name.Should().Be("Iron Ore - Grassy Fields 1");
                source.Type.Should().Be(SourceType.Node);
            });
        }
    }
}
