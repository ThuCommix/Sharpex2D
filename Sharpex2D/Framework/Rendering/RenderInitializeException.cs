using System;
using System.Runtime.Serialization;

namespace Sharpex2D.Framework.Rendering
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Tested)]
    [Serializable]
    public class RenderInitializeException : Exception
    {
        /// <summary>
        ///     Initializes a new RenderInitializeException class.
        /// </summary>
        public RenderInitializeException()
        {
        }

        /// <summary>
        ///     Initializes a new RenderInitializeException class.
        /// </summary>
        /// <param name="message">The Message.</param>
        public RenderInitializeException(string message) : base(message)
        {
        }

        /// <summary>
        ///     Initializes a new RenderInitializeException class.
        /// </summary>
        /// <param name="message">The Message.</param>
        /// <param name="inner">The InnerException.</param>
        public RenderInitializeException(string message, Exception inner) : base(message, inner)
        {
        }

        /// <summary>
        ///     Initializes a new RenderInitializeException class.
        /// </summary>
        /// <param name="serializationInfo">The SerializationInfo.</param>
        /// <param name="context">The StreamContext.</param>
        public RenderInitializeException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context)
        {
        }
    }
}