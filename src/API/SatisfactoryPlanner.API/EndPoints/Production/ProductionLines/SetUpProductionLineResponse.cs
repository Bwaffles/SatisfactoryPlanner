namespace SatisfactoryPlanner.API.Modules.Production.ProductionLines
{
    public class SetUpProductionLineResponse(Guid productionLineId)
    {
        public Guid ProductionLineId { get; } = productionLineId;
    }
}
