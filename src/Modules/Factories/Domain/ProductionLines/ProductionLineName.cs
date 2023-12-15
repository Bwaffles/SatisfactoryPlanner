using SatisfactoryPlanner.BuildingBlocks.Domain;
using SatisfactoryPlanner.Modules.Factories.Domain.ProductionLines.Rules;

namespace SatisfactoryPlanner.Modules.Factories.Domain.ProductionLines
{
    public class ProductionLineName : ValueObject
    {
        public string Value { get; }

        private ProductionLineName(string value)
        {
            CheckRule(new ProductionLineNameCannotBeEmptyRule(value));

            Value = value;
        }

        public static ProductionLineName As(string value) => new(value);
    }
}