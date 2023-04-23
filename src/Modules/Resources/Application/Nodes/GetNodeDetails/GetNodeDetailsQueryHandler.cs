using Dapper;
using SatisfactoryPlanner.BuildingBlocks.Application.Data;
using SatisfactoryPlanner.Modules.Resources.Application.Configuration.Queries;
using SatisfactoryPlanner.Modules.Resources.Application.Extractors;
using SatisfactoryPlanner.Modules.Resources.Domain;
using System.Threading;
using System.Threading.Tasks;

namespace SatisfactoryPlanner.Modules.Resources.Application.Nodes.GetNodeDetails
{
    // ReSharper disable once UnusedMember.Global
    internal class GetNodeDetailsQueryHandler : IQueryHandler<GetNodeDetailsQuery, NodeDetailsDto>
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;

        public GetNodeDetailsQueryHandler(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        public async Task<NodeDetailsDto> Handle(GetNodeDetailsQuery query, CancellationToken cancellationToken)
        {
            var connection = _dbConnectionFactory.GetOpenConnection();

            const string nodeDetailsSql =
                "     SELECT (CASE WHEN tapped_node.extractor_id is null " +
                "             THEN false " +
                "             ELSE true " +
                $"            END) AS {nameof(NodeDto.IsTapped)} " +
                $"         , tapped_node.extraction_rate AS {nameof(NodeDto.ExtractionRate)} " +
                $"         , node.id AS {nameof(NodeDetailsDto.Id)}" +
                $"         , node.purity AS {nameof(NodeDetailsDto.Purity)}" +
                $"         , node.biome AS {nameof(NodeDetailsDto.Biome)}" +
                $"         , node.number AS {nameof(NodeDetailsDto.Number)}" +
                $"         , resource.id AS {nameof(NodeDetailsDto.ResourceId)}" +
                $"         , resource.name AS {nameof(NodeDetailsDto.ResourceName)}" +
                "       FROM resources.tapped_nodes AS tapped_node " +
                " INNER JOIN resources.nodes     AS node     ON node.id     = tapped_node.node_id " +
                " INNER JOIN resources.resources AS resource ON resource.id = node.resource_id " +
                "      WHERE tapped_node.world_id = @worldId" +
                "        AND tapped_node.node_id = @nodeId";

            var param = new
            {
                query.NodeId, query.WorldId
            };

            var nodeDetails = await connection.QuerySingleAsync<NodeDetailsDto>(nodeDetailsSql, param);

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
                var nodeModel = await NodeFactory.GetNode(connection, nodeDetails.Id);
                availableExtractor.MaxExtractionRate =
                    ResourceExtractionCalculator.GetMaxExtractionRate(extractor, nodeModel);
            }

            return nodeDetails;
        }
    }
}