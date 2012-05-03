
using System;

namespace FunctionalDotNet
{
    public static class PartialApplication
    {
        public static Func<T2, TResult> Apply<T1, T2, TResult>(this Func<T1, T2, TResult> func, T1 value)
        {
            return x => func(value, x);
        }
        public static Func<T1, TResult> Apply<T1, T2, TResult>(this Func<T1, T2, TResult> func, T2 value)
        {
            return x => func(x, value);
        }
        
        public static Func<T2, T3, TResult> Apply<T1, T2, T3, TResult>(this Func<T1, T2, T3, TResult> func, T1 value)
        {
            return (x, y) => func(value, x, y);
        }
        public static Func<T3, TResult> Apply<T1, T2, T3, TResult>(this Func<T1, T2, T3, TResult> func, T1 first, T2 second)
        {
            return x => func(first, second, x);
        }

        public static Func<T2, T3, T4, TResult> Apply<T1, T2, T3, T4, TResult>(this Func<T1, T2, T3, T4, TResult> func, T1 value)
        {
            return (x, y, z) => func(value, x, y, z);
        }
        public static Func<T3, T4, TResult> Apply<T1, T2, T3, T4, TResult>(this Func<T1, T2, T3, T4, TResult> func, T1 first, T2 second)
        {
            return (x, y) => func(first, second, x, y);
        }
        public static Func<T4, TResult> Apply<T1, T2, T3, T4, TResult>(this Func<T1, T2, T3, T4, TResult> func, T1 first, T2 second, T3 third)
        {
            return x => func(first, second, third, x);
        }

        public static Func<T2, T3, T4, T5, TResult> Apply<T1, T2, T3, T4, T5, TResult>(this Func<T1, T2, T3, T4, T5, TResult> func, T1 value)
        {
            return (w, x, y, z) => func(value, w, x, y, z);
        }
        public static Func<T3, T4, T5, TResult> Apply<T1, T2, T3, T4, T5, TResult>(this Func<T1, T2, T3, T4, T5, TResult> func, T1 first, T2 second)
        {
            return (x, y, z) => func(first, second, x, y, z);
        }
        public static Func<T4, T5, TResult> Apply<T1, T2, T3, T4, T5, TResult>(this Func<T1, T2, T3, T4, T5, TResult> func, T1 first, T2 second, T3 third)
        {
            return (x, y) => func(first, second, third, x, y);
        }
        public static Func<T5, TResult> Apply<T1, T2, T3, T4, T5, TResult>(this Func<T1, T2, T3, T4, T5, TResult> func, T1 first, T2 second, T3 third, T4 fourth)
        {
            return x => func(first, second, third, fourth, x);
        }
    }
}
