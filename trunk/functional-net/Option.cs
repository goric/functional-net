/*  
 * Functional .NET - extending the functional programming capabilities of non-F# .NET languages.
 * Copyright (C) 2012 Timothy Goric
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 * 
 *     http://www.apache.org/licenses/LICENSE-2.0
 * 
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 * 
*/

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
