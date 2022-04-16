using SatisfactoryPlanner.BuildingBlocks.Domain;
using System;

namespace SatisfactoryPlanner.Modules.Resources.Domain.ResourceNodes
{
    public class ResourceNodePurity : ValueObject
    {
        public string Value { get; }

        public static ResourceNodePurity Impure => new ResourceNodePurity(nameof(Impure));

        public static ResourceNodePurity Normal => new ResourceNodePurity(nameof(Normal));

        public static ResourceNodePurity Pure => new ResourceNodePurity(nameof(Pure));

        public decimal GetMultiplier()
        {
            if (Value == Impure.Value)
                return 0.5m;

            if (Value == Normal.Value)
                return 1;

            if (Value == Pure.Value)
                return 2;

            throw new ArgumentOutOfRangeException(nameof(Value));
        }

        public static ResourceNodePurity Of(string value)
        {
            return new(value);
        }

        private ResourceNodePurity(string value)
        {
            Value = value;
        }
    }
}
