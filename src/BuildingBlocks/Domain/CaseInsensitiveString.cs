using System;

namespace SatisfactoryPlanner.BuildingBlocks.Domain
{
    /// <summary>
    ///     A string that has case-insensitive equality.
    /// </summary>
    public readonly struct CaseInsensitiveString(string value) : IEquatable<CaseInsensitiveString>
    {
        public readonly string Value = value;

        public bool Equals(CaseInsensitiveString other) =>
            string.Equals(Value, other.Value, StringComparison.InvariantCultureIgnoreCase);

        public override bool Equals(object? obj)
        {
            if (obj is string stringValue)
                return Equals(new CaseInsensitiveString(stringValue));

            return obj is CaseInsensitiveString other && Equals(other);
        }

        public override int GetHashCode() => Value.GetHashCode();

        public static bool operator ==(CaseInsensitiveString left, CaseInsensitiveString right) => left.Equals(right);

        public static bool operator !=(CaseInsensitiveString left, CaseInsensitiveString right) => !(left == right);

        public override string ToString() => Value;

        public static implicit operator string(CaseInsensitiveString value) => value.Value;

        public static implicit operator CaseInsensitiveString(string value) => new(value);
    }
}