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
    public static class Currying
    {
        /// <summary>
        /// Converts a function taking n parameters into a chain of functions, each taking a single parameter and
        /// returning a subsequent function taking 1 parameter. The final function in the chain will take one 
        /// parameter and return the result of the original function, with all given parameters applied.
        /// A function ('T * 'T2 *" 'T3) -> 'TResult, curried, will be converted into 'T1 -> 'T2 -> 'T3 -> 'TResult.
        /// </summary>
        public static Func<T1, Func<T2, TResult>> Curry<T1, T2, TResult>(this Func<T1, T2, TResult> func)
        {
            return x => y => func(x, y);
        }

        /// <summary>
        /// Converts a function taking n parameters into a chain of functions, each taking a single parameter and
        /// returning a subsequent function taking 1 parameter. The final function in the chain will take one 
        /// parameter and return the result of the original function, with all given parameters applied.
        /// A function ('T * 'T2 *" 'T3) -> 'TResult, curried, will be converted into 'T1 -> 'T2 -> 'T3 -> 'TResult.
        /// </summary>
        public static Func<T1, Func<T2, Func<T3, TResult>>> Curry<T1, T2, T3, TResult>(this Func<T1, T2, T3, TResult> func)
        {
            return x => y => z => func(x, y, z);
        }

        /// <summary>
        /// Converts a function taking n parameters into a chain of functions, each taking a single parameter and
        /// returning a subsequent function taking 1 parameter. The final function in the chain will take one 
        /// parameter and return the result of the original function, with all given parameters applied.
        /// A function ('T * 'T2 *" 'T3) -> 'TResult, curried, will be converted into 'T1 -> 'T2 -> 'T3 -> 'TResult.
        /// </summary>
        public static Func<T1, Func<T2, Func<T3, Func<T4,TResult>>>> Curry<T1, T2, T3, T4, TResult>(this Func<T1, T2, T3, T4, TResult> func)
        {
            return w => x => y => z => func(w, x, y, z);
        }

        /// <summary>
        /// Converts a function taking n parameters into a chain of functions, each taking a single parameter and
        /// returning a subsequent function taking 1 parameter. The final function in the chain will take one 
        /// parameter and return the result of the original function, with all given parameters applied.
        /// A function ('T * 'T2 *" 'T3) -> 'TResult, curried, will be converted into 'T1 -> 'T2 -> 'T3 -> 'TResult.
        /// </summary>
        public static Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, TResult>>>>> Curry<T1, T2, T3, T4, T5, TResult>(this Func<T1, T2, T3, T4, T5, TResult> func)
        {
            return v => w => x => y => z => func(v, w, x, y, z);
        }

        /// <summary>
        /// Converts a function taking n parameters into a chain of functions, each taking a single parameter and
        /// returning a subsequent function taking 1 parameter. The final function in the chain will take one 
        /// parameter and return the result of the original function, with all given parameters applied.
        /// A function ('T * 'T2 *" 'T3) -> 'TResult, curried, will be converted into 'T1 -> 'T2 -> 'T3 -> 'TResult.
        /// </summary>
        public static Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, Func<T6, TResult>>>>>> Curry<T1, T2, T3, T4, T5, T6, TResult>(this Func<T1, T2, T3, T4, T5, T6, TResult> func)
        {
            return u => v => w => x => y => z => func(u, v, w, x, y, z);
        }

        /// <summary>
        /// Converts a function taking n parameters into a chain of functions, each taking a single parameter and
        /// returning a subsequent function taking 1 parameter. The final function in the chain will take one 
        /// parameter and return the result of the original function, with all given parameters applied.
        /// A function ('T * 'T2 *" 'T3) -> 'TResult, curried, will be converted into 'T1 -> 'T2 -> 'T3 -> 'TResult.
        /// </summary>
        public static Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, Func<T6, Func<T7, TResult>>>>>>> Curry<T1, T2, T3, T4, T5, T6, T7, TResult>(this Func<T1, T2, T3, T4, T5, T6, T7, TResult> func)
        {
            return t => u => v => w => x => y => z => func(t, u, v, w, x, y, z);
        }

        /// <summary>
        /// Converts a function taking n parameters into a chain of functions, each taking a single parameter and
        /// returning a subsequent function taking 1 parameter. The final function in the chain will take one 
        /// parameter and return the result of the original function, with all given parameters applied.
        /// A function ('T * 'T2 *" 'T3) -> 'TResult, curried, will be converted into 'T1 -> 'T2 -> 'T3 -> 'TResult.
        /// </summary>
        public static Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, Func<T6, Func<T7, Func<T8, TResult>>>>>>>> Curry<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(this Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult> func)
        {
            return s => t => u => v => w => x => y => z => func(s, t, u, v, w, x, y, z);
        }
    }
}
