using Dapper;
using SatisfactoryPlanner.BuildingBlocks.Application.Data;
using SatisfactoryPlanner.Modules.Factories.Application.Configuration.Queries;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SatisfactoryPlanner.Modules.Factories.Application.Resources.GetResources
{
    public class GetResourcesQueryHandler : IQueryHandler<GetResourcesQuery, List<ResourceDto>>
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
               "SELECT " +
               $"item.code AS {nameof(ResourceDto.Code)}, " +
               $"item.name AS {nameof(ResourceDto.Name)} " +
               "FROM factories.items AS item " +
               "WHERE item.type = 'Resource' " +
               "ORDER BY item.resource_form desc, item.resource_sink_points"))
               .AsList();
        }
    }
}
