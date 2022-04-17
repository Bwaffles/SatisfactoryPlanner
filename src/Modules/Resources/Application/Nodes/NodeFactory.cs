using Dapper;
using SatisfactoryPlanner.Modules.Resources.Domain.Nodes;
using SatisfactoryPlanner.Modules.Resources.Domain.Resources;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace SatisfactoryPlanner.Modules.Resources.Application.Nodes
{
    public static class NodeFactory
    {
        public static async Task<Node> GetNode(IDbConnection connection, Guid nodeId)
        {
            var availableNodes = await GetAvailableNodes(connection, null);

            var node = availableNodes.FirstOrDefault(_ => _.Id == nodeId);
            if (node == null)
                return null;

            return Node.CreateNew(
                new NodeId(node.Id),
                NodePurity.Of(node.Purity),
                new ResourceId(node.ResourceId));
        }

        public static async Task<List<NodeDto>> GetAvailableNodes(IDbConnection connection, Guid? resourceId)
        {
            return (await connection.QueryAsync<NodeDto>(
               "    SELECT " +
               $"          node.id AS {nameof(NodeDto.Id)}, " +
               $"          resource.id AS {nameof(NodeDto.ResourceId)}, " +
               $"          resource.name AS {nameof(NodeDto.ResourceName)}, " +
               $"          node.purity AS {nameof(NodeDto.Purity)}, " +
               $"          node.biome AS {nameof(NodeDto.Biome)}, " +
               $"          node.map_position_x AS {nameof(NodeDto.MapPositionX)}, " +
               $"          node.map_position_y AS {nameof(NodeDto.MapPositionY)}, " +
               $"          node.map_position_z AS {nameof(NodeDto.MapPositionZ)} " +
               "      FROM resources.nodes AS node " +
               "INNER JOIN resources.resources AS resource ON resource.id = node.resource_id " +
               "     WHERE (@resourceId is null or node.resource_id = @resourceId) " +
               "  ORDER BY node.purity",
               new
               {
                   resourceId
               })).ToList();
        }
    }
}
