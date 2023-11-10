using Dapper;
using SatisfactoryPlanner.Modules.Resources.Domain.Extractors;
using SatisfactoryPlanner.Modules.Resources.Domain.Resources;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace SatisfactoryPlanner.Modules.Resources.Application.Extractors
{
    internal class ExtractorFactory
    { // TODO maybe I need a repository for Extractors? I don't remember why I chose to do it this way
        public static async Task<Extractor> GetExtractor(IDbConnection connection, Guid extractorId)
        {
            var extractor = (await GetAvailableExtractors(connection, null)).First(availableExtractor => availableExtractor.Id == extractorId);
            var allowedResourceIds = await GetAllowedResourceIds(connection, extractor.Id);

            return CreateExtractor(extractor, allowedResourceIds);
        }

        public static async Task<Extractor> GetFastestExtractor(IDbConnection connection, Guid resourceId)
        {
            // Water and nitrogen gas don't have extractors yet...
            var fastestExtractor = (await GetAvailableExtractors(connection, resourceId)).MinBy(availableExtractor => availableExtractor.SecondsToCompleteCycle);
            if (fastestExtractor == null)
                return null; // TODO fix this

            var allowedResourceIds = await GetAllowedResourceIds(connection, fastestExtractor.Id);

            return CreateExtractor(fastestExtractor, allowedResourceIds);
        }

        private static Extractor CreateExtractor(ExtractorDto extractor, List<Guid> allowedResourceIds)
        {
            return Extractor.CreateNew(
                new ExtractorId(extractor.Id),
                ExtractorCycle.CreateNew(extractor.SecondsToCompleteCycle, extractor.ResourcesExtractedPerCycle),
                ExtractorClockspeed.CreateNew(extractor.DefaultClockspeed, extractor.OverclockPerShard,
                    extractor.MaxShards),
                allowedResourceIds.Select(allowedResourceId => new ResourceId(allowedResourceId)).ToList());
        }

        internal static async Task<List<ExtractorDto>> GetAvailableExtractors(IDbConnection connection,
            Guid? resourceId)
        {
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
            return (await connection.QueryAsync<ExtractorDto>(
                sql,
                new
                {
                    resourceId
                })).ToList();
        }

        private static async Task<List<Guid>> GetAllowedResourceIds(IDbConnection connection, Guid extractorId)
        {
            const string sql = "SELECT extractor_allowed_resource.resource_id " +
                               "  FROM resources.extractor_allowed_resources AS extractor_allowed_resource " +
                               " WHERE extractor_allowed_resource.extractor_id = @extractorId ";
            return (await connection.QueryAsync<Guid>(
                sql,
                new
                {
                    extractorId
                })).ToList();
        }
    }
}