using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DatabaseMigrator.Scripts.Generators._0005_update_resource_nodes_v1._1
{
    public class UpdateResourceNodesScriptGenerator
    {
        private static string FolderPath = $"{Scripts.GeneratorsPath}/0005_UpdateResourceNodes_v1_1";
        private string _table;

        public void Generate(string table)
        {
            _table = table;

            // These nodes were downloaded from the satisfactory interactive map game version 1.1
            var updatedNodesText = File.ReadAllText($"{FolderPath}/nodes_v1.1.json");
            var updatedNodesRaw = JsonConvert.DeserializeObject<List<Option>>(updatedNodesText);
            var updatedNodes = new List<UpdatedResourceNode>();
            foreach (var nodesByResource in updatedNodesRaw)
            {
                foreach (var nodesByPurity in nodesByResource.options)
                {
                    foreach (var node in nodesByPurity.markers)
                    {
                        updatedNodes.Add(new UpdatedResourceNode
                        {
                            ItemCode = nodesByResource.type,
                            InstanceName = node.pathName,
                            Purity = node.purity,
                            MapPositionX = node.x,
                            MapPositionY = node.y,
                            MapPositionZ = node.z
                        });
                    }
                }
            }

            var existingNodesText = File.ReadAllText($"{FolderPath}/existing_nodes.json");
            var existingNodes = JsonConvert.DeserializeObject<List<ExistingResourceNode>>(existingNodesText)!
                .OrderBy(a => a.ResourceId)
                .ThenBy(a => a.Purity)
                .ToList();

            var script = "";
            script = GenerateInsertNodesScript(script);
            script = GenerateDeleteNodesScript(existingNodes, updatedNodes, script);
            script = GenerateUpdateNodesScript(existingNodes, updatedNodes, script);

            File.WriteAllText(Scripts.UpdateResourceNodes1_1, script);
        }

        /// <summary>
        ///     Generate scripts to insert new nodes added in update 1.1.
        /// </summary>
        private string GenerateInsertNodesScript(string script)
        { // reading new nodes from a file because I had to manually update the file with the biome and number
            var newNodesText = File.ReadAllText($"{FolderPath}/new_nodes.json");
            var newNodes = JsonConvert.DeserializeObject<List<UpdatedResourceNode>>(newNodesText)!;

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
                                         "number, " +
                                         "map_position_x, " +
                                         "map_position_y, " +
                                         "map_position_z, " +
                                         "instance_name, " +
                                         "resource_id) ");
                scriptBuilder.AppendLine("SELECT " +
                                         "gen_random_uuid (), " +
                                         $"'{item.Purity}', " +
                                         $"'{item.Biome}', " +
                                         $"'{item.Number}', " +
                                         $"{item.MapPositionX}, " +
                                         $"{item.MapPositionY}, " +
                                         $"{item.MapPositionZ}, " +
                                         $"'{item.InstanceName}', " +
                                         "resource.id ");
                scriptBuilder.AppendLine($"  FROM resources.resources AS resource " +
                                         $" WHERE resource.code = '{item.ItemCode}';");
                script += scriptBuilder.ToString();
            }

            return script;
        }

        /// <summary>
        ///     Generate scripts to delete removed nodes from update 1.1.
        /// </summary>
        private string GenerateDeleteNodesScript(List<ExistingResourceNode> existingNodes, List<UpdatedResourceNode> updatedNodes,
            string script)
        {
            var deletedNodes = existingNodes
                .Where(existingNode => updatedNodes.All(updatedNode => updatedNode.InstanceName != existingNode.InstanceName));

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
        ///     Generate scripts to update the co-ordinates of nodes that were moved in update 1.1.
        /// </summary>
        private string GenerateUpdateNodesScript(List<ExistingResourceNode> existingNodes, List<UpdatedResourceNode> updatedNodes,
            string script)
        {
            script += "-- Nodes with changed purity" + Environment.NewLine;

            var purityChangedNodes = updatedNodes
                .Where(update1_1Node => existingNodes
                    .Any(existingNode => update1_1Node.InstanceName == existingNode.InstanceName && update1_1Node.Purity != existingNode.Purity))
                .OrderBy(existingNode => existingNode.ItemCode);

            foreach (var node in purityChangedNodes)
            {
                var scriptBuilder = new StringBuilder();
                scriptBuilder.AppendLine($"UPDATE resources.{_table} " + 
                                         $"   SET purity = '{node.Purity}'" +
                                         $" WHERE instance_name = '{node.InstanceName}';");
                script += scriptBuilder.ToString();
            }

            const int CordinateCutoff = 500;
            // If the coordinates changed significantly then it might be in a different biome, and I'd need to adjust that and numbering manually
            script += "-- Nodes with minor location change" + Environment.NewLine;

            var minorLocationChangedNodes = updatedNodes
                .Where(update1_1Node => existingNodes
                    .Any(existingNode => update1_1Node.InstanceName == existingNode.InstanceName && 
                                         (Math.Abs(update1_1Node.MapPositionX - existingNode.MapPositionX) < CordinateCutoff ||
                                         Math.Abs(update1_1Node.MapPositionY - existingNode.MapPositionY) < CordinateCutoff ||
                                         Math.Abs(update1_1Node.MapPositionZ - existingNode.MapPositionZ) < CordinateCutoff)))
                .OrderBy(existingNode => existingNode.ItemCode);


            foreach (var node in minorLocationChangedNodes)
            {
                var scriptBuilder = new StringBuilder();
                scriptBuilder.AppendLine($"UPDATE resources.{_table} ");
                scriptBuilder.AppendLine($"   SET map_position_x = {node.MapPositionX}" +
                                         $"     , map_position_y = {node.MapPositionY}" +
                                         $"     , map_position_z = {node.MapPositionZ}");
                scriptBuilder.AppendLine($" WHERE instance_name = '{node.InstanceName}';");
                script += scriptBuilder.ToString();
            }
            script += "-- Nodes with major location change" + Environment.NewLine;

            var majorLocationChangedNodes = updatedNodes
                .Where(update1_1Node => existingNodes
                    .Any(existingNode => update1_1Node.InstanceName == existingNode.InstanceName &&
                                         (Math.Abs(update1_1Node.MapPositionX - existingNode.MapPositionX) >= CordinateCutoff ||
                                         Math.Abs(update1_1Node.MapPositionY - existingNode.MapPositionY) >= CordinateCutoff ||
                                         Math.Abs(update1_1Node.MapPositionZ - existingNode.MapPositionZ) >= CordinateCutoff)))
                .OrderBy(existingNode => existingNode.ItemCode);


            foreach (var node in majorLocationChangedNodes)
            {
                var scriptBuilder = new StringBuilder();
                scriptBuilder.AppendLine($"UPDATE resources.{_table} ");
                scriptBuilder.AppendLine($"   SET map_position_x = {node.MapPositionX}" +
                                         $"     , map_position_y = {node.MapPositionY}" +
                                         $"     , map_position_z = {node.MapPositionZ}");
                scriptBuilder.AppendLine($" WHERE instance_name = '{node.InstanceName}';");
                script += scriptBuilder.ToString();
            }

            script += "-- Custom biome change" + Environment.NewLine;
            script += "UPDATE resources.nodes SET biome = 'Western Dune Forest' WHERE instance_name = 'Persistent_Level:PersistentLevel.BP_ResourceNode71_736';" + Environment.NewLine;

            script += "-- Custom re-numbering of existing nodes" + Environment.NewLine;
            script += "UPDATE resources.nodes SET number = 2 WHERE instance_name = 'Persistent_Level:PersistentLevel.BP_ResourceNode150';" + Environment.NewLine;

            script += "UPDATE resources.nodes SET number = 2 WHERE instance_name = 'Persistent_Level:PersistentLevel.BP_ResourceNode121_4877';" + Environment.NewLine;

            script += "UPDATE resources.nodes SET number = 5 WHERE instance_name = 'Persistent_Level:PersistentLevel.BP_ResourceNode122';" + Environment.NewLine;
            script += "UPDATE resources.nodes SET number = 6 WHERE instance_name = 'Persistent_Level:PersistentLevel.BP_ResourceNode7_380';" + Environment.NewLine;
            script += "UPDATE resources.nodes SET number = 7 WHERE instance_name = 'Persistent_Level:PersistentLevel.BP_ResourceNode5_381';" + Environment.NewLine;
            script += "UPDATE resources.nodes SET number = 8 WHERE instance_name = 'Persistent_Level:PersistentLevel.BP_ResourceNode6_379';" + Environment.NewLine;

            script += "UPDATE resources.nodes SET number = 3 WHERE instance_name = 'Persistent_Level:PersistentLevel.BP_ResourceNode504';" + Environment.NewLine;
            script += "UPDATE resources.nodes SET number = 4 WHERE instance_name = 'Persistent_Level:PersistentLevel.BP_ResourceNode503';" + Environment.NewLine;
            script += "UPDATE resources.nodes SET number = 5 WHERE instance_name = 'Persistent_Level:PersistentLevel.BP_ResourceNode559';" + Environment.NewLine;
            script += "UPDATE resources.nodes SET number = 6 WHERE instance_name = 'Persistent_Level:PersistentLevel.BP_ResourceNode560';" + Environment.NewLine;

            script += "UPDATE resources.nodes SET number = 3 WHERE instance_name = 'Persistent_Level:PersistentLevel.BP_ResourceNode588';" + Environment.NewLine;

            return script;
        }

        //private static void GenerateNewNodesFile(List<ExistingResourceNode> existingNodes, List<UpdatedResourceNode> updatedNodes)
        //{ // what I used to generate the initial new nodes file
        //    var newNodes = updatedNodes
        //        .Where(updatedNode => existingNodes.All(existingNode => existingNode.InstanceName != updatedNode.InstanceName));

        //    File.WriteAllText($"{FolderPath}/new_nodes.json", JsonConvert.SerializeObject(newNodes));
        //}

        public class ExistingResourceNode
        {
            public Guid Id { get; set; }
            public string Purity { get; set; }
            public string Biome { get; set; }

            [JsonProperty(PropertyName = "map_position_x")]
            public double MapPositionX { get; set; }

            [JsonProperty(PropertyName = "map_position_y")]
            public double MapPositionY { get; set; }

            [JsonProperty(PropertyName = "map_position_z")]
            public double MapPositionZ { get; set; }

            [JsonProperty(PropertyName = "intance_name")]
            public string InstanceName { get; set; }

            [JsonProperty(PropertyName = "resource_id")]
            public Guid ResourceId { get; set; }
            public int Number { get; set; }
        }

        public class UpdatedResourceNode
        {
            public string ItemCode { get; set; }
            public string InstanceName { get; set; }
            public double MapPositionX { get; set; }
            public double MapPositionY { get; set; }
            public double MapPositionZ { get; set; }
            public string Purity { get; set; }

            public string Biome { get; set; }
            public int Number { get; set; }
        }

        public class Marker
        {
            public string pathName { get; set; }
            public double x { get; set; }
            public double y { get; set; }
            public double z { get; set; }
            public string type { get; set; }
            public string purity { get; set; }
            public bool obstructed { get; set; }
            public string lastCheck { get; set; }
        }

        public class Option
        {
            public string name { get; set; }
            public string type { get; set; }
            public List<Option> options { get; set; }
            public string layerId { get; set; }
            public string purity { get; set; }
            public string outsideColor { get; set; }
            public string insideColor { get; set; }
            public string icon { get; set; }
            public List<Marker> markers { get; set; }
        }


    }
}
