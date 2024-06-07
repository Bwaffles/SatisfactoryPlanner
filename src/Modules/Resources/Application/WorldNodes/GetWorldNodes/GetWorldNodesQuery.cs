using SatisfactoryPlanner.BuildingBlocks.Application.Data;
using SatisfactoryPlanner.Modules.Resources.Application.Configuration.Queries;
using SatisfactoryPlanner.Modules.Resources.Application.Extractors;
using SatisfactoryPlanner.Modules.Resources.Application.Nodes;
using SatisfactoryPlanner.Modules.Resources.Domain.Extractors;
using SatisfactoryPlanner.Modules.Resources.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using System.Threading;
using Dapper;
using System.Linq;
using static SatisfactoryPlanner.Modules.Resources.Application.WorldNodes.GetWorldNodes.GetWorldNodesResult;

namespace SatisfactoryPlanner.Modules.Resources.Application.WorldNodes.GetWorldNodes
{
    public class GetWorldNodesQuery(Guid worldId, Guid? resourceId) : QueryBase<GetWorldNodesResult>
    {
        public Guid WorldId { get; } = worldId;
        public Guid? ResourceId { get; } = resourceId;
    }

    internal class GetWorldNodesQueryHandler(IDbConnectionFactory dbConnectionFactory) : IQueryHandler<GetWorldNodesQuery, GetWorldNodesResult>
    {
        private readonly IDbConnectionFactory _dbConnectionFactory = dbConnectionFactory;
        private readonly Dictionary<Guid, Extractor> _fastestExtractors = [];

        public async Task<GetWorldNodesResult> Handle(GetWorldNodesQuery query, CancellationToken cancellationToken)
        {
            var connection = _dbConnectionFactory.GetOpenConnection();

            const string sql =
                "    SELECT " +
                $"          world_node.is_tapped AS {nameof(WorldNodeDto.IsTapped)}, " +
                $"          world_node.extraction_rate AS {nameof(WorldNodeDto.ExtractionRate)}, " +
                $"          world_node.node_id AS {nameof(WorldNodeDto.Id)}, " +
                $"          world_node.node_purity AS {nameof(WorldNodeDto.Purity)}, " +
                $"          world_node.node_biome AS {nameof(WorldNodeDto.Biome)}, " +
                $"          world_node.node_number AS {nameof(WorldNodeDto.Number)}, " +
                $"          world_node.node_map_position_x AS {nameof(WorldNodeDto.MapPositionX)}, " +
                $"          world_node.node_map_position_y AS {nameof(WorldNodeDto.MapPositionY)}, " +
                $"          world_node.node_map_position_z AS {nameof(WorldNodeDto.MapPositionZ)}, " +
                $"          world_node.resource_id AS {nameof(WorldNodeDto.ResourceId)}, " +
                $"          world_node.resource_name AS {nameof(WorldNodeDto.ResourceName)} " +
                "      FROM resources.v_world_nodes AS world_node " +
                "     WHERE world_node.world_id = @worldId " +
                "       AND (@resourceId is null or world_node.resource_id = @resourceId) " +
                "  ORDER BY world_node.resource_sink_points, world_node.node_biome,  world_node.node_number";

            var param = new
            {
                query.ResourceId,
                query.WorldId
            };

            var nodes = (await connection.QueryAsync<WorldNodeDto>(sql, param)).ToList();
            foreach (var node in nodes)
                node.MaxExtractionRate = await GetMaxExtractionRate(connection, node);

            return new GetWorldNodesResult
            {
                WorldNodes = nodes
            };
        }

        private async Task<decimal> GetMaxExtractionRate(IDbConnection connection, WorldNodeDto worldNode)
        {
            if (!_fastestExtractors.TryGetValue(worldNode.ResourceId, out var fastestExtractor))
            {
                fastestExtractor = await ExtractorFactory.GetFastestExtractor(connection, worldNode.ResourceId);
                _fastestExtractors.Add(worldNode.ResourceId, fastestExtractor);
            }

            var nodeModel = await NodeFactory.GetNode(connection, worldNode.Id);
            return ResourceExtractionCalculator.GetMaxExtractionRate(fastestExtractor, nodeModel);
        }
    }
}