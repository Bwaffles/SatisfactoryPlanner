using SatisfactoryPlanner.BuildingBlocks.Domain;
using SatisfactoryPlanner.Modules.Production.Domain.ProductionLines.Rules;

namespace SatisfactoryPlanner.Modules.Production.Domain.ProductionLines
{
    /// <summary>
    ///     The name of the production line.
    /// </summary>
    /// <remarks>
    ///     The name is case insensitive so that there can't be 2 lines with the same name, 
    ///     however we preserve the exact casing of the entered name so the user is in control of how it appears.
    ///     For example, they have some acronyms all in caps and they want that to show exactly. If we store it
    ///     without the entered casing we lose that context.
    /// </remarks>
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

        internal bool HasSameCasingAs(ProductionLineName name) => string.Equals(Value.Value, name.Value.Value, StringComparison.InvariantCulture);
    }
}