using System;
using System.Runtime.Serialization;

namespace Sharpex2D.Framework.Game.Services.Availability
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Tested)]
    [Serializable]
    public class NetworkNotAvailableException : Exception
    {
        /// <summary>
        ///     Initializes a new NetworkNotAvailableException class.
        /// </summary>
        public NetworkNotAvailableException()
        {
        }

        /// <summary>
        ///     Initializes a new NetworkNotAvailableException class.
        /// </summary>
        /// <param name="message">The Message.</param>
        public NetworkNotAvailableException(string message) : base(message)
        {
        }

        /// <summary>
        ///     Initializes a new NetworkNotAvailableException class.
        /// </summary>
        /// <param name="message">The Message.</param>
        /// <param name="inner">The InnerException.</param>
        public NetworkNotAvailableException(string message, Exception inner) : base(message, inner)
        {
        }

        /// <summary>
        ///     Initializes a new NetworkNotAvailableException class.
        /// </summary>
        /// <param name="serializationInfo">The SerializationInfo.</param>
        /// <param name="context">The StreamContext.</param>
        public NetworkNotAvailableException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context)
        {
        }
    }
}