using SatisfactoryPlanner.BuildingBlocks.Domain;
using SatisfactoryPlanner.Modules.Production.Domain.ProductionLines.Rules;

namespace SatisfactoryPlanner.Modules.Production.Domain.ProductionLines
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

        public override string ToString() => Value.ToString();
    }
}