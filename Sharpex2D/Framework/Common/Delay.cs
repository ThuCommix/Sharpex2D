using System;
using System.Diagnostics;

namespace Sharpex2D.Framework.Common
{
    public static class Delay
    {
        /// <summary>
        /// Executes the action with the specified delay.
        /// </summary>
        /// <param name="delay">The Delay.</param>
        /// <param name="action">The Action.</param>
        public static void Execute(float delay, Action action)
        {
            Execute<object>(delay, () =>
            {
                action();
                return null;
            });
        }
        /// <summary>
        /// Executes the function with the specified delay.
        /// </summary>
        /// <typeparam name="T">The Type.</typeparam>
        /// <param name="delay">The Delay.</param>
        /// <param name="function">The Function.</param>
        /// <returns>T</returns>
        public static T Execute<T>(float delay, Func<T> function)
        {
            var sw = new Stopwatch();
            sw.Start();
            while (sw.ElapsedMilliseconds < delay) { }
            sw.Stop();

            return function();
        }
    }
}
