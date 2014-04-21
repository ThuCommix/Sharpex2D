using System;
using System.Diagnostics;
using Log = Sharpex2D.Framework.Debug.Logging.Log;

namespace Sharpex2D.Framework.Debug
{
    public class Profiler
    {
        /// <summary>
        /// Profiles an action.
        /// </summary>
        /// <param name="guid">The Guid.</param>
        /// <param name="action">The Action.</param>
        public static void Profile(Guid guid, Action action)
        {
            var sw = new Stopwatch();
            Log.Next(@"Profiling: " + guid, Logging.LogLevel.Info, Logging.LogMode.StandardOut);
            sw.Start();
            action.Invoke();
            sw.Stop();
            Log.Next(@"End profiling: " + guid + @" Time: " + sw.ElapsedMilliseconds + @"ms", Logging.LogLevel.Info, Logging.LogMode.StandardOut);
            sw.Reset();
        }
    }
}
