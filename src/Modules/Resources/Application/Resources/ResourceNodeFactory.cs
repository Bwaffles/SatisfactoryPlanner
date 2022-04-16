using Dapper;
using SatisfactoryPlanner.Modules.Resources.Domain.ResourceNodes;
using SatisfactoryPlanner.Modules.Resources.Domain.Resources;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace SatisfactoryPlanner.Modules.Resources.Application.Resources
{
    public static class ResourceNodeFactory
    {
        public static async Task<ResourceNode> GetResourceNode(IDbConnection connection, Guid resourceNodeId)
        {
            var availableResourceNodes = await GetAvailableResourceNodes(connection, null);

            var resourceNode = availableResourceNodes.FirstOrDefault(_ => _.Id == resourceNodeId);
            if (resourceNode == null)
                return null;

            return ResourceNode.CreateNew(
                new ResourceNodeId(resourceNode.Id),
                ResourceNodePurity.Of(resourceNode.Purity),
                new ResourceId(resourceNode.ItemId));
        }

        public static async Task<List<ResourceNodeDto>> GetAvailableResourceNodes(IDbConnection connection, Guid? resourceId)
        {
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
               "FROM resources.resource_nodes AS resource_node " +
               "INNER JOIN resources.items AS item ON item.id = resource_node.item_id " +
               "WHERE (@resourceId is null or resource_node.item_id = @resourceId) " +
               "ORDER BY resource_node.purity",
               new
               {
                   resourceId
               })).ToList();
        }
    }
}
