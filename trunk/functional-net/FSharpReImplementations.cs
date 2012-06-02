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
    /// Re-implementations of most of the functions available on Seq in F#
    /// </summary>
    public static class FSharpReImplementations
    {
        /// <summary>
        /// Asserts whether the given function yields true for all elements of the sequence.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable"></param>
        /// <param name="pred">The predicate ('T -> bool) to be applied to each element</param>
        /// <returns></returns>
        public static bool Forall<T>(this IEnumerable<T> enumerable, Func<T, bool> pred)
        {
            if (enumerable == null)
                throw new ArgumentNullException("enumerable");

            return enumerable.All(pred);
        }
        /// <summary>
        /// Asserts whether the given function yields true for all elements of both sequences. 
        /// If sequences are of differing length, they are enumerated until the shorter is exhausted.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <param name="pred">The predicate ('T * 'T2 -> bool) to be applied to each set of elements</param>
        /// <returns></returns>
        public static bool Forall2<T, T2>(this IEnumerable<T> first, IEnumerable<T2> second, Func<T, T2, bool> pred)
        {
            if (first == null)
                throw new ArgumentNullException("first");
            if (second == null)
                throw new ArgumentNullException("second");

            using (var enum1 = first.GetEnumerator())
            using (var enum2 = second.GetEnumerator())
                while (enum1.MoveNext() && enum2.MoveNext())
                    if (!pred(enum1.Current, enum2.Current))
                        return false;
            return true;
        }

        /// <summary>
        /// Asserts whether the given function yields true for any element in the sequence.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable"></param>
        /// <param name="pred">The predicate ('T -> bool) to be applied to each element</param>
        /// <returns></returns>
        public static bool Exists<T>(this IEnumerable<T> enumerable, Func<T, bool> pred)
        {
            if (enumerable == null)
                throw new ArgumentNullException("enumerable");

            return enumerable.Any(pred);
        }
        /// <summary>
        /// Asserts whether the given function yields true for any pair of elements in the sequences.
        /// If sequences are of differing length, they are enumerated until the shorter is exhausted.
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <param name="pred">The predicate ('T * 'T2 -> bool) to be applied to each set of elements</param>
        /// <returns></returns>
        public static bool Exists2<T1, T2>(this IEnumerable<T1> first, IEnumerable<T2> second, Func<T1, T2, bool> pred)
        {
            if (first == null)
                throw new ArgumentNullException("first");
            if (second == null)
                throw new ArgumentNullException("second");

            using (var enum1 = first.GetEnumerator())
            using (var enum2 = second.GetEnumerator())
                while (enum1.MoveNext() && enum2.MoveNext())
                    if (pred(enum1.Current, enum2.Current))
                        return true;
            return false;
        }

        /// <summary>
        /// Invokes the given action on each element of the sequence. Executes immediately.
        /// </summary>
        /// <typeparam name="T">The underlying type of the sequence</typeparam>
        /// <param name="enumerable">The sequence to be acted on</param>
        /// <param name="action">The action ('T -> unit) to be invoked for each element</param>
        public static void Iter<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            if (enumerable == null)
                throw new ArgumentNullException("enumerable");

            foreach (var item in enumerable)
            {
                action(item);
            }
        }
        /// <summary>
        /// Invokes the given action on each pair of elements in the sequences. Executes immediately.
        /// If sequences are of differing length, they are enumerated until the shorter is exhausted.
        /// </summary>
        /// <typeparam name="T">The underlying type of the first sequence</typeparam>
        /// <typeparam name="T2">The underlying type of the second sequence</typeparam>
        /// <param name="first">The sequence to be acted on</param>
        /// <param name="second"></param>
        /// <param name="action">The action ('T * 'T2 -> unit) to be invoked for each element</param>
        public static void Iter2<T, T2>(this IEnumerable<T> first, IEnumerable<T2> second, Action<T, T2> action)
        {
            if (first == null)
                throw new ArgumentNullException("first");
            if (second == null)
                throw new ArgumentNullException("second");

            using (var enum1 = first.GetEnumerator())
            using (var enum2 = second.GetEnumerator())
                while (enum1.MoveNext() && enum2.MoveNext())
                    action(enum1.Current, enum2.Current);
        }

        /// <summary>
        /// Returns the nth item in the sequence (0-based). If the index is greater than the length of
        /// the sequence, the default value of <typeparamref name="T"/> is returned.
        /// </summary>
        /// <typeparam name="T">The underlying type of the sequence</typeparam>
        /// <param name="enumerable">The sequence to be acted on</param>
        /// <param name="idx">The index of the sequence to be returned (0-based)</param>
        /// <returns></returns>
        public static T Nth<T>(this IEnumerable<T> enumerable, int idx)
        {
            if (enumerable == null)
                throw new ArgumentNullException("enumerable");

            if (enumerable is IList<T>)
            {
                var asList = (IList<T>) enumerable;
                return asList.Count <= idx ? default(T) : asList[idx];
            }

            var index = 0;
            using (var enumer = enumerable.GetEnumerator())
            {
                while (enumer.MoveNext())
                {
                    if (index == idx)
                        return enumer.Current;
                    index++;
                }
            }
            return default(T);
        }

        /// <summary>
        /// Chops the sequence to the given length. 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IEnumerable<T> Truncate<T>(this IEnumerable<T> enumerable, int count)
        {
            if (enumerable == null)
                throw new ArgumentNullException("enumerable");

            return TruncateImpl(enumerable, count);
        }
        private static IEnumerable<T> TruncateImpl<T>(this IEnumerable<T> enumerable, int count)
        {
            var index = 0;
            using (var enumer = enumerable.GetEnumerator())
            {
                while (enumer.MoveNext())
                {
                    if (index <= count)
                        yield return enumer.Current;
                    else
                        yield break;
                    index++;
                }
            }
        }

        public static IEnumerable<Tuple<T, T>> Pairwise<T>(this IEnumerable<T> enumerable)
        {
            if (enumerable == null)
                throw new ArgumentNullException("enumerable");

            return PairwiseImpl(enumerable);
        }
        private static IEnumerable<Tuple<T, T>> PairwiseImpl<T>(this IEnumerable<T> enumerable)
        {
            var prev = default(T);

            using (var enumer = enumerable.GetEnumerator())
            {
                if (enumer.MoveNext())
                    prev = enumer.Current;

                while (enumer.MoveNext())
                {
                    yield return new Tuple<T, T>(prev, enumer.Current);
                    prev = enumer.Current;
                }
            }
        }
        
        public static IEnumerable<T[]> Windowed<T>(this IEnumerable<T> enumerable, int windowSize)
        {
            if (enumerable == null)
                throw new ArgumentNullException("enumerable");

            return WindowedImpl(enumerable, windowSize);
        }
        private static IEnumerable<T[]> WindowedImpl<T>(this IEnumerable<T> enumerable, int windowSize)
        {
            var arr = new T[windowSize];

            var remaining = windowSize - 1;
            var i = 0;

            foreach (var item in enumerable)
            {
                arr[i] = item;
                i = (i + 1) % windowSize;

                if (remaining != 0)
                {
                    remaining--;
                    continue;
                }

                var retArray = new T[windowSize];
                for (var j = 0; j < windowSize; j++)
                    retArray[j] = arr[(i + j) % windowSize];
                yield return retArray;
            }
        }
        
        /// <summary>
        /// Zips the elements of each list into a tuple ('T * 'T2), and returns a sequence of the tuple elements.
        /// If sequences are of differing length, they are enumerated until the shorter is exhausted.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public static IEnumerable<Tuple<T, T2>> Zip<T, T2>(this IEnumerable<T> first, IEnumerable<T2> second)
        {
            if (first == null)
                throw new ArgumentNullException("first");
            if (second == null)
                throw new ArgumentNullException("second");

            return ZipImpl(first, second);
        }
        private static IEnumerable<Tuple<T, T2>> ZipImpl<T, T2>(this IEnumerable<T> first, IEnumerable<T2> second)
        {
            using (var enum1 = first.GetEnumerator())
            using (var enum2 = second.GetEnumerator())
                while (enum1.MoveNext() && enum2.MoveNext())
                    yield return new Tuple<T, T2>(enum1.Current, enum2.Current);
        }

        /// <summary>
        /// Zips the elements of each list into a tuple ('T * 'T2 * 'T3), and returns a sequence of the tuple elements.
        /// If sequences are of differing length, they are enumerated until the shortest is exhausted.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <param name="third"></param>
        /// <returns></returns>
        public static IEnumerable<Tuple<T, T2, T3>> Zip3<T, T2, T3>(this IEnumerable<T> first, IEnumerable<T2> second, IEnumerable<T3> third)
        {
            if (first == null)
                throw new ArgumentNullException("first");
            if (second == null)
                throw new ArgumentNullException("second");
            if (third == null)
                throw new ArgumentNullException("third");

            return Zip3Impl(first, second, third);
        }
        private static IEnumerable<Tuple<T, T2, T3>> Zip3Impl<T, T2, T3>(this IEnumerable<T> first, IEnumerable<T2> second, IEnumerable<T3> third)
        {
            using (var enum1 = first.GetEnumerator())
            using (var enum2 = second.GetEnumerator())
            using (var enum3 = third.GetEnumerator())
                while (enum1.MoveNext() && enum2.MoveNext() && enum3.MoveNext())
                    yield return new Tuple<T, T2, T3>(enum1.Current, enum2.Current, enum3.Current);
        }

        /// <summary>
        /// Returns a single-element sequence of the given type, with the lone elemnt being the default value of the type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IEnumerable<T> Singleton<T>()
        {
            yield return default(T);
        }

        /*
         *      var fibs= Unfold(x => Option.Some(Tuple.Create(x.Item1, Tuple.Create(x.Item2, x.Item1 + x.Item2))), Tuple.Create(1, 1));
         *      var first20 = results.Take(20);
         */
        public static IEnumerable<TResult> Unfold<T, TResult>(Func<T, Option<Tuple<TResult, T>>> generator, T start)
        {
            if (generator == null)
                throw new ArgumentNullException("generator");

            return UnfoldImpl(generator, start);
        }
        private static IEnumerable<TResult> UnfoldImpl<T, TResult>(Func<T, Option<Tuple<TResult, T>>> generator, T start)
        {
            var next = start;

            while (true)
            {
                var result = generator(next);
                if (result.IsNone)
                    yield break;

                yield return result.Value.Item1;

                next = result.Value.Item2;
            }
        }

        /// <summary>
        /// Creates a lazy infinite sequence to be streamed from.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="func"></param>
        /// <returns></returns>
        public static IEnumerable<T> InitializeInfinite<T>(Func<int, T> func)
        {
            if (func == null)
                throw new ArgumentNullException("func");

            return Unfold(p => Option.Some(Tuple.Create(func(p), p + 1)), 0);
        }

        /// <summary>
        /// Applies the given projection to each element in the source enumerable, then flattens the resulting sequences 
        /// into a single sequence.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="enumerable"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static IEnumerable<TResult> Collect<T, TResult> (this IEnumerable<T> enumerable,  Func<T, IEnumerable<TResult>> func)
        {
            if (enumerable == null)
                throw new ArgumentNullException("enumerable");
            if (func == null)
                throw new ArgumentNullException("func");

            return enumerable.SelectMany(func);
        }
    }
}
