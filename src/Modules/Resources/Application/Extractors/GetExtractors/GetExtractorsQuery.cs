using SatisfactoryPlanner.Modules.Resources.Application.Configuration.Queries;
using System;
using System.Collections.Generic;

namespace SatisfactoryPlanner.Modules.Resources.Application.Extractors.GetExtractors
{
    public class GetExtractorsQuery : QueryBase<List<ExtractorDto>>
    {
        /// <summary>
        ///     The id of the resource that the extractors must be capable of tapping, or null to return all extractors.
        /// </summary>
        public Guid? ResourceId { get; }

        public GetExtractorsQuery(Guid? resourceId)
        {
            ResourceId = resourceId;
        }
    }
}