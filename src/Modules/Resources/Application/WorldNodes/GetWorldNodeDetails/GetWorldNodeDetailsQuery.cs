using Dapper;
using SatisfactoryPlanner.BuildingBlocks.Application.Data;
using SatisfactoryPlanner.Modules.Resources.Application.Configuration.Queries;
using SatisfactoryPlanner.Modules.Resources.Application.Extractors;
using SatisfactoryPlanner.Modules.Resources.Application.Nodes;
using SatisfactoryPlanner.Modules.Resources.Domain;
using System;
using System.Threading;
using System.Threading.Tasks;
using static SatisfactoryPlanner.Modules.Resources.Application.WorldNodes.GetWorldNodeDetails.WorldNodeDetailsResult;

namespace SatisfactoryPlanner.Modules.Resources.Application.WorldNodes.GetWorldNodeDetails
{
    public class GetWorldNodeDetailsQuery(Guid worldId, Guid nodeId) : QueryBase<WorldNodeDetailsResult>
    {
        public Guid WorldId { get; } = worldId;
        public Guid NodeId { get; } = nodeId;
    }

    internal class GetWorldNodeDetailsQueryHandler(IDbConnectionFactory dbConnectionFactory) : IQueryHandler<GetWorldNodeDetailsQuery, WorldNodeDetailsResult>
    {
        private readonly IDbConnectionFactory _dbConnectionFactory = dbConnectionFactory;

        public async Task<WorldNodeDetailsResult> Handle(GetWorldNodeDetailsQuery query, CancellationToken cancellationToken)
        {
            var connection = _dbConnectionFactory.GetOpenConnection();

            const string nodeDetailsSql =
                "     SELECT " +
                $"           world_node.is_tapped AS {nameof(WorldNodeDetails.IsTapped)} " +
                $"         , world_node.extractor_id AS {nameof(WorldNodeDetails.ExtractorId)} " +
                $"         , world_node.extraction_rate AS {nameof(WorldNodeDetails.ExtractionRate)} " +
                $"         , world_node.node_id AS {nameof(WorldNodeDetails.NodeId)}" +
                $"         , world_node.node_name AS {nameof(WorldNodeDetails.NodeName)}" +
                $"         , world_node.node_purity AS {nameof(WorldNodeDetails.Purity)}" +
                $"         , world_node.node_biome AS {nameof(WorldNodeDetails.Biome)}" +
                $"         , world_node.node_number AS {nameof(WorldNodeDetails.Number)}" +
                $"         , world_node.resource_id AS {nameof(WorldNodeDetails.ResourceId)}" +
                $"         , world_node.resource_name AS {nameof(WorldNodeDetails.ResourceName)}" +
                "       FROM resources.v_world_nodes AS world_node " +
                "      WHERE world_node.world_id = @worldId" +
                "        AND world_node.node_id = @nodeId";

            var param = new
            {
                query.NodeId,
                query.WorldId
            };

            var nodeDetails = await connection.QuerySingleAsync<WorldNodeDetails>(nodeDetailsSql, param);

            const string availableExtractorSql =
                $"   SELECT extractor.id AS {nameof(AvailableExtractor.Id)}" +
                $"        , extractor.name AS {nameof(AvailableExtractor.Name)}" +
                "      FROM resources.extractors AS extractor " +
                "     WHERE EXISTS (SELECT 1 " +
                "                     FROM resources.extractor_allowed_resources AS extractor_allowed_resource" +
                "                    WHERE extractor_allowed_resource.extractor_id = extractor.id " +
                "                      AND extractor_allowed_resource.resource_id = @resourceId) ";

            var availableExtractorParam = new
            {
                resourceId = nodeDetails.ResourceId
            };
            nodeDetails.AvailableExtractors =
                await connection.QueryAsync<AvailableExtractor>(availableExtractorSql, availableExtractorParam);
            var nodeModel = await NodeFactory.GetNode(connection, nodeDetails.NodeId);

            foreach (var availableExtractor in nodeDetails.AvailableExtractors)
            {
                var extractor = await ExtractorFactory.GetExtractor(connection, availableExtractor.Id);
                availableExtractor.MaxExtractionRate =
                    ResourceExtractionCalculator.GetMaxExtractionRate(extractor, nodeModel);
            }

            return new WorldNodeDetailsResult
            {
                Details = nodeDetails
            };
        }
    }
}