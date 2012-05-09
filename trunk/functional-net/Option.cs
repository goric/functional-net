using System;
using System.Collections.Generic;

namespace FunctionalDotNet
{
    /// <summary>
    /// An implementation of the Maybe/Option type: http://en.wikipedia.org/wiki/Option_type 
    /// Offers representation of of a specific type, even in the case of no value being present.
    /// Note this is different from Nullable which is constrained to structs
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class Option<T> : IEquatable<Option<T>>
    {
        private readonly T _value;
        private static readonly Option<T> _none = new Option<T>();

        public bool IsNone { get { return this == _none; } }
        public bool IsSome { get { return !IsNone; } }
        public static Option<T> None { get { return _none; } }
        public static Option<T> Some(T value) { return new Option<T>(value); }

        public T Value
        {
            get
            {
                if (IsSome)
                    return _value;

                throw new InvalidOperationException();
            }
        }

        private Option() { }
        private Option(T value)
        {
            _value = value;
        }

        public override bool Equals(object obj)
        {
            return obj is Option<T> && Equals((Option<T>) obj);
        }
        public bool Equals(Option<T> other)
        {
            return IsNone ? other.IsNone : EqualityComparer<T>.Default.Equals(_value, other._value);
        }

        public override int GetHashCode()
        {
            return IsNone ? 0 : EqualityComparer<T>.Default.GetHashCode(_value);
        }
    }

    public static class Option
    {
        public static Option<T> Some<T>(T value)
        {
            return Option<T>.Some(value);
        }
    }
}
