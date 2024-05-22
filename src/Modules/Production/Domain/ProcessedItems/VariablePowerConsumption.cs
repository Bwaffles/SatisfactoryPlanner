using SatisfactoryPlanner.BuildingBlocks.Domain;

namespace SatisfactoryPlanner.Modules.Production.Domain.ProcessedItems
{
    public class VariablePowerConsumption : ValueObject
    {
        /// <summary>
        /// Added to the variable power consumption. If the power-consumption curve's range (all possible output values)
        /// is normalized to[0.0, 1.0], this constant can be thought of as a power-consumption minimum.
        /// Note: Only available if "Produced In" contains any building that supports variable power consumption.
        /// </summary>
        public decimal Constant { get; }

        /// <summary>
        /// Multiplied into the variable power consumption. If the power-consumption curve's range (all possible output values)
        /// is normalized to[0.0, 1.0], this value added to the power-consumption constant can be thought of as a power-consumption maximum.
        /// Note: Only available if "Produced In" contains any building that supports variable power consumption.
        /// </summary>
        public decimal Factor { get; }

        private VariablePowerConsumption(decimal constant, decimal factor)
        {
            Constant = constant;
            Factor = factor;
        }

        public static VariablePowerConsumption Of(decimal constant, decimal factor)
        => new(constant, factor);

        public static VariablePowerConsumption None() => new(0, 1);
    }
}