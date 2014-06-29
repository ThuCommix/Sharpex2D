using System;
using System.Collections.Generic;
using System.Diagnostics;
using Sharpex2D.Framework.Debug.Logging.Adapters;
using Sharpex2D.Framework.Debug.Logging.Adapters.VisualStudio;

namespace Sharpex2D.Framework.Debug.Logging
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Tested)]
    public static class LogManager
    {
        /// <summary>
        /// Gets the logger.
        /// </summary>
        /// <param name="type">The Type.</param>
        /// <returns>Logger.</returns>
        public static Logger GetLogger(Type type)
        {
            var logger = new Logger(type);
            Loggers.Add(logger);
            return logger;
        }

        /// <summary>
        /// Gets the class logger.
        /// </summary>
        /// <returns>The Logger.</returns>
        public static Logger GetClassLogger()
        {
            var logger = new Logger(new StackFrame(1).GetMethod().DeclaringType);
            Loggers.Add(logger);
            return logger;
        }

        /// <summary>
        /// Gets or sets the MinimumLogLevel.
        /// </summary>
        public static LogLevel MinimumLogLevel { set; get; }

        /// <summary>
        /// Gets or sets the Adapter.
        /// </summary>
        public static IAdapter Adapter { set; get; }

        private static readonly List<Logger> Loggers;

        /// <summary>
        /// Initializes a new LogManager class.
        /// </summary>
        static LogManager()
        {
            Loggers = new List<Logger>();
            MinimumLogLevel = LogLevel.Info;

            Adapter = new VSAdapter();
        }

        /// <summary>
        /// Traces the message.
        /// </summary>
        /// <param name="message">The Message.</param>
        /// <param name="level">The LogLevel.</param>
        internal static void Trace(string message, LogLevel level)
        {
            if (level >= MinimumLogLevel)
            {
                if (Adapter != null)
                {
                    Adapter.Write(string.Format("[{0}]{1}", level, message));

                    if (level == LogLevel.Critical)
                    {
                        Adapter.Write("");
                        Adapter.Write(Environment.StackTrace);
                        Adapter.Write("");
                    }
                }
            }
        }
    }
}
