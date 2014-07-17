using System;

namespace SkbLib
{
    internal class PrefixString : IComparable<PrefixString>, IEquatable<PrefixString>
    {
        private readonly char[] _buffer;
        private readonly int _hashCode;

        public readonly int Length;

        public PrefixString(string str) : this(str.ToCharArray(), str.Length)
        { }

        private PrefixString(char[] buffer, int length)
        {
            _buffer = buffer;
            Length = length;
            _hashCode = CalculateHashCode();
        }

        public PrefixString GetPrefix(int length)
        {
            if (length <= 0)
                throw new ArgumentException("length should be greater than 0", "length");

            return length >= Length ? this : new PrefixString(_buffer, length);
        }

        private int CalculateHashCode()
        {
            if (Length == 0)
                return 0;

            const int prime = 37;
            var seed = 173;

            unchecked
            {
                for (var i = 0; i < Length; i++)
                    seed = prime*seed + _buffer[i];
            }
            return seed;
        }

        public int CompareTo(PrefixString other)
        {
            if (ReferenceEquals(_buffer, other._buffer))
                return Length.CompareTo(other.Length);

            var minLength = Math.Min(Length, other.Length);

            for (var i = 0; i < minLength; i++)
                if (_buffer[i] != other._buffer[i])
                    return _buffer[i] < other._buffer[i] ? -1 : 1;

            return Length.CompareTo(other.Length);
        }

        public bool Equals(PrefixString other)
        {
            return CompareTo(other) == 0;
        }

        public override int GetHashCode()
        {
            return _hashCode;
        }

        public override string ToString()
        {
            return new string(_buffer, 0, Length);
        }
    }
}
