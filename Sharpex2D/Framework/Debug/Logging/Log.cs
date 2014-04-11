using System;
using System.Collections.Generic;
using Sharpex2D.Framework.Common.Extensions;

namespace Sharpex2D.Framework.Debug.Logging
{
    public static class Log
    {
        /// <summary>
        /// Initializes a new Log class.
        /// </summary>
        static Log()
        {
            Entries = new List<LogEntry>();
        }

        private static readonly List<LogEntry> Entries;

        /// <summary>
        /// Writes a new log entry.
        /// </summary>
        /// <param name="message">The Message.</param>
        /// <param name="level">The Level.</param>
        /// <param name="mode">The Mode.</param>
        public static void Next(string message, LogLevel level, LogMode mode)
        {
            Entries.Add(new LogEntry(message, level) {Mode = mode, Time = DateTime.Now});

            if (mode == LogMode.StandardOut)
            {
                Console.WriteLine(Entries[Entries.Count - 1].Time.ToLongTimeString() + @" [" +
                                  level.ToFriendlyString() + @"]: " + message);
            }

            if (mode == LogMode.StandardError)
            {
                Console.Error.WriteLine(Entries[Entries.Count - 1].Time.ToLongTimeString() + @" [" +
                                        level.ToFriendlyString() + @"]: " + message);
            }
        }

        /// <summary>
        /// Writes a new log entry.
        /// </summary>
        /// <param name="message">The Message.</param>
        /// <param name="level">The Level.</param>
        public static void Next(string message, LogLevel level)
        {
            Next(message, level, LogMode.None);
        }

        /// <summary>
        /// Gets all entries.
        /// </summary>
        /// <returns>Array of LogEntry</returns>
        public static LogEntry[] GetEntries()
        {
            return Entries.ToArray();
        }
    }
}
