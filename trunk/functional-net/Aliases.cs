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
using System.Linq;

namespace FunctionalDotNet
{
    /// <summary>
    /// Wrappers on existing IEnumerable functions, re-named with more standard functional names
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
