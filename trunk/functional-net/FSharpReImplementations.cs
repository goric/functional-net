using System;
using System.Collections;
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
            using (var enum1 = first.GetEnumerator())
            using (var enum2 = second.GetEnumerator())
                while (enum1.MoveNext() && enum2.MoveNext())
                    if (pred(enum1.Current, enum2.Current))
                        return true;
            return false;
        }

        /// <summary>
        /// Invokes the given action on each element of the sequence.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable"></param>
        /// <param name="action">The action ('T -> unit) to be invoked for each element</param>
        public static void Iter<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            foreach (var item in enumerable)
            {
                action(item);
            }
        }
        /// <summary>
        /// Invokes the given action on each pair of elements in the sequences.
        /// If sequences are of differing length, they are enumerated until the shorter is exhausted.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <param name="action">The action ('T * 'T2 -> unit) to be invoked for each element</param>
        public static void Iter2<T, T2>(this IEnumerable<T> first, IEnumerable<T2> second, Action<T, T2> action)
        {
            using (var enum1 = first.GetEnumerator())
            using (var enum2 = second.GetEnumerator())
                while (enum1.MoveNext() && enum2.MoveNext())
                    action(enum1.Current, enum2.Current);
        }

        /// <summary>
        /// Returns the nth item in the sequence (0-based). 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable"></param>
        /// <param name="idx"></param>
        /// <returns></returns>
        public static T Nth<T>(this IEnumerable<T> enumerable, int idx)
        {
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
        /*
        public static IEnumerable<T[]> Windowed<T>(this IEnumerable<T> enumerable, int windowSize)
        {
            var prev = new T[windowSize];

            using(var enumer = enumerable.GetEnumerator())
            {
                while(enumer.MoveNext())
                {
                    
                }
            }
        }
        */
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
            return Unfold(p => Option.Some(Tuple.Create(func(p), p + 1)), 0);
        }

        public static IEnumerable<TResult> Collect<T, TResult> (this IEnumerable<T> enumerable,  Func<T, IEnumerable<TResult>> func)
        {
            return enumerable.SelectMany(func);
        }
    }
}
