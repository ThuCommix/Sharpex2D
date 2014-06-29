using System;

namespace Sharpex2D.Framework.Debug.Logging
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Tested)]
    public class Logger
    {
        /// <summary>
        /// Gets the Type.
        /// </summary>
        public Type Type { private set; get; }

        /// <summary>
        /// Initializes a new Logger class.
        /// </summary>
        /// <param name="type">The Type.</param>
        internal Logger(Type type)
        {
            Type = type;
        }

        /// <summary>
        /// Formats the message.
        /// </summary>
        /// <param name="message">The Message.</param>
        /// <param name="level">The LogLevel.</param>
        private void Format(string message, LogLevel level)
        {
            LogManager.Trace(string.Format("[{0}][{1}] {2}", Type.Name, DateTime.Now.ToLongTimeString(), message), level);
        }

        /// <summary>
        /// Logs a new info message.
        /// </summary>
        /// <param name="message">The Message.</param>
        public void Info(string message)
        {
            Format(message, LogLevel.Info);
        }

        /// <summary>
        /// Logs a new info message.
        /// </summary>
        /// <param name="stringFormat">The StringFormat.</param>
        /// <param name="arguments">The Arguments.</param>
        public void Info(string stringFormat, params object[] arguments)
        {
            Format(string.Format(stringFormat, arguments), LogLevel.Info);
        }

        /// <summary>
        /// Logs a new warning message.
        /// </summary>
        /// <param name="message">The Message.</param>
        public void Warn(string message)
        {
            Format(message, LogLevel.Warning);
        }

        /// <summary>
        /// Logs a new warning message.
        /// </summary>
        /// <param name="stringFormat">The StringFormat.</param>
        /// <param name="arguments">The Arguments.</param>
        public void Warn(string stringFormat, params object[] arguments)
        {
            Format(string.Format(stringFormat, arguments), LogLevel.Warning);
        }

        /// <summary>
        /// Logs a new error message.
        /// </summary>
        /// <param name="message">The Message.</param>
        public void Error(string message)
        {
            Format(message, LogLevel.Error);
        }

        /// <summary>
        /// Logs a new error message.
        /// </summary>
        /// <param name="stringFormat">The StringFormat.</param>
        /// <param name="arguments">The Arguments.</param>
        public void Error(string stringFormat, params object[] arguments)
        {
            Format(string.Format(stringFormat, arguments), LogLevel.Error);
        }

        /// <summary>
        /// Logs a new critical message.
        /// </summary>
        /// <param name="message">The Message.</param>
        public void Critical(string message)
        {
            Format(message, LogLevel.Critical);
        }

        /// <summary>
        /// Logs a new critical message.
        /// </summary>
        /// <param name="stringFormat">The StringFormat.</param>
        /// <param name="arguments">The Arguments.</param>
        public void Critical(string stringFormat, params object[] arguments)
        {
            Format(string.Format(stringFormat, arguments), LogLevel.Critical);
        }

        /// <summary>
        /// Logs a new engine message.
        /// </summary>
        /// <param name="message">The Message.</param>
        internal void Engine(string message)
        {
            Format(message, LogLevel.Engine);
        }

        /// <summary>
        /// Logs a new engine message.
        /// </summary>
        /// <param name="stringFormat">The StringFormat.</param>
        /// <param name="arguments">The Arguments.</param>
        internal void Engine(string stringFormat, params object[] arguments)
        {
            Format(string.Format(stringFormat, arguments), LogLevel.Engine);
        }
    }
}
