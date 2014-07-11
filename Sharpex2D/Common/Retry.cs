// Copyright (c) 2012-2014 Sharpex2D - Kevin Scholz (ThuCommix)
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the 'Software'), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED 'AS IS', WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using System;
using System.Threading;
using Sharpex2D.Debug.Logging;

namespace Sharpex2D.Common
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Tested)]
    public static class Retry
    {
        /// <summary>
        ///     Sets or gets the EnableErrorReport.
        /// </summary>
        public static bool EnableErrorReport { set; get; }

        /// <summary>
        ///     Executes an action with unlimited retries.
        /// </summary>
        /// <param name="timeout">The Timeout.</param>
        /// <param name="action">The Action.</param>
        public static void Unlimited(TimeSpan timeout, Action action)
        {
            Unlimited<object>(timeout, () =>
            {
                action();
                return null;
            });
        }

        /// <summary>
        ///     Executes a function with unlimited retries.
        /// </summary>
        /// <param name="timeout">The Timeout.</param>
        /// <param name="func">The Function.</param>
        public static T Unlimited<T>(TimeSpan timeout, Func<T> func)
        {
            while (true)
            {
                try
                {
                    return func();
                }
                catch (Exception ex)
                {
                    if (EnableErrorReport)
                    {
                        LogManager.GetClassLogger().Warn(ex.Message);
                    }
                    Thread.Sleep(timeout);
                }
            }
        }

        /// <summary>
        ///     >Executes an action with limited retries.
        /// </summary>
        /// <param name="iterations">The Retries.</param>
        /// <param name="timeout">The Timeout.</param>
        /// <param name="action">The Action.</param>
        public static void Limited(int iterations, TimeSpan timeout, Action action)
        {
            Limited<object>(iterations, timeout, () =>
            {
                action();
                return null;
            });
        }

        /// <summary>
        ///     >Executes a function with limited retries.
        /// </summary>
        /// <param name="iterations">The Retries.</param>
        /// <param name="timeout">The Timeout.</param>
        /// <param name="func">The Function.</param>
        public static T Limited<T>(int iterations, TimeSpan timeout, Func<T> func)
        {
            if (iterations < 1) throw new ArgumentOutOfRangeException("iterations");

            for (int i = 1; i <= iterations; i++)
            {
                try
                {
                    return func();
                }
                catch (Exception ex)
                {
                    if (EnableErrorReport)
                    {
                        LogManager.GetClassLogger().Warn(ex.Message);
                    }
                    Thread.Sleep(timeout);
                }
            }
            return default(T);
        }
    }
}