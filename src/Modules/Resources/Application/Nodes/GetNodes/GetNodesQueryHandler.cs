using Dapper;
using SatisfactoryPlanner.BuildingBlocks.Application.Data;
using SatisfactoryPlanner.Modules.Resources.Application.Configuration.Queries;
using SatisfactoryPlanner.Modules.Resources.Application.Extractors;
using SatisfactoryPlanner.Modules.Resources.Domain;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SatisfactoryPlanner.Modules.Resources.Application.Nodes.GetNodes
{
    // ReSharper disable once UnusedMember.Global
    internal class GetNodesQueryHandler : IQueryHandler<GetNodesQuery, List<NodeDto>>
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;

        public GetNodesQueryHandler(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        public async Task<List<NodeDto>> Handle(GetNodesQuery query, CancellationToken cancellationToken)
        {
            var connection = _dbConnectionFactory.GetOpenConnection();

            const string sql = $"    SELECT node.id AS {nameof(NodeDto.Id)}" +
                               $"         , resource.id AS {nameof(NodeDto.ResourceId)}" +
                               $"         , resource.name AS {nameof(NodeDto.ResourceName)}" +
                               $"         , node.purity AS {nameof(NodeDto.Purity)}" +
                               $"         , node.biome AS {nameof(NodeDto.Biome)}" +
                               $"         , node.number AS {nameof(NodeDto.Number)}" +
                               $"         , node.map_position_x AS {nameof(NodeDto.MapPositionX)}" +
                               $"         , node.map_position_y AS {nameof(NodeDto.MapPositionY)}" +
                               $"         , node.map_position_z AS {nameof(NodeDto.MapPositionZ)}" +
                               "         , (SELECT exists(SELECT 1" +
                               "                            FROM resources.tapped_nodes AS tapped_node" +
                               "                           WHERE tapped_node.world_id = @worldId" +
                               $"                             AND tapped_node.node_id = node.id)) AS {nameof(NodeDto.IsTapped)}" +
                               "         , (SELECT tapped_node.extraction_rate " +
                               "              FROM resources.tapped_nodes AS tapped_node" +
                               "              WHERE tapped_node.world_id = @worldId" +
                               $"                AND tapped_node.node_id = node.id) AS {nameof(NodeDto.ExtractionRate)}" +
                               "       FROM resources.nodes AS node " +
                               " INNER JOIN resources.resources AS resource ON resource.id = node.resource_id " +
                               "      WHERE (@resourceId is null or node.resource_id = @resourceId) " +
                               "   ORDER BY resource.resource_sink_points, node.biome, node.number";

            var param = new
            {
                query.ResourceId, query.WorldId
            };

            var nodes = (await connection.QueryAsync<NodeDto>(sql, param)).ToList();
            foreach (var node in nodes)
                node.MaxExtractionRate = await GetMaxExtractionRate(connection, node);

            return nodes;
        }

        private static async Task<decimal> GetMaxExtractionRate(IDbConnection connection, NodeDto node)
        {
            var extractor = await ExtractorFactory.GetFastestExtractor(connection, node.ResourceId);
            var nodeModel = await NodeFactory.GetNode(connection, node.Id);
            return ResourceExtractionCalculator.GetMaxExtractionRate(extractor, nodeModel);
        }
    }
}