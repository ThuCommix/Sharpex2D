using System;
using System.Runtime.Serialization;

namespace Sharpex2D.Framework.Content
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Tested)]
    [Serializable]
    public class ContentLoadException : Exception
    {
        /// <summary>
        ///     Initializes a new ContentLoadException class.
        /// </summary>
        public ContentLoadException()
        {
        }

        /// <summary>
        ///     Initializes a new ContentLoadException class.
        /// </summary>
        /// <param name="message">The Message.</param>
        public ContentLoadException(string message) : base(message)
        {
        }

        /// <summary>
        ///     Initializes a new ContentLoadException class.
        /// </summary>
        /// <param name="message">The Message.</param>
        /// <param name="inner">The InnerException.</param>
        public ContentLoadException(string message, Exception inner) : base(message, inner)
        {
        }

        /// <summary>
        ///     Initializes a new ContentLoadException class.
        /// </summary>
        /// <param name="serializationInfo">The SerializationInfo.</param>
        /// <param name="context">The StreamContext.</param>
        public ContentLoadException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context)
        {
        }
    }
}