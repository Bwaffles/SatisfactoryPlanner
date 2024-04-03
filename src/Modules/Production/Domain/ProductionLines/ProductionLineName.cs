using SatisfactoryPlanner.BuildingBlocks.Domain;
using SatisfactoryPlanner.Modules.Factories.Domain.ProductionLines.Rules;

namespace SatisfactoryPlanner.Modules.Factories.Domain.ProductionLines
{
    public class ProductionLineName : ValueObject
    {
        public CaseInsensitiveString Value { get; }

        private ProductionLineName(CaseInsensitiveString value)
        {
            CheckRule(new ProductionLineNameCannotBeEmptyRule(value));

            Value = new CaseInsensitiveString(value);
        }

        public static ProductionLineName As(CaseInsensitiveString value) => new(value);
    }
}