using System;
using System.Collections.Generic;
using System.Linq;

namespace FunctionalDotNet
{
    /// <summary>
    /// Wrappers on existing IEnumerable functions, re-named with their standard functional names
    /// </summary>
    public static class Aliases
    {
        #region Map

        public static IEnumerable<T2> Map<T, T2>(this IEnumerable<T> enumerable, Func<T, T2> selector)
        {
            return enumerable.Select(selector);
        }
        public static IEnumerable<T2> Mapi<T, T2>(this IEnumerable<T> enumerable, Func<T, int, T2> selector)
        {
            return enumerable.Select(selector);
        }
        public static IEnumerable<T3> Map2<T, T2, T3>(this IEnumerable<T> enumerable, IEnumerable<T2> other, Func<T,T2,T3> selector)
        {
            return enumerable.Zip(other, selector);
        }

        #endregion

        #region Fold / Reduce

        public static T Reduce<T>(this IEnumerable<T> enumerable, Func<T, T, T> func)
        {
            return enumerable.Aggregate(func);
        }
        public static T2 Reduce<T, T2>(this IEnumerable<T> enumerable, T2 initval, Func<T2, T, T2> func)
        {
            return enumerable.Aggregate(initval, func);
        }

        public static T Fold<T>(this IEnumerable<T> enumerable, Func<T, T, T> func)
        {
            return Reduce(enumerable, func);
        }
        public static T2 Fold<T, T2>(this IEnumerable<T> enumerable, T2 initval, Func<T2, T, T2> func)
        {
            return Reduce(enumerable, initval, func);
        }
        public static T FoldL<T>(this IEnumerable<T> enumerable, Func<T, T, T> func)
        {
            return Reduce(enumerable, func);
        }
        public static T2 FoldL<T, T2>(this IEnumerable<T> enumerable, T2 initval, Func<T2, T, T2> func)
        {
            return Reduce(enumerable, initval, func);
        }

        public static T FoldR<T>(this IEnumerable<T> enumerable, Func<T, T, T> func)
        {
            return enumerable.Reverse().Aggregate(func);
        }
        public static T2 FoldR<T, T2>(this IEnumerable<T> enumerable, T2 initval, Func<T2, T, T2> func)
        {
            return enumerable.Reverse().Aggregate(initval, func);
        }

        #endregion

        public static IEnumerable<T> Filter<T>(this IEnumerable<T> enumerable, Func<T, bool> pred)
        {
            return enumerable.Where(pred);
        }
        public static T Find<T>(this IEnumerable<T> enumerable, Func<T, bool> pred)
        {
            return enumerable.First(pred);
        }
        public static T Head<T>(this IEnumerable<T> enumerable)
        {
            return enumerable.First();
        }
    }
}
