using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Text;

namespace DatabaseMigrator.Scripts.Generators._0004_UpdateResourceNodes
{
    internal class UpdateResourceNodesScriptGenerator
    {
        private string _table;

        public void Generate(string table)
        {
            _table = table;

            // These data files were generated from parsing a save file.
            var update7NodesText =
                File.ReadAllText(
                    $"{Scripts.GeneratorsPath}/0004_UpdateResourceNodes/update_7_resource_nodes_data.json");
            var update7Nodes = JsonConvert.DeserializeObject<List<ResourceNode>>(update7NodesText)!
                .OrderBy(a => a.ItemCode)
                .ThenBy(a => a.Purity)
                .ToList();

            var existingNodesText =
                File.ReadAllText($"{Scripts.GeneratorsPath}/0004_UpdateResourceNodes/resource_nodes_data.json");
            var existingNodes = JsonConvert.DeserializeObject<List<ResourceNode>>(existingNodesText)!
                .OrderBy(a => a.ItemCode)
                .ThenBy(a => a.Purity)
                .ToList();

            var script = "";
            script = GenerateInsertNodesScript(script);
            script = GenerateDeleteNodesScript(existingNodes, update7Nodes, script);
            script = GenerateUpdateNodesScript(existingNodes, update7Nodes, script);

            File.WriteAllText(Scripts.UpdateResourceNodes, script);
        }

        /// <summary>
        ///     Generate scripts to update the co-ordinates of nodes that were moved in update 7.
        /// </summary>
        private string GenerateUpdateNodesScript(List<ResourceNode> existingNodes, List<ResourceNode> update7Nodes,
            string script)
        {
            var changedNodes = update7Nodes
                .Where(update7Node => existingNodes
                    .Any(node => update7Node.InstanceName == node.InstanceName &&
                                 (update7Node.Position.X != node.Position.X ||
                                  update7Node.Position.Y != node.Position.Y ||
                                  update7Node.Position.Z != node.Position.Z)
                    )
                );

            foreach (var node in changedNodes)
            {
                var scriptBuilder = new StringBuilder();
                scriptBuilder.AppendLine($"UPDATE resources.{_table} ");
                scriptBuilder.AppendLine($"   SET map_position_x = {node.Position.X}" +
                                         $"     , map_position_y = {node.Position.Y}" +
                                         $"     , map_position_z = {node.Position.Z}");
                scriptBuilder.AppendLine($" WHERE instance_name = '{node.InstanceName}';");
                script += scriptBuilder.ToString();
            }

            return script;
        }
        
        /// <summary>
        ///     Generate scripts to delete removed nodes from update 7.
        /// </summary>
        private string GenerateDeleteNodesScript(List<ResourceNode> existingNodes, List<ResourceNode> update7Nodes,
            string script)
        {
            var deletedNodes = existingNodes
                .Where(node => update7Nodes.All(update7Node => update7Node.InstanceName != node.InstanceName));

            foreach (var node in deletedNodes)
            {
                var scriptBuilder = new StringBuilder();
                scriptBuilder.AppendLine($"DELETE FROM resources.{_table} " +
                                         $"WHERE instance_name = '{node.InstanceName}';");
                script += scriptBuilder.ToString();
            }

            return script;
        }

        /// <summary>
        ///     Generate scripts to insert new nodes added in update 7.
        /// </summary>
        private string GenerateInsertNodesScript(string script)
        { // reading new nodes from a file because I had to manually update the file with the purity, biome and item code
            var newNodesText = File.ReadAllText($"{Scripts.GeneratorsPath}/0004_UpdateResourceNodes/new_nodes.json");
            var newNodes = JsonConvert.DeserializeObject<List<ResourceNode>>(newNodesText)!;

            foreach (var item in newNodes
                         .OrderBy(_ => _.ItemCode)
                         .ThenBy(_ => _.Purity)
                         .ThenBy(_ => _.Biome))
            {
                var scriptBuilder = new StringBuilder();
                scriptBuilder.AppendLine($"INSERT INTO resources.{_table} (" +
                                         "id, " +
                                         "purity, " +
                                         "biome, " +
                                         "map_position_x, " +
                                         "map_position_y, " +
                                         "map_position_z, " +
                                         "instance_name, " +
                                         "resource_id) ");
                scriptBuilder.AppendLine("SELECT " +
                                         "gen_random_uuid (), " +
                                         $"'{item.Purity}', " +
                                         $"'{item.Biome}', " +
                                         $"{item.Position.X}, " +
                                         $"{item.Position.Y}, " +
                                         $"{item.Position.Z}, " +
                                         $"'{item.InstanceName}', " +
                                         "resource.id ");
                scriptBuilder.AppendLine($"  FROM resources.resources AS resource " +
                                         $" WHERE resource.code = '{item.ItemCode}';");
                script += scriptBuilder.ToString();
            }

            return script;
        }

        // ReSharper disable once UnusedMember.Local
        private static void GenerateNewNodesFile(List<ResourceNode> update7Nodes, List<ResourceNode> existingNodes)
        { // what I used to generate the initial new nodes file
            var newNodes = update7Nodes
                .Where(update7Node => existingNodes.All(node => node.InstanceName != update7Node.InstanceName));

            File.WriteAllText($"{Scripts.GeneratorsPath}/0004_UpdateResourceNodes/new_nodes.json",
                JsonConvert.SerializeObject(newNodes));
        }

        [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Local")]
        [SuppressMessage("ReSharper", "ClassNeverInstantiated.Local")]
        private class ResourceNode
        {
            public string ItemCode { get; set; }

            public string Purity { get; set; }

            public string Biome { get; set; }

            public string InstanceName { get; set; }

            public Position Position { get; set; }
        }

        [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Local")]
        [SuppressMessage("ReSharper", "ClassNeverInstantiated.Local")]
        private class Position
        {
            public decimal X { get; set; }

            public decimal Y { get; set; }

            public decimal Z { get; set; }
        }
    }
}