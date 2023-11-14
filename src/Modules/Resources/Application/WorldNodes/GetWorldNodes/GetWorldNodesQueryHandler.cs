using Dapper;
using SatisfactoryPlanner.BuildingBlocks.Application.Data;
using SatisfactoryPlanner.Modules.Resources.Application.Configuration.Queries;
using SatisfactoryPlanner.Modules.Resources.Application.Extractors;
using SatisfactoryPlanner.Modules.Resources.Application.Nodes;
using SatisfactoryPlanner.Modules.Resources.Domain;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SatisfactoryPlanner.Modules.Resources.Application.WorldNodes.GetWorldNodes
{
    // ReSharper disable once UnusedMember.Global
    internal class GetWorldNodesQueryHandler : IQueryHandler<GetWorldNodesQuery, List<WorldNodeDto>>
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;

        public GetWorldNodesQueryHandler(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        public async Task<List<WorldNodeDto>> Handle(GetWorldNodesQuery query, CancellationToken cancellationToken)
        {
            var connection = _dbConnectionFactory.GetOpenConnection();

            const string sql =
                "    SELECT (CASE WHEN world_node.extractor_id is null " +
                "            THEN false " +
                "            ELSE true " +
                $"           END) AS {nameof(WorldNodeDto.IsTapped)} " +
                $"        , world_node.extraction_rate AS {nameof(WorldNodeDto.ExtractionRate)} " +
                $"        , node.id AS {nameof(WorldNodeDto.Id)}" +
                $"        , node.purity AS {nameof(WorldNodeDto.Purity)}" +
                $"        , node.biome AS {nameof(WorldNodeDto.Biome)}" +
                $"        , node.number AS {nameof(WorldNodeDto.Number)}" +
                $"        , node.map_position_x AS {nameof(WorldNodeDto.MapPositionX)}" +
                $"        , node.map_position_y AS {nameof(WorldNodeDto.MapPositionY)}" +
                $"        , node.map_position_z AS {nameof(WorldNodeDto.MapPositionZ)}" +
                $"        , resource.id AS {nameof(WorldNodeDto.ResourceId)}" +
                $"        , resource.name AS {nameof(WorldNodeDto.ResourceName)}" +
                "      FROM resources.world_nodes AS world_node " +
                "INNER JOIN resources.nodes AS node ON node.id = world_node.node_id " +
                "INNER JOIN resources.resources AS resource ON resource.id = node.resource_id " +
                "     WHERE world_node.world_id = @worldId " +
                "       AND (@resourceId is null or node.resource_id = @resourceId) " +
                "  ORDER BY resource.resource_sink_points, node.biome, node.number";

            var param = new
            {
                query.ResourceId, query.WorldId
            };

            var nodes = (await connection.QueryAsync<WorldNodeDto>(sql, param)).ToList();
            foreach (var node in nodes)
                node.MaxExtractionRate = await GetMaxExtractionRate(connection, node);

            return nodes;
        }

        private static async Task<decimal> GetMaxExtractionRate(IDbConnection connection, WorldNodeDto worldNode)
        {
            var extractor = await ExtractorFactory.GetFastestExtractor(connection, worldNode.ResourceId);
            var nodeModel = await NodeFactory.GetNode(connection, worldNode.Id);
            return ResourceExtractionCalculator.GetMaxExtractionRate(extractor, nodeModel);
        }
    }
}