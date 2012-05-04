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
        public static bool Forall<T>(this IEnumerable<T> enumerable, Func<T, bool> pred)
        {
            return enumerable.All(pred);
        }
        public static bool Forall2<T, T2>(this IEnumerable<T> first, IEnumerable<T2> second, Func<T, T2, bool> pred)
        {
            using (var enum1 = first.GetEnumerator())
            using (var enum2 = second.GetEnumerator())
                while (enum1.MoveNext() && enum2.MoveNext())
                    if (!pred(enum1.Current, enum2.Current))
                        return false;
            return true;
        }

        public static bool Exists<T>(this IEnumerable<T> enumerable, Func<T, bool> pred)
        {
            return enumerable.Any(pred);
        }
        public static bool Exists2<T1, T2>(this IEnumerable<T1> first, IEnumerable<T2> second, Func<T1, T2, bool> pred)
        {
            using (var enum1 = first.GetEnumerator())
            using (var enum2 = second.GetEnumerator())
                while (enum1.MoveNext() && enum2.MoveNext())
                    if (pred(enum1.Current, enum2.Current))
                        return true;
            return false;
        }

        public static void Iter<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            foreach (var item in enumerable)
            {
                action(item);
            }
        }
        public static void Iter2<T, T2>(this IEnumerable<T> first, IEnumerable<T2> second, Action<T, T2> action)
        {
            using (var enum1 = first.GetEnumerator())
            using (var enum2 = second.GetEnumerator())
                while (enum1.MoveNext() && enum2.MoveNext())
                    action(enum1.Current, enum2.Current);
        }

        public static T Nth<T>(this IEnumerable<T> enumerable, int idx)
        {
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

        public static IEnumerable<Tuple<T, T2>> Zip<T, T2>(this IEnumerable<T> first, IEnumerable<T2> second)
        {
            using (var enum1 = first.GetEnumerator())
            using (var enum2 = second.GetEnumerator())
                while (enum1.MoveNext() && enum2.MoveNext())
                    yield return new Tuple<T, T2>(enum1.Current, enum2.Current);
        }

        public static IEnumerable<Tuple<T, T2, T3>> Zip3<T, T2, T3>(this IEnumerable<T> first, IEnumerable<T2> second, IEnumerable<T3> third)
        {
            using (var enum1 = first.GetEnumerator())
            using (var enum2 = second.GetEnumerator())
            using (var enum3 = third.GetEnumerator())
                while (enum1.MoveNext() && enum2.MoveNext() && enum3.MoveNext())
                    yield return new Tuple<T, T2, T3>(enum1.Current, enum2.Current, enum3.Current);
        }

        public static IEnumerable<T> Singleton<T>()
        {
            yield return default(T);
        }
    }
}
