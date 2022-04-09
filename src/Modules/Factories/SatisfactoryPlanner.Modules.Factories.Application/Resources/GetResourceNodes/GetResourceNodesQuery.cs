using SatisfactoryPlanner.Modules.Factories.Application.Configuration.Queries;
using System.Collections.Generic;

namespace SatisfactoryPlanner.Modules.Factories.Application.Resources.GetResourceNodes
{
    public class GetResourceNodesQuery : QueryBase<List<ResourceNodeDto>>
    {
        public GetResourceNodesQuery(string resourceCode)
        {
            ResourceCode = resourceCode;
        }

        public string ResourceCode { get; set; }
    }
}
