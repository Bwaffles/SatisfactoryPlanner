using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace SatisfactoryPlanner.API.Modules.Resources.Nodes
{
    public class IncreaseExtractionRateRequest
    {
        [BindRequired]
        public decimal ExtractionRate { get; set; }
    }
}