using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace SatisfactoryPlanner.API.Modules.Resources.WorldNodes
{
    public class IncreaseWorldNodeExtractionRateRequest
    {
        [BindRequired]
        public decimal ExtractionRate { get; set; }
    }
}