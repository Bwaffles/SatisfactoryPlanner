using DatabaseMigrator.Scripts.Generators._0005_update_resource_nodes_v1._1;
using Newtonsoft.Json;
using static DatabaseMigrator.Scripts.Generators._0005_update_resource_nodes_v1._1.UpdateResourceNodesScriptGenerator;

namespace DatabaseMigrator.UnitTests
{
    public class Tests
    {
        [Test]
        public void Test1()
        {
            var sut = new UpdateResourceNodesScriptGenerator();
            sut.Generate("nodes");

            Assert.Pass();
        }

        [Test]
        public void Test2()
        {
            var newNodesText = File.ReadAllText(@"C:\Users\Thana\source\repos\SatisfactoryPlanner\src\Database\DatabaseMigrator\Scripts\Generators\0005_UpdateResourceNodes_v1.1/new_nodes.json");
            var newNodes = JsonConvert.DeserializeObject<List<UpdatedResourceNode>>(newNodesText)!;

            Assert.IsTrue(newNodes.All(node => !string.IsNullOrWhiteSpace(node.Biome)));
            Assert.IsTrue(newNodes.All(node => node.Number > 0));

            var biomeGroups = newNodes
                .GroupBy(node => node.ItemCode + node.Biome);
            foreach (var biomeGroup in biomeGroups)
            {
                var a = biomeGroup.Count();
                Assert.AreEqual(biomeGroup.Count(), biomeGroup.DistinctBy(_ => _.Number).Count(), $"for {biomeGroup.Key}");
            }
        }
    }
}