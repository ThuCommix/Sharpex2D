
using System;

namespace SharpexGL.Framework.Debug
{
    public class LogEntry
    {
        /// <summary>
        /// Initializes a new LogEntry class.
        /// </summary>
        /// <param name="message">The Message.</param>
        /// <param name="level">The Level.</param>
        public LogEntry(string message, LogLevel level)
        {
            Message = message;
            Level = level;
        }
        /// <summary>
        /// Gets the Message.
        /// </summary>
        public string Message { get; private set; }
        /// <summary>
        /// Gets the LogLevel.
        /// </summary>
        public LogLevel Level { get; private set; }
        /// <summary>
        /// Gets or sets the LogMode.
        /// </summary>
        internal LogMode Mode { set; get; }
        /// <summary>
        /// Gets or sets the Time.
        /// </summary>
        public DateTime Time { set; get; }
    }
} 
