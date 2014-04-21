using System;

namespace Sharpex2D.Framework.Debug.Logging
{
    public class LogEntry
    {
        /// <summary>
        /// Initializes a new LogEntry class.
        /// </summary>
        /// <param name="message">The Message.</param>
        /// <param name="level">The Level.</param>
        /// <param name="time">The Time.</param>
        public LogEntry(string message, LogLevel level, DateTime time)
        {
            Message = message;
            Level = level;
            Time = time;
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
        /// Gets or sets the Time.
        /// </summary>
        public DateTime Time { set; get; }
    }
} 
