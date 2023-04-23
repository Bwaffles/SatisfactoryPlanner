using Dapper;
using SatisfactoryPlanner.BuildingBlocks.Application.Data;
using SatisfactoryPlanner.Modules.Resources.Application.Configuration.Queries;
using SatisfactoryPlanner.Modules.Resources.Application.Extractors;
using SatisfactoryPlanner.Modules.Resources.Application.Nodes;
using SatisfactoryPlanner.Modules.Resources.Domain;
using System.Threading;
using System.Threading.Tasks;

namespace SatisfactoryPlanner.Modules.Resources.Application.WorldNodes.GetWorldNodeDetails
{
    // ReSharper disable once UnusedMember.Global
    internal class GetWorldNodeDetailsQueryHandler : IQueryHandler<GetWorldNodeDetailsQuery, WorldNodeDetailsDto>
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;

        public GetWorldNodeDetailsQueryHandler(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        public async Task<WorldNodeDetailsDto> Handle(GetWorldNodeDetailsQuery query,
            CancellationToken cancellationToken)
        {
            var connection = _dbConnectionFactory.GetOpenConnection();

            const string nodeDetailsSql =
                "     SELECT (CASE WHEN world_node.extractor_id is null " +
                "             THEN false " +
                "             ELSE true " +
                $"            END) AS {nameof(WorldNodeDetailsDto.IsTapped)} " +
                $"         , world_node.extractor_id AS {nameof(WorldNodeDetailsDto.ExtractorId)} " +
                $"         , world_node.extraction_rate AS {nameof(WorldNodeDetailsDto.ExtractionRate)} " +
                $"         , node.id AS {nameof(WorldNodeDetailsDto.NodeId)}" +
                $"         , node.purity AS {nameof(WorldNodeDetailsDto.Purity)}" +
                $"         , node.biome AS {nameof(WorldNodeDetailsDto.Biome)}" +
                $"         , node.number AS {nameof(WorldNodeDetailsDto.Number)}" +
                $"         , resource.id AS {nameof(WorldNodeDetailsDto.ResourceId)}" +
                $"         , resource.name AS {nameof(WorldNodeDetailsDto.ResourceName)}" +
                "       FROM resources.world_nodes AS world_node " +
                " INNER JOIN resources.nodes     AS node     ON node.id     = world_node.node_id " +
                " INNER JOIN resources.resources AS resource ON resource.id = node.resource_id " +
                "      WHERE world_node.world_id = @worldId" +
                "        AND world_node.node_id = @nodeId";

            var param = new
            {
                query.NodeId, query.WorldId
            };

            var nodeDetails = await connection.QuerySingleAsync<WorldNodeDetailsDto>(nodeDetailsSql, param);

            const string availableExtractorSql =
                $"   SELECT extractor.id AS {nameof(AvailableExtractorDto.Id)}" +
                $"        , extractor.name AS {nameof(AvailableExtractorDto.Name)}" +
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
                await connection.QueryAsync<AvailableExtractorDto>(availableExtractorSql, availableExtractorParam);

            foreach (var availableExtractor in nodeDetails.AvailableExtractors)
            {
                var extractor = await ExtractorFactory.GetExtractor(connection, availableExtractor.Id);
                var nodeModel = await NodeFactory.GetNode(connection, nodeDetails.NodeId);
                availableExtractor.MaxExtractionRate =
                    ResourceExtractionCalculator.GetMaxExtractionRate(extractor, nodeModel);
            }

            return nodeDetails;
        }
    }
}