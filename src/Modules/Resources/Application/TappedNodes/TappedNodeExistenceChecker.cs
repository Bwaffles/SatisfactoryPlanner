using Dapper;
using SatisfactoryPlanner.BuildingBlocks.Application.Data;
using SatisfactoryPlanner.Modules.Resources.Domain.TappedNodes;
using System;

namespace SatisfactoryPlanner.Modules.Resources.Application.TappedNodes
{
    public class TappedNodeExistenceChecker : ITappedNodeExistenceChecker
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;

        public TappedNodeExistenceChecker(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        public bool IsTapped(Guid nodeId)
        {
            var connection = _dbConnectionFactory.GetOpenConnection();


            return connection.QuerySingle<int>(
                "SELECT COUNT(*) " +
                "  FROM resources.tapped_nodes AS tapped_node " +
                " WHERE tapped_node.node_id = @nodeId",
                new
                {
                    nodeId
                }) > 0;
        }
    }
}
