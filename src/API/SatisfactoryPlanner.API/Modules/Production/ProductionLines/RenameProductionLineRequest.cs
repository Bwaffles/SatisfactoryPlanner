using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace SatisfactoryPlanner.API.Modules.Production.ProductionLines
{
    public class RenameProductionLineRequest
    {
        [BindRequired]
        public string Name { get; set; }
    }
}
