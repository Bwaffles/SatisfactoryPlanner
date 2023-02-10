using Dapper;
using SatisfactoryPlanner.BuildingBlocks.Application.Data;
using SatisfactoryPlanner.Modules.Resources.Application.Configuration.Queries;
using System.Collections.Generic;
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
                               $"         , node.map_position_x AS {nameof(NodeDto.MapPositionX)}" +
                               $"         , node.map_position_y AS {nameof(NodeDto.MapPositionY)}" +
                               $"         , node.map_position_z AS {nameof(NodeDto.MapPositionZ)}" +
                               "       FROM resources.nodes AS node " +
                               " INNER JOIN resources.resources AS resource ON resource.id = node.resource_id " +
                               "      WHERE (@resourceId is null or node.resource_id = @resourceId) " +
                               "   ORDER BY resource.resource_sink_points, node.biome, node.purity";

            var param = new
            {
                query.ResourceId
            };

            return (await connection.QueryAsync<NodeDto>(sql, param)).ToList();
        }
    }
}
