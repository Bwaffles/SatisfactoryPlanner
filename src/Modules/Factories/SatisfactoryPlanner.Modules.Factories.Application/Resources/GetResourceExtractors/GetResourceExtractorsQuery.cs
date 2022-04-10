using SatisfactoryPlanner.Modules.Factories.Application.Configuration.Queries;
using System.Collections.Generic;

namespace SatisfactoryPlanner.Modules.Factories.Application.Resources.GetResourceExtractors
{
    public class GetResourceExtractorsQuery : QueryBase<List<ResourceExtractorDto>>
    {
        public GetResourceExtractorsQuery(string resourceCode)
        {
            ResourceCode = resourceCode;
        }

        public string ResourceCode { get; }
    }
}
