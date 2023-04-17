using Dapper;
using SatisfactoryPlanner.BuildingBlocks.Application.Data;
using SatisfactoryPlanner.Modules.Resources.Application.Configuration.Queries;
using SatisfactoryPlanner.Modules.Resources.Application.Nodes;
using SatisfactoryPlanner.Modules.Resources.Domain.Resources;
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

            const string sql = "SELECT " +
                               $"      extractor.id AS {nameof(ExtractorDto.Id)}, " +
                               $"      extractor.name AS {nameof(ExtractorDto.Name)}, " +
                               $"      extractor.seconds_to_complete_cycle AS {nameof(ExtractorDto.SecondsToCompleteCycle)}, " +
                               $"      extractor.resources_extracted_per_cycle AS {nameof(ExtractorDto.ResourcesExtractedPerCycle)}, " +
                               $"      extractor.default_clockspeed AS {nameof(ExtractorDto.DefaultClockspeed)}, " +
                               $"      extractor.overclock_per_shard AS {nameof(ExtractorDto.OverclockPerShard)}, " +
                               $"      extractor.max_shards AS {nameof(ExtractorDto.MaxShards)} " +
                               "  FROM resources.extractors AS extractor " +
                               " WHERE @resourceId is null " +
                               "    OR EXISTS (SELECT 1 " +
                               "                 FROM resources.extractor_allowed_resources AS extractor_allowed_resource" +
                               "                WHERE extractor_allowed_resource.extractor_id = extractor.id " +
                               "                  AND extractor_allowed_resource.resource_id = @resourceId) ";

            var param = new
            {
                query.ResourceId
            };

            return (await connection.QueryAsync<ExtractorDto>(sql, param)).ToList();
        }
    }
}
