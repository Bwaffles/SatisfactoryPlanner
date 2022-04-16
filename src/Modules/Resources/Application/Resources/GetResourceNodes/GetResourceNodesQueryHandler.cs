using SatisfactoryPlanner.BuildingBlocks.Application.Data;
using SatisfactoryPlanner.Modules.Resources.Application.Configuration.Queries;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SatisfactoryPlanner.Modules.Resources.Application.Resources.GetResourceNodes
{
    internal class GetResourceNodesQueryHandler : IQueryHandler<GetResourceNodesQuery, List<ResourceNodeDto>>
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;

        public GetResourceNodesQueryHandler(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        public async Task<List<ResourceNodeDto>> Handle(GetResourceNodesQuery query, CancellationToken cancellationToken)
        {
            var connection = _dbConnectionFactory.GetOpenConnection();

            return await ResourceNodeFactory.GetAvailableResourceNodes(connection, query.ResourceId);
        }
    }
}
