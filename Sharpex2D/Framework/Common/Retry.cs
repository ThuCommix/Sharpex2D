using System;
using System.Threading;
using Sharpex2D.Framework.Debug.Logging;

namespace Sharpex2D.Framework.Common
{
    public static class Retry
    {
        /// <summary>
        /// Sets or gets the EnableErrorReport.
        /// </summary>
        public static bool EnableErrorReport { set; get; }

        /// <summary>
        /// Executes an action with unlimited retries.
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
        /// Executes a function with unlimited retries.
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
                        Log.Next(ex.Message, LogLevel.Warning, LogMode.StandardOut);
                    }
                    Thread.Sleep(timeout);
                }
            }
        }

        /// <summary>
        /// >Executes an action with limited retries.
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
        /// >Executes a function with limited retries.
        /// </summary>
        /// <param name="iterations">The Retries.</param>
        /// <param name="timeout">The Timeout.</param>
        /// <param name="func">The Function.</param>
        public static T Limited<T>(int iterations, TimeSpan timeout, Func<T> func)
        {
            if (iterations < 1) throw new ArgumentOutOfRangeException("iterations");

            for (var i = 1; i <= iterations; i++)
            {
                try
                {
                    return func();
                }
                catch (Exception ex)
                {
                    if (EnableErrorReport)
                    {
                        Log.Next(ex.Message, LogLevel.Warning, LogMode.StandardOut);
                    }
                    Thread.Sleep(timeout);
                }
            }
            return default(T);
        }
    }
}
