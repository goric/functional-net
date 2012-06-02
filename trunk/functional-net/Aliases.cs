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

        /// <summary>
        /// Applies the given projection to each element of the sequence.
        /// </summary>
        /// <typeparam name="T">The underlying type of the sequence</typeparam>
        /// <typeparam name="TResult">The type returned by the projection function</typeparam>
        /// <param name="enumerable">The sequence to be acted on</param>
        /// <param name="selector">A function 'T -> 'TResult to be applied to each member of the input sequence</param>
        /// <returns>A sequence of 'TResult</returns>
        public static IEnumerable<TResult> Map<T, TResult>(this IEnumerable<T> enumerable, Func<T, TResult> selector)
        {
            if (enumerable == null)
                throw new ArgumentNullException("enumerable");

            return enumerable.Select(selector);
        }

        /// <summary>
        /// Applies the given projection to each element of the sequence, using the item's index in the sequence.
        /// </summary>
        /// <typeparam name="T">The underlying type of the sequence</typeparam>
        /// <typeparam name="TResult">The type returned by the projection function</typeparam>
        /// <param name="enumerable">The sequence to be acted on</param>
        /// <param name="selector">A function ('T * int) -> 'TResult to be applied to each member of the input sequence</param>
        /// <returns>A sequence of 'TResult</returns>
        public static IEnumerable<TResult> Mapi<T, TResult>(this IEnumerable<T> enumerable, Func<T, int, TResult> selector)
        {
            if (enumerable == null)
                throw new ArgumentNullException("enumerable");

            return enumerable.Select(selector);
        }

        /// <summary>
        /// Applies the given projection to each set of elements of the two input sequences.
        /// If the sequences differ in length, the resulting sequence will be the length of the shorter.
        /// </summary>
        /// <typeparam name="T">The underlying type of the source sequence</typeparam>
        /// <typeparam name="T2">The underlying type of the second sequence</typeparam>
        /// <typeparam name="TResult">The type returned by the projection function</typeparam>
        /// <param name="enumerable">The initial input sequence to be acted on</param>
        /// <param name="other">The second input sequence to be acted on</param>
        /// <param name="selector">A function ('T * 'T2) -> 'TResult to be applied to each member of the input sequence</param>
        /// <returns>A sequence of 'TResult</returns>
        public static IEnumerable<TResult> Map2<T, T2, TResult>(this IEnumerable<T> enumerable, IEnumerable<T2> other, Func<T, T2, TResult> selector)
        {
            if (enumerable == null)
                throw new ArgumentNullException("enumerable");
            if (other == null)
                throw new ArgumentNullException("other");

            return enumerable.Zip(other, selector);
        }

        #endregion

        #region Fold / Reduce

        /// <summary>
        /// Applies the given accumulator function over the sequence, yielding an aggregated value.
        /// </summary>
        /// <typeparam name="T">The underlying type of the input sequence</typeparam>
        /// <param name="enumerable">The sequence to be acted on</param>
        /// <param name="func">An accumulating function ('T * 'T) -> 'T to be executed on all elements of the sequence</param>
        /// <returns>The value resulting from application of the accumulator over the elements of the sequence</returns>
        public static T Reduce<T>(this IEnumerable<T> enumerable, Func<T, T, T> func)
        {
            if (enumerable == null)
                throw new ArgumentNullException("enumerable");
            if (func == null)
                throw new ArgumentNullException("func");

            return enumerable.Aggregate(func);
        }

        /// <summary>
        /// Applies the given accumulator function over the sequence, using the initial value to yield an aggregated value.
        /// </summary>
        /// <typeparam name="T">The underlying type of the input sequence</typeparam>
        /// <typeparam name="TResult">The return type of the accumulating function</typeparam>
        /// <param name="enumerable">The sequence to be acted on</param>
        /// <param name="initval">The initial value to be placed in the accumulator.</param>
        /// <param name="func">An accumulating function ('T * 'T) -> 'T to be executed on all elements of the sequence</param>
        /// <returns>The value resulting from application of the accumulator over the elements of the sequence, using the given initial value</returns>
        public static TResult Reduce<T, TResult>(this IEnumerable<T> enumerable, TResult initval, Func<TResult, T, TResult> func)
        {
            if (enumerable == null)
                throw new ArgumentNullException("enumerable");
            if (func == null)
                throw new ArgumentNullException("func");

            return enumerable.Aggregate(initval, func);
        }

        /// <summary>
        /// Applies the given accumulator function over the sequence, yielding an aggregated value.
        /// </summary>
        /// <typeparam name="T">The underlying type of the input sequence</typeparam>
        /// <param name="enumerable">The sequence to be acted on</param>
        /// <param name="func">An accumulating function ('T * 'T) -> 'T to be executed on all elements of the sequence</param>
        /// <returns>The value resulting from application of the accumulator over the elements of the sequence</returns>
        public static T Fold<T>(this IEnumerable<T> enumerable, Func<T, T, T> func)
        {
            if (enumerable == null)
                throw new ArgumentNullException("enumerable");
            if (func == null)
                throw new ArgumentNullException("func");

            return Reduce(enumerable, func);
        }

        /// <summary>
        /// Applies the given accumulator function over the sequence, using the initial value to yield an aggregated value.
        /// </summary>
        /// <typeparam name="T">The underlying type of the input sequence</typeparam>
        /// <typeparam name="TResult">The return type of the accumulating function</typeparam>
        /// <param name="enumerable">The sequence to be acted on</param>
        /// <param name="initval">The initial value to be placed in the accumulator.</param>
        /// <param name="func">An accumulating function ('T * 'T) -> 'T to be executed on all elements of the sequence</param>
        /// <returns>The value resulting from application of the accumulator over the elements of the sequence, using the given initial value</returns>
        public static TResult Fold<T, TResult>(this IEnumerable<T> enumerable, TResult initval, Func<TResult, T, TResult> func)
        {
            if (enumerable == null)
                throw new ArgumentNullException("enumerable");
            if (func == null)
                throw new ArgumentNullException("func");

            return Reduce(enumerable, initval, func);
        }

        /// <summary>
        /// Applies the given accumulator function over the sequence, yielding an aggregated value.
        /// </summary>
        /// <typeparam name="T">The underlying type of the input sequence</typeparam>
        /// <param name="enumerable">The sequence to be acted on</param>
        /// <param name="func">An accumulating function ('T * 'T) -> 'T to be executed on all elements of the sequence</param>
        /// <returns>The value resulting from application of the accumulator over the elements of the sequence</returns>
        public static T FoldL<T>(this IEnumerable<T> enumerable, Func<T, T, T> func)
        {
            if (enumerable == null)
                throw new ArgumentNullException("enumerable");
            if (func == null)
                throw new ArgumentNullException("func");

            return Reduce(enumerable, func);
        }

        /// <summary>
        /// Applies the given accumulator function over the sequence, using the initial value to yield an aggregated value.
        /// </summary>
        /// <typeparam name="T">The underlying type of the input sequence</typeparam>
        /// <typeparam name="TResult">The return type of the accumulating function</typeparam>
        /// <param name="enumerable">The sequence to be acted on</param>
        /// <param name="initval">The initial value to be placed in the accumulator.</param>
        /// <param name="func">An accumulating function ('T * 'T) -> 'T to be executed on all elements of the sequence</param>
        /// <returns>The value resulting from application of the accumulator over the elements of the sequence, using the given initial value</returns>
        public static TResult FoldL<T, TResult>(this IEnumerable<T> enumerable, TResult initval, Func<TResult, T, TResult> func)
        {
            if (enumerable == null)
                throw new ArgumentNullException("enumerable");
            if (func == null)
                throw new ArgumentNullException("func");

            return Reduce(enumerable, initval, func);
        }

        /// <summary>
        /// Applies the given accumulator function over the sequence in reverse, yielding an aggregated value.
        /// </summary>
        /// <typeparam name="T">The underlying type of the input sequence</typeparam>
        /// <param name="enumerable">The sequence to be acted on</param>
        /// <param name="func">An accumulating function ('T * 'T) -> 'T to be executed on all elements of the sequence</param>
        /// <returns>The value resulting from application of the accumulator over the elements of the sequence in reverse</returns>
        public static T FoldR<T>(this IEnumerable<T> enumerable, Func<T, T, T> func)
        {
            if (enumerable == null)
                throw new ArgumentNullException("enumerable");
            if (func == null)
                throw new ArgumentNullException("func");

            return enumerable.Reverse().Aggregate(func);
        }

        /// <summary>
        /// Applies the given accumulator function over the sequence in reverse, using the initial value to yield an aggregated value.
        /// </summary>
        /// <typeparam name="T">The underlying type of the input sequence</typeparam>
        /// <typeparam name="TResult">The return type of the accumulating function</typeparam>
        /// <param name="enumerable">The sequence to be acted on</param>
        /// <param name="initval">The initial value to be placed in the accumulator.</param>
        /// <param name="func">An accumulating function ('T * 'T) -> 'T to be executed on all elements of the sequence in reverse</param>
        /// <returns>The value resulting from application of the accumulator over the elements of the sequence in reverse, using the given initial value</returns>
        public static TResult FoldR<T, TResult>(this IEnumerable<T> enumerable, TResult initval, Func<TResult, T, TResult> func)
        {
            if (enumerable == null)
                throw new ArgumentNullException("enumerable");
            if (func == null)
                throw new ArgumentNullException("func");

            return enumerable.Reverse().Aggregate(initval, func);
        }

        #endregion

        /// <summary>
        /// Applies the given predicate to each element in the input sequence, yielding a new 
        /// sequence of any elements for which it returns true.
        /// </summary>
        /// <typeparam name="T">The underlying type of the input sequence</typeparam>
        /// <param name="enumerable">The sequence to be filtered</param>
        /// <param name="pred">A function 'T -> bool used to filter the sequence</param>
        /// <returns>A sequence based on the input, containing elements for which the predicate returns true</returns>
        public static IEnumerable<T> Filter<T>(this IEnumerable<T> enumerable, Func<T, bool> pred)
        {
            if (enumerable == null)
                throw new ArgumentNullException("enumerable");
            if (pred == null)
                throw new ArgumentNullException("pred");

            return enumerable.Where(pred);
        }

        /// <summary>
        /// Returns the first element in the sequence for which the given predicate evaluates to true.
        /// Throws an exception if the function yields false for all elements.
        /// </summary>
        /// <typeparam name="T">The underlying type of the sequence</typeparam>
        /// <param name="enumerable">The sequence to be acted on</param>
        /// <param name="pred">A function 'T -> bool used to evaluate against each element</param>
        /// <returns>The first element in the sequence for which the predicate is true.</returns>
        public static T Find<T>(this IEnumerable<T> enumerable, Func<T, bool> pred)
        {
            if (enumerable == null)
                throw new ArgumentNullException("enumerable");
            if (pred == null)
                throw new ArgumentNullException("pred");

            return enumerable.First(pred);
        }

        /// <summary>
        /// Returns the first item in the given sequence.
        /// </summary>
        /// <typeparam name="T">The underlying type of the sequence</typeparam>
        /// <param name="enumerable">The source sequence</param>
        /// <returns>The first item in the sequence.</returns>
        public static T Head<T>(this IEnumerable<T> enumerable)
        {
            if (enumerable == null)
                throw new ArgumentNullException("enumerable");

            return enumerable.First();
        }
    }
}
