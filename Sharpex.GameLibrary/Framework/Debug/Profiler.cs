using System;
using System.Diagnostics;

namespace SharpexGL.Framework.Debug
{
    public class Profiler
    {
        /// <summary>
        /// Initializes a new Profiler class.
        /// </summary>
        public Profiler()
        {
            
        }

        /// <summary>
        /// Profiles an action.
        /// </summary>
        /// <param name="guid">The Guid.</param>
        /// <param name="action">The Action.</param>
        public void Profile(Guid guid, Action action)
        {
            var sw = new Stopwatch();
            Console.WriteLine(@"Profiling: " + guid);
            sw.Start();
            action.Invoke();
            sw.Stop();
            Console.WriteLine(@"End profiling: " + guid + @" Time: " + sw.ElapsedMilliseconds + @"ms");
            sw.Reset();
        }
    }
}
