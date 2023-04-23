//using SatisfactoryPlanner.Modules.Resources.Application.Contracts;
//using SatisfactoryPlanner.Modules.Resources.Application.Nodes.GetNodeDetails;
//using SatisfactoryPlanner.Modules.Resources.Application.Nodes.GetNodes;
//using SatisfactoryPlanner.Modules.Resources.Application.Nodes.TapNode;
//using SatisfactoryPlanner.Modules.Resources.Application.Resources.GetResources;

//namespace SatisfactoryPlanner.Modules.Resources.IntegrationTests.TappedNodes
//{
//    public class TappedNodeFixture
//    {
//        private readonly Settings _settings = new();

//        public TappedNodeFixture WithWorldId(Guid worldId)
//        {
//            _settings.WorldId = worldId;
//            return this;
//        }

//        /// <summary>
//        ///     Create a new tapped node.
//        /// </summary>
//        /// <returns>Returns the settings that were used to create the tapped node.</returns>
//        public async Task<Settings> CreateTappedNode(IResourcesModule resourcesModule)
//        {
//            var bauxite = (await resourcesModule.ExecuteQueryAsync(new GetResourcesQuery(_settings.WorldId)))
//                .First(resource => resource.Name == "Bauxite");

//            _settings.NodeId =
//                (await resourcesModule.ExecuteQueryAsync(new GetNodesQuery(_settings.WorldId, bauxite.Id))).First().Id;

//            var nodeDetails =
//                await resourcesModule.ExecuteQueryAsync(new GetNodeDetailsQuery(_settings.WorldId, _settings.NodeId));
//            var extractor = nodeDetails.AvailableExtractors
//                .First(nodeDetail => nodeDetail.Name == "Miner Mk.1");
//            _settings.TappedNodeId =
//                await resourcesModule.ExecuteCommandAsync(new TapNodeCommand(_settings.WorldId, _settings.NodeId,
//                    extractor.Id));

//            return _settings;
//        }

//        public class Settings
//        {
//            public Guid WorldId { get; set; } = Guid.NewGuid();

//            public Guid TappedNodeId { get; set; }

//            public Guid NodeId { get; set; }

//            public void Deconstruct(out Guid worldId, out Guid nodeId, out Guid tappedNodeId)
//            {
//                worldId = WorldId;
//                nodeId = NodeId;
//                tappedNodeId = TappedNodeId;
//            }
//        }
//    }
//}