// Copyright (c) 2012-2015 Sharpex2D - Kevin Scholz (ThuCommix)
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the 'Software'), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED 'AS IS', WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using Sharpex2D.Framework.Debug.Logging.Adapters;

namespace Sharpex2D.Framework.Debug.Logging
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Tested)]
    public static class LogManager
    {
        private static readonly List<Logger> Loggers;

        /// <summary>
        /// Initializes a new LogManager class.
        /// </summary>
        static LogManager()
        {
            Loggers = new List<Logger>();
#if DEBUG
            MinimumLogLevel = LogLevel.Info;
#else
            MinimumLogLevel = LogLevel.Warning;
#endif

            Adapter = new VSAdapter();
        }

        /// <summary>
        /// Gets or sets the MinimumLogLevel.
        /// </summary>
        public static LogLevel MinimumLogLevel { set; get; }

        /// <summary>
        /// Gets or sets the Adapter.
        /// </summary>
        public static IAdapter Adapter { set; get; }

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