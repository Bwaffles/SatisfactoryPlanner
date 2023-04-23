using Dapper;
using SatisfactoryPlanner.Modules.Resources.Application.WorldNodes.GetWorldNodes;
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
        // TODO maybe I need a repository for Nodes? I don't remember why I chose to do it this way
        public static async Task<IEnumerable<Node>> GetNodes(IDbConnection connection, Guid? resourceId)
        {
            return (await GetAvailableNodes(connection, resourceId))
                .Select(CreateNode);
        }

        private static Node CreateNode(WorldNodeDto worldNode) =>
            Node.CreateNew(
                new NodeId(worldNode.Id),
                NodePurity.Of(worldNode.Purity),
                new ResourceId(worldNode.ResourceId));

        public static async Task<Node> GetNode(IDbConnection connection, Guid nodeId)
        {
            var availableNodes = await GetAvailableNodes(connection, null);

            var node = availableNodes.FirstOrDefault(_ => _.Id == nodeId);
            if (node == null)
                return null;

            return CreateNode(node);
        }

        public static async Task<List<WorldNodeDto>> GetAvailableNodes(IDbConnection connection, Guid? resourceId)
        {
            return (await connection.QueryAsync<WorldNodeDto>(
                "    SELECT " +
                $"          node.id AS {nameof(WorldNodeDto.Id)}, " +
                $"          resource.id AS {nameof(WorldNodeDto.ResourceId)}, " +
                $"          resource.name AS {nameof(WorldNodeDto.ResourceName)}, " +
                $"          node.purity AS {nameof(WorldNodeDto.Purity)}, " +
                $"          node.biome AS {nameof(WorldNodeDto.Biome)}, " +
                $"          node.map_position_x AS {nameof(WorldNodeDto.MapPositionX)}, " +
                $"          node.map_position_y AS {nameof(WorldNodeDto.MapPositionY)}, " +
                $"          node.map_position_z AS {nameof(WorldNodeDto.MapPositionZ)} " +
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