namespace Utilities
{
    using System;

    public struct AbcInt : IComparable, IComparable<AbcInt>, IEquatable<AbcInt>
    {
        public ushort Value { get; private set; }
        public ushort Level { get; private set; }

        public AbcInt(ushort value, ushort level)
        {
            this.Value = value;
            this.Level = level;
        }

        public AbcInt(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(value));
            }

            var split = value.Split(' ');
            if (split.Length != 2)
            {
                throw new ArgumentException("Value must be in the format of 'value level'.", nameof(value));
            }

            if (!ushort.TryParse(split[0], out var v))
            {
                throw new ArgumentException("Value must be a valid number.", nameof(value));
            }

            if (!AbcIntExtensions.TryParseAbcLevel(split[1], out var l))
            {
                throw new ArgumentException("Level must be a valid ABC level.", nameof(value));
            }

            this.Value = v;
            this.Level = l;
        }

        public int CompareTo(object obj)
        {
            if (obj is AbcInt abcInt)
            {
                return this.CompareTo(abcInt);
            }

            throw new ArgumentException($"Object is not an {nameof(AbcInt)}");
        }

        public int CompareTo(AbcInt other)
        {
            return this.Level == other.Level ? this.Value.CompareTo(other.Value) : this.Level.CompareTo(other.Level);
        }

        public bool Equals(AbcInt other)
        {
            return this.Value == other.Value && this.Level == other.Level;
        }

        public override bool Equals(object obj)
        {
            return obj is AbcInt other && this.Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.Value, this.Level);
        }

        public override string ToString()
        {
            return $"{this.Value} ({this.Level})";
        }
    }
}