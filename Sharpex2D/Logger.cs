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
using System.IO;

namespace Sharpex2D.Framework
{
    public class Logger : Singleton<Logger>
    {
        /// <summary>
        /// Initializes a new Logger class
        /// </summary>
        public Logger()
        {
            MinimumLogLevel = LogLevel.Debug;
            Out = Console.Out;
        }

        /// <summary>
        /// Gets or sets the minimum log level
        /// </summary>
        public LogLevel MinimumLogLevel { set; get; }

        /// <summary>
        /// Gets or sets the textwriter
        /// </summary>
        public TextWriter Out { set; get; }

        /// <summary>
        /// Writes a debug message
        /// </summary>
        /// <param name="message">The Message</param>
        public void Debug(string message)
        {
            Trace(message, LogLevel.Debug);
        }

        /// <summary>
        /// Writes a warning message
        /// </summary>
        /// <param name="message">The Message</param>
        public void Warn(string message)
        {
            Trace(message, LogLevel.Warning);
        }

        /// <summary>
        /// Writes an error message
        /// </summary>
        /// <param name="message">The Message</param>
        public void Error(string message)
        {
            Trace(message, LogLevel.Error);
        }

        /// <summary>
        /// Writes an engine message
        /// </summary>
        /// <param name="message">The Message</param>
        internal void Engine(string message)
        {
            Trace(message, LogLevel.Engine);
        }

        /// <summary>
        /// Traces a message
        /// </summary>
        /// <param name="message">The Message</param>
        /// <param name="level">The LogLevel</param>
        public void Trace(string message, LogLevel level)
        {
            if (level >= MinimumLogLevel)
            {
                Out?.WriteLine($"[{level}] {message}");
            }
        }
    }
}
