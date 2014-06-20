using System;
using System.Runtime.Serialization;

namespace Sharpex2D.Framework.Plugin
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Tested)]
    [Serializable]
    public class PluginException : Exception
    {
        /// <summary>
        ///     Initializes a new PluginException class.
        /// </summary>
        public PluginException()
        {
        }

        /// <summary>
        ///     Initializes a new PluginException class.
        /// </summary>
        /// <param name="message">The Message.</param>
        public PluginException(string message) : base(message)
        {
        }

        /// <summary>
        ///     Initializes a new PluginException class.
        /// </summary>
        /// <param name="message">The Message.</param>
        /// <param name="inner">The InnerException.</param>
        public PluginException(string message, Exception inner) : base(message, inner)
        {
        }

        /// <summary>
        ///     Initializes a new PluginException class.
        /// </summary>
        /// <param name="serializationInfo">The SerializationInfo.</param>
        /// <param name="context">The StreamContext.</param>
        public PluginException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context)
        {
        }
    }
}