using System;

namespace Sharpex2D.Framework.Debug.Logging
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Tested)]
    public enum LogLevel
    {
        /// <summary>
        ///     Indicates an information.
        /// </summary>
        Info = 0,

        /// <summary>
        ///     Indicates a warning.
        /// </summary>
        Warning = 1,

        /// <summary>
        ///     Indicates an error.
        /// </summary>
        Error = 2,

        /// <summary>
        ///     Indicates a critical error.
        /// </summary>
        Critical = 3,

        /// <summary>
        ///     Indicates a engine message.
        /// </summary>
        Engine = 4
    }
}