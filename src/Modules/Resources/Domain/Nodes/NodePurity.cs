using SatisfactoryPlanner.BuildingBlocks.Domain;
using System;

namespace SatisfactoryPlanner.Modules.Resources.Domain.Nodes
{
    public class NodePurity : ValueObject
    {
        public string Value { get; }

        public static NodePurity Impure => new NodePurity(nameof(Impure));

        public static NodePurity Normal => new NodePurity(nameof(Normal));

        public static NodePurity Pure => new NodePurity(nameof(Pure));

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

        public static NodePurity Of(string value)
        {
            return new(value);
        }

        private NodePurity(string value)
        {
            Value = value;
        }
    }
}
