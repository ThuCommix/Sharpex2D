using System;
using System.Runtime.Serialization;

namespace Sharpex2D.Framework.Localization
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Tested)]
    [Serializable]
    public class LanguageSerializationException : Exception
    {
        /// <summary>
        ///     Initializes a new LanguageSerializationException class.
        /// </summary>
        public LanguageSerializationException()
        {
        }

        /// <summary>
        ///     Initializes a new LanguageSerializationException class.
        /// </summary>
        /// <param name="message">The Message.</param>
        public LanguageSerializationException(string message) : base(message)
        {
        }

        /// <summary>
        ///     Initializes a new LanguageSerializationException class.
        /// </summary>
        /// <param name="message">The Message.</param>
        /// <param name="inner">The InnerException.</param>
        public LanguageSerializationException(string message, Exception inner) : base(message, inner)
        {
        }

        /// <summary>
        ///     Initializes a new LanguageSerializationException class.
        /// </summary>
        /// <param name="serializationInfo">The SerializationInfo.</param>
        /// <param name="context">The StreamContext.</param>
        public LanguageSerializationException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context)
        {
        }
    }
}