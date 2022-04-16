using Dapper;
using SatisfactoryPlanner.BuildingBlocks.Application.Data;
using SatisfactoryPlanner.Modules.Resources.Application.Configuration.Queries;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SatisfactoryPlanner.Modules.Resources.Application.Resources.GetResources
{
    internal class GetResourcesQueryHandler : IQueryHandler<GetResourcesQuery, List<ResourceDto>>
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;

        public GetResourcesQueryHandler(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        public async Task<List<ResourceDto>> Handle(GetResourcesQuery request, CancellationToken cancellationToken)
        {
            var connection = _dbConnectionFactory.GetOpenConnection();

            return (await connection.QueryAsync<ResourceDto>(
               "  SELECT " +
               $"        resource.id AS {nameof(ResourceDto.Id)}, " +
               $"        resource.name AS {nameof(ResourceDto.Name)} " +
               "    FROM resources.resources AS resource " +
               "ORDER BY resource.resource_form desc" +
               "       , resource.resource_sink_points"))
               .AsList();
        }
    }
}
