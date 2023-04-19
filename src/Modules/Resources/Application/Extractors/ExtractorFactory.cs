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
    {
        // TODO maybe I need a repository for Extractors? I don't remember why I chose to do it this way
        public static async Task<Extractor> GetFastestExtractor(IDbConnection connection, Guid resourceId)
        {
            var availableExtractors = await GetAvailableExtractors(connection, resourceId);

            var fastestExtractor = availableExtractors.MinBy(_ => _.SecondsToCompleteCycle);
            if (fastestExtractor == null)
                return null;

            var allowedResources = await GetAllowedResources(connection, fastestExtractor.Id);

            return CreateExtractor(fastestExtractor, allowedResources);
        }

        private static Extractor CreateExtractor(ExtractorDto extractor, List<Guid> allowedResources)
        {
            return Extractor.CreateNew(
                new ExtractorId(extractor.Id),
                ExtractorCycle.CreateNew(extractor.SecondsToCompleteCycle, extractor.ResourcesExtractedPerCycle),
                ExtractorClockspeed.CreateNew(extractor.DefaultClockspeed, extractor.OverclockPerShard,
                    extractor.MaxShards),
                allowedResources.Select(_ => new ResourceId(_)).ToList());
        }

        public static async Task<Extractor> GetExtractor(IDbConnection connection, Guid extractorId)
        {
            var availableExtractors = await GetAvailableExtractors(connection, null);

            var extractor = availableExtractors.FirstOrDefault(_ => _.Id == extractorId);
            if (extractor == null)
                return null;

            var allowedResources = await GetAllowedResources(connection, extractor.Id);

            return CreateExtractor(extractor, allowedResources);
        }

        internal static async Task<List<ExtractorDto>> GetAvailableExtractors(IDbConnection connection,
            Guid? resourceId)
        {
            return (await connection.QueryAsync<ExtractorDto>(
                "SELECT " +
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
                "                  AND extractor_allowed_resource.resource_id = @resourceId) ",
                new
                {
                    resourceId
                })).ToList();
        }

        private static async Task<List<Guid>> GetAllowedResources(IDbConnection connection, Guid extractorId)
        {
            return (await connection.QueryAsync<Guid>(
                "SELECT extractor_allowed_resource.resource_id " +
                "  FROM resources.extractor_allowed_resources AS extractor_allowed_resource " +
                " WHERE extractor_allowed_resource.extractor_id = @extractorId ",
                new
                {
                    extractorId
                })).ToList();
        }
    }
}