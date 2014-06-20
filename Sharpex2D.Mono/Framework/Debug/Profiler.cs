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
        /// <summary>
        ///     Profiles an action.
        /// </summary>
        /// <param name="guid">The Guid.</param>
        /// <param name="action">The Action.</param>
        public static void Profile(Guid guid, Action action)
        {
            var sw = new Stopwatch();
            Log.Next(@"Profiling: " + guid, LogLevel.Info, LogMode.StandardOut);
            sw.Start();
            action.Invoke();
            sw.Stop();
            Log.Next(@"End profiling: " + guid + @" Time: " + sw.ElapsedMilliseconds + @"ms", LogLevel.Info,
                LogMode.StandardOut);
            sw.Reset();
        }
    }
}