using System;
using System.Runtime.Serialization;

namespace Sharpex2D.Framework.Content.Pipeline
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Tested)]
    [Serializable]
    public class ContentProcessorException : Exception
    {
        /// <summary>
        ///     Initializes a new ContentProcessorException class.
        /// </summary>
        public ContentProcessorException()
        {
        }

        /// <summary>
        ///     Initializes a new ContentProcessorException class.
        /// </summary>
        /// <param name="message">The Message.</param>
        public ContentProcessorException(string message) : base(message)
        {
        }

        /// <summary>
        ///     Initializes a new ContentProcessorException class.
        /// </summary>
        /// <param name="message">The Message.</param>
        /// <param name="inner">The InnerException.</param>
        public ContentProcessorException(string message, Exception inner) : base(message, inner)
        {
        }

        /// <summary>
        ///     Initializes a new ContentProcessorException class.
        /// </summary>
        /// <param name="serializationInfo">The SerializationInfo.</param>
        /// <param name="context">The StreamContext.</param>
        public ContentProcessorException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context)
        {
        }
    }
}