// Copyright (c) 2012-2014 Sharpex2D - Kevin Scholz (ThuCommix)
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

namespace Sharpex2D.Framework.Debug.Logging
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Tested)]
    public class Logger
    {
        /// <summary>
        ///     Initializes a new Logger class.
        /// </summary>
        /// <param name="type">The Type.</param>
        internal Logger(Type type)
        {
            Type = type;
        }

        /// <summary>
        ///     Gets the Type.
        /// </summary>
        public Type Type { private set; get; }

        /// <summary>
        ///     Formats the message.
        /// </summary>
        /// <param name="message">The Message.</param>
        /// <param name="level">The LogLevel.</param>
        private void Format(string message, LogLevel level)
        {
            LogManager.Trace(string.Format("[{0}][{1}] {2}", Type.Name, DateTime.Now.ToLongTimeString(), message), level);
        }

        /// <summary>
        ///     Logs a new info message.
        /// </summary>
        /// <param name="message">The Message.</param>
        public void Info(string message)
        {
            Format(message, LogLevel.Info);
        }

        /// <summary>
        ///     Logs a new info message.
        /// </summary>
        /// <param name="stringFormat">The StringFormat.</param>
        /// <param name="arguments">The Arguments.</param>
        public void Info(string stringFormat, params object[] arguments)
        {
            Format(string.Format(stringFormat, arguments), LogLevel.Info);
        }

        /// <summary>
        ///     Logs a new warning message.
        /// </summary>
        /// <param name="message">The Message.</param>
        public void Warn(string message)
        {
            Format(message, LogLevel.Warning);
        }

        /// <summary>
        ///     Logs a new warning message.
        /// </summary>
        /// <param name="stringFormat">The StringFormat.</param>
        /// <param name="arguments">The Arguments.</param>
        public void Warn(string stringFormat, params object[] arguments)
        {
            Format(string.Format(stringFormat, arguments), LogLevel.Warning);
        }

        /// <summary>
        ///     Logs a new error message.
        /// </summary>
        /// <param name="message">The Message.</param>
        public void Error(string message)
        {
            Format(message, LogLevel.Error);
        }

        /// <summary>
        ///     Logs a new error message.
        /// </summary>
        /// <param name="stringFormat">The StringFormat.</param>
        /// <param name="arguments">The Arguments.</param>
        public void Error(string stringFormat, params object[] arguments)
        {
            Format(string.Format(stringFormat, arguments), LogLevel.Error);
        }

        /// <summary>
        ///     Logs a new critical message.
        /// </summary>
        /// <param name="message">The Message.</param>
        public void Critical(string message)
        {
            Format(message, LogLevel.Critical);
        }

        /// <summary>
        ///     Logs a new critical message.
        /// </summary>
        /// <param name="stringFormat">The StringFormat.</param>
        /// <param name="arguments">The Arguments.</param>
        public void Critical(string stringFormat, params object[] arguments)
        {
            Format(string.Format(stringFormat, arguments), LogLevel.Critical);
        }

        /// <summary>
        ///     Logs a new engine message.
        /// </summary>
        /// <param name="message">The Message.</param>
        internal void Engine(string message)
        {
            Format(message, LogLevel.Engine);
        }

        /// <summary>
        ///     Logs a new engine message.
        /// </summary>
        /// <param name="stringFormat">The StringFormat.</param>
        /// <param name="arguments">The Arguments.</param>
        internal void Engine(string stringFormat, params object[] arguments)
        {
            Format(string.Format(stringFormat, arguments), LogLevel.Engine);
        }
    }
}