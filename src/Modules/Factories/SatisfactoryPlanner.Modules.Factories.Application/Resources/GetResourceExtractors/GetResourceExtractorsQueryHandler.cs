using Dapper;
using SatisfactoryPlanner.BuildingBlocks.Application.Data;
using SatisfactoryPlanner.Modules.Factories.Application.Configuration.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
               $"     resource_extractor.code AS {nameof(ResourceExtractorDto.Code)}, " +
               $"     resource_extractor.name AS {nameof(ResourceExtractorDto.Name)} " +
               " FROM factories.resource_extractors AS resource_extractor " +
               "    , factories.resource_extractor_allowed_resources AS resource_extractor_allowed_resource " +
               "WHERE resource_extractor_allowed_resource.item_code = @ResourceCode " +
               "  AND resource_extractor_allowed_resource.resource_extractor_code = resource_extractor.code",
               new
               {
                   request.ResourceCode
               })).ToList();
        }
    }
}
