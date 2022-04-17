using SatisfactoryPlanner.BuildingBlocks.Application.Data;
using SatisfactoryPlanner.Modules.Resources.Application.Configuration.Queries;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SatisfactoryPlanner.Modules.Resources.Application.Nodes.GetNodes
{
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

            return await NodeFactory.GetAvailableNodes(connection, query.ResourceId);
        }
    }
}
