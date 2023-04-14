using Dapper;
using SatisfactoryPlanner.BuildingBlocks.Application.Data;
using SatisfactoryPlanner.Modules.Resources.Application.Configuration.Queries;
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

            const string sql = $"    SELECT node.id AS {nameof(NodeDetailsDto.Id)}" +
                               $"         , resource.id AS {nameof(NodeDetailsDto.ResourceId)}" +
                               $"         , resource.name AS {nameof(NodeDetailsDto.ResourceName)}" +
                               $"         , node.purity AS {nameof(NodeDetailsDto.Purity)}" +
                               $"         , node.biome AS {nameof(NodeDetailsDto.Biome)}" +
                               $"         , node.number AS {nameof(NodeDetailsDto.Number)}" +
                               "         , (SELECT exists(SELECT 1" +
                               "                            FROM resources.tapped_nodes AS tapped_node" +
                               "                           WHERE tapped_node.world_id = @worldId" +
                               $"                             AND tapped_node.node_id = node.id)) AS {nameof(NodeDetailsDto.IsTapped)}" +
                               "         , (SELECT tapped_node.amount_to_extract " +
                               "              FROM resources.tapped_nodes AS tapped_node" +
                               "              WHERE tapped_node.world_id = @worldId" +
                               $"                AND tapped_node.node_id = node.id) AS {nameof(NodeDetailsDto.AmountToExtract)}" +
                               "       FROM resources.nodes AS node " +
                               " INNER JOIN resources.resources AS resource ON resource.id = node.resource_id " +
                               "      WHERE node.id = @nodeId";

            var param = new
            {
                query.NodeId, query.WorldId
            };

            return await connection.QuerySingleAsync<NodeDetailsDto>(sql, param);
        }
    }
}