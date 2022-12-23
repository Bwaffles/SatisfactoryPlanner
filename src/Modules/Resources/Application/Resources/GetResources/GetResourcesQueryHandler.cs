using Dapper;
using SatisfactoryPlanner.BuildingBlocks.Application.Data;
using SatisfactoryPlanner.Modules.Resources.Application.Configuration.Queries;
using SatisfactoryPlanner.Modules.Resources.Application.Extractors;
using SatisfactoryPlanner.Modules.Resources.Application.Nodes;
using SatisfactoryPlanner.Modules.Resources.Domain;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SatisfactoryPlanner.Modules.Resources.Application.Resources.GetResources
{
    // ReSharper disable once UnusedMember.Global
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

            const string sql = "  SELECT " +
                               $"        resource.id AS {nameof(ResourceDto.Id)}, " +
                               $"        resource.name AS {nameof(ResourceDto.Name)}, " +
                               $"        0 AS {nameof(ResourceDto.ExtractedResources)} " +
                               "    FROM resources.resources AS resource " +
                               "ORDER BY resource.resource_form desc " +
                               "       , resource.resource_sink_points;";
            var resources = (await connection.QueryAsync<ResourceDto>(
                    sql))
                .AsList();

            foreach (var resource in resources)
            {
                var extractor = await ExtractorFactory.GetFastestExtractor(connection, resource.Id);
                var nodes = await NodeFactory.GetNodes(connection, resource.Id);

                foreach (var node in nodes)
                    resource.TotalResources += ResourceExtractionCalculator.GetAmountExtractable(extractor, node);
            }

            return resources;
        }
    }
}