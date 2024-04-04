using SatisfactoryPlanner.BuildingBlocks.Domain;

namespace SatisfactoryPlanner.Modules.Production.Domain.ProductionLines.Rules
{
    public class ProductionLineNameCannotBeEmptyRule(CaseInsensitiveString name) : IBusinessRule
    {
        public string Message => "Name cannot be empty.";

        public bool IsBroken() => string.IsNullOrWhiteSpace(name);
    }
}