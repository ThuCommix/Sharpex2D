using System;
using System.Collections.Generic;
using SharpexGL.Framework.Common.Extensions;
using SharpexGL.Framework.Components;

namespace SharpexGL.Framework.Common.Debug
{
    public class Log : IComponent
    {

        #region IComponent Implementation
        /// <summary>
        /// Gets the guid.
        /// </summary>
        public Guid Guid { get { return new Guid("EF851384-11D8-40D0-9049-240AAD033045"); } }
        #endregion

        /// <summary>
        /// Initializes a new Log class.
        /// </summary>
        public Log()
        {
            _entries = new List<LogEntry>();
            SGL.Components.AddComponent(this);
        }

        private readonly List<LogEntry> _entries;

        /// <summary>
        /// Writes a new log entry.
        /// </summary>
        /// <param name="message">The Message.</param>
        /// <param name="level">The Level.</param>
        /// <param name="mode">The Mode.</param>
        public void Next(string message, LogLevel level, LogMode mode)
        {
            _entries.Add(new LogEntry(message, level) {Mode = mode, Time = DateTime.Now});

            if (mode == LogMode.StandardOut)
            {
                Console.WriteLine(_entries[_entries.Count - 1].Time.ToLongTimeString() + @" [" + level.ToFriendlyString() + @"]: " + message);
            }

            if (mode == LogMode.StandardError)
            {
                Console.Error.WriteLine(_entries[_entries.Count - 1].Time.ToLongTimeString() + @" [" + level.ToFriendlyString() + @"]: " + message);
            }
        }
        /// <summary>
        /// Writes a new log entry.
        /// </summary>
        /// <param name="message">The Message.</param>
        /// <param name="level">The Level.</param>
        public void Next(string message, LogLevel level)
        {
            Next(message, level, LogMode.None);
        }
        /// <summary>
        /// Gets all entries.
        /// </summary>
        /// <returns>Array of LogEntry</returns>
        public LogEntry[] GetEntries()
        {
            return _entries.ToArray();
        }
    }
}
