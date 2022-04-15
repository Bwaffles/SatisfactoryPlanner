using Dapper;
using SatisfactoryPlanner.BuildingBlocks.Application.Data;
using SatisfactoryPlanner.Modules.Factories.Application.Configuration.Queries;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SatisfactoryPlanner.Modules.Factories.Application.Resources.GetResourceExtractors
{
    internal class GetResourceExtractorsQueryHandler : IQueryHandler<GetResourceExtractorsQuery, List<ResourceExtractorDto>>
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;

        public GetResourceExtractorsQueryHandler(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        public async Task<List<ResourceExtractorDto>> Handle(GetResourceExtractorsQuery request, CancellationToken cancellationToken)
        {
            var connection = _dbConnectionFactory.GetOpenConnection();

            return (await connection.QueryAsync<ResourceExtractorDto>(
               "SELECT " +
               $"     resource_extractor.id AS {nameof(ResourceExtractorDto.Id)}, " +
               $"     resource_extractor.code AS {nameof(ResourceExtractorDto.Code)}, " +
               $"     resource_extractor.name AS {nameof(ResourceExtractorDto.Name)} " +
               " FROM factories.resource_extractors AS resource_extractor " +
               "    , factories.resource_extractor_allowed_resources AS resource_extractor_allowed_resource " +
               "WHERE resource_extractor_allowed_resource.item_id = @ResourceId " +
               "  AND resource_extractor_allowed_resource.resource_extractor_id = resource_extractor.id",
               new
               {
                   request.ResourceId
               })).ToList();
        }
    }
}
