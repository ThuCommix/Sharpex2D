using System;
using System.Diagnostics;
using Sharpex2D.Framework.Debug.Logging;

namespace Sharpex2D.Framework.Debug
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Tested)]
    public class Profiler
    {
        private static readonly Logger Logger;

        /// <summary>
        /// Initializes a  new Profiler class.
        /// </summary>
        static Profiler()
        {
            Logger = LogManager.GetClassLogger();
        }

        /// <summary>
        ///     Profiles an action.
        /// </summary>
        /// <param name="guid">The Guid.</param>
        /// <param name="action">The Action.</param>
        public static void Profile(Guid guid, Action action)
        {
            var sw = new Stopwatch();
            Logger.Info("Profiling: {0}.", guid);
            sw.Start();
            action.Invoke();
            sw.Stop();
            Logger.Info("End profiling: {0}, Time: {1}ms", guid, sw.ElapsedMilliseconds);
            sw.Reset();
        }
    }
}