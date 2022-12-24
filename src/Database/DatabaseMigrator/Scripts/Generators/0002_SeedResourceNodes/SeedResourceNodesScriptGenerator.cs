using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DatabaseMigrator.Scripts.Generators._0002_SeedResourceNodes
{
    internal class SeedResourceNodesScriptGenerator
    {
        public void Generate()
        {
            var fileContents =
                File.ReadAllText($"{Scripts.GeneratorsPath}/0002_SeedResourceNodes/resource_nodes_data.json");
            var nodes = JsonConvert.DeserializeObject<List<ResourceNode>>(fileContents);

            var insertScriptBuilder = new StringBuilder();
            insertScriptBuilder.AppendLine("INSERT INTO factories.resource_nodes (item_code, " +
                                           "purity, " +
                                           "biome, " +
                                           "map_position_x, " +
                                           "map_position_y, " +
                                           "map_position_z, " +
                                           "instance_name) ");
            insertScriptBuilder.AppendLine("VALUES ");

            foreach (var item in nodes
                         .Where(_ => _.ItemCode is not null &&
                                     _.ItemCode !=
                                     "Desc_SAM_C") // excluding S.A.M ore since there is no item in the game files yet
                         .OrderBy(_ => _.ItemCode)
                         .ThenBy(_ => _.Purity)
                         .ThenBy(_ => _.Biome))
                insertScriptBuilder.AppendLine($"('{item.ItemCode}', " +
                                               $"'{item.Purity}', " +
                                               $"'{item.Biome}', " +
                                               $"{item.Position.X}, " +
                                               $"{item.Position.Y}, " +
                                               $"{item.Position.Z}, " +
                                               $"'{item.InstanceName}'),");

            var insertScript = insertScriptBuilder.ToString()
                .TrimEnd()
                .TrimEnd(',');
            insertScript += ";";

            File.WriteAllText(Scripts.SeedResourcesNodes, insertScript);
        }

        private class ResourceNode
        {
            public string ItemCode { get; set; }

            public string Purity { get; set; }

            public string Biome { get; set; }

            public string InstanceName { get; set; }

            public Position Position { get; set; }
        }

        private class Position
        {
            public decimal X { get; set; }

            public decimal Y { get; set; }

            public decimal Z { get; set; }
        }
    }
}