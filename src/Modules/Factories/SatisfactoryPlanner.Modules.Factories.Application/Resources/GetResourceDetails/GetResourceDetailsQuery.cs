using SatisfactoryPlanner.Modules.Factories.Application.Configuration.Queries;

namespace SatisfactoryPlanner.Modules.Factories.Application.Resources.GetResourceDetails
{
    public class GetResourceDetailsQuery : QueryBase<ResourceDetailsDto>
    {
        public GetResourceDetailsQuery(string resourceCode)
        {
            ResourceCode = resourceCode;
        }

        public string ResourceCode { get; set; }
    }
}
