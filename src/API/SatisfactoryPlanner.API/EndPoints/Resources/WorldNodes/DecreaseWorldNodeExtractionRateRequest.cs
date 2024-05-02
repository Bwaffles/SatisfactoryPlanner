using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace SatisfactoryPlanner.API.Modules.Resources.WorldNodes
{
    public class DecreaseWorldNodeExtractionRateRequest
    {
        [BindRequired]
        public decimal ExtractionRate { get; set; }
    }
}