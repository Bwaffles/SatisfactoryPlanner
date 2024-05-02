using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace SatisfactoryPlanner.API.Modules.Production.ProductionLines
{
    public class SetUpProductionLineRequest
    {
        [BindRequired]
        public string Name { get; set; }
    }
}
