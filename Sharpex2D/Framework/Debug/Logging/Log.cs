using System;
using System.Collections.Generic;
using Sharpex2D.Framework.Common.Extensions;

namespace Sharpex2D.Framework.Debug.Logging
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Tested)]
    public static class Log
    {
        private static readonly List<LogEntry> Entries;

        /// <summary>
        ///     Initializes a new Log class.
        /// </summary>
        static Log()
        {
            Entries = new List<LogEntry>();
        }

        /// <summary>
        ///     Writes a new log entry.
        /// </summary>
        /// <param name="message">The Message.</param>
        /// <param name="level">The Level.</param>
        /// <param name="mode">The Mode.</param>
        public static void Next(string message, LogLevel level, LogMode mode)
        {
            Entries.Add(new LogEntry(message, level, DateTime.Now));

            if (mode == LogMode.StandardOut)
            {
                if (level == LogLevel.Critical)
                {
                    Console.WriteLine(Entries[Entries.Count - 1].Time.ToLongTimeString() + @" [" +
                                      level.ToFriendlyString() + @"]: " + message + Environment.NewLine +
                                      @"Stacktrace: " + Environment.NewLine + Environment.StackTrace);
                }
                else
                {
                    Console.WriteLine(Entries[Entries.Count - 1].Time.ToLongTimeString() + @" [" +
                                      level.ToFriendlyString() + @"]: " + message);
                }
            }

            if (mode == LogMode.StandardError)
            {
                if (level == LogLevel.Critical)
                {
                    Console.Error.WriteLine(Entries[Entries.Count - 1].Time.ToLongTimeString() + @" [" +
                                            level.ToFriendlyString() + @"]: " + message + Environment.NewLine +
                                            @"Stacktrace: " + Environment.NewLine + Environment.StackTrace);
                }
                else
                {
                    Console.Error.WriteLine(Entries[Entries.Count - 1].Time.ToLongTimeString() + @" [" +
                                            level.ToFriendlyString() + @"]: " + message);
                }
            }
        }

        /// <summary>
        ///     Writes a new log entry.
        /// </summary>
        /// <param name="message">The Message.</param>
        /// <param name="level">The Level.</param>
        public static void Next(string message, LogLevel level)
        {
            Next(message, level, LogMode.StandardOut);
        }

        /// <summary>
        ///     Writes a new log entry.
        /// </summary>
        /// <param name="format">The StringFormat.</param>
        /// <param name="level">The Level.</param>
        /// <param name="mode">The Mode.</param>
        /// <param name="args">The StringFormat Arguments.</param>
        public static void Next(string format, LogLevel level, LogMode mode, params object[] args)
        {
            Next(string.Format(format, args), level, mode);
        }

        /// <summary>
        ///     Writes a new log entry.
        /// </summary>
        /// <param name="format">The StringFormat.</param>
        /// <param name="level">The Level.</param>
        /// <param name="args">The StringFormat Arguments.</param>
        public static void Next(string format, LogLevel level, params object[] args)
        {
            Next(format, level, LogMode.StandardOut, args);
        }

        /// <summary>
        ///     Gets all entries.
        /// </summary>
        /// <returns>Array of LogEntry</returns>
        public static LogEntry[] GetEntries()
        {
            return Entries.ToArray();
        }
    }
}