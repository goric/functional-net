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
