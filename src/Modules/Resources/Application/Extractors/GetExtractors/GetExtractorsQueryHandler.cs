using Dapper;
using SatisfactoryPlanner.BuildingBlocks.Application.Data;
using SatisfactoryPlanner.Modules.Resources.Application.Configuration.Queries;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SatisfactoryPlanner.Modules.Resources.Application.Extractors.GetExtractors
{
    internal class GetExtractorsQueryHandler : IQueryHandler<GetExtractorsQuery, List<ExtractorDto>>
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;

        public GetExtractorsQueryHandler(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        public async Task<List<ExtractorDto>> Handle(GetExtractorsQuery query, CancellationToken cancellationToken)
        {
            var connection = _dbConnectionFactory.GetOpenConnection();

            return (await connection.QueryAsync<ExtractorDto>(
               "SELECT " +
               $"     extractor.id AS {nameof(ExtractorDto.Id)}, " +
               $"     extractor.name AS {nameof(ExtractorDto.Name)} " +
               " FROM resources.extractors AS extractor " +
               "WHERE @ResourceId is null " +
               "   OR EXISTS (SELECT 1 " +
               "                FROM resources.extractor_allowed_resources AS extractor_allowed_resource" +
               "               WHERE extractor_allowed_resource.extractor_id = extractor.id " +
               "                 AND extractor_allowed_resource.resource_id = @ResourceId) ",
               new
               {
                   query.ResourceId
               })).ToList();
        }
    }
}
