using SatisfactoryPlanner.BuildingBlocks.Domain;

namespace SatisfactoryPlanner.Modules.Warehouses.Domain.ItemSources
{
    public sealed class Rate : ValueObject
    {
        public decimal Value { get; }

        private Rate(decimal value)
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException(nameof(value), value, "Rate cannot be negative.");

            Value = value;
        }

        public static Rate Of(decimal rate) => new(rate);
    }
}