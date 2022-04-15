using Dapper;
using SatisfactoryPlanner.BuildingBlocks.Application.Data;
using SatisfactoryPlanner.Modules.Factories.Application.Configuration.Queries;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SatisfactoryPlanner.Modules.Factories.Application.Resources.GetResourceNodes
{
    public class GetResourceNodesQueryHandler : IQueryHandler<GetResourceNodesQuery, List<ResourceNodeDto>>
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;

        public GetResourceNodesQueryHandler(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        public async Task<List<ResourceNodeDto>> Handle(GetResourceNodesQuery query, CancellationToken cancellationToken)
        {
            var connection = _dbConnectionFactory.GetOpenConnection();

            return (await connection.QueryAsync<ResourceNodeDto>(
               "SELECT " +
               $"resource_node.id AS {nameof(ResourceNodeDto.Id)}, " +
               $"item.id AS {nameof(ResourceNodeDto.ItemId)}, " +
               $"item.name AS {nameof(ResourceNodeDto.ItemName)}, " +
               $"resource_node.purity AS {nameof(ResourceNodeDto.Purity)}, " +
               $"resource_node.biome AS {nameof(ResourceNodeDto.Biome)}, " +
               $"resource_node.map_position_x AS {nameof(ResourceNodeDto.MapPositionX)}, " +
               $"resource_node.map_position_y AS {nameof(ResourceNodeDto.MapPositionY)}, " +
               $"resource_node.map_position_z AS {nameof(ResourceNodeDto.MapPositionZ)} " +
               "FROM factories.resource_nodes AS resource_node " +
               "INNER JOIN factories.items AS item ON item.id = resource_node.item_id " +
               "WHERE resource_node.item_id = @ResourceId " +
               "ORDER BY resource_node.purity",
               new
               {
                   query.ResourceId
               })).ToList();
        }
    }
}
