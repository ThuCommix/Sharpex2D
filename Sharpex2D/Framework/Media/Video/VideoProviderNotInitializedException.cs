using System;
using System.Runtime.Serialization;

namespace Sharpex2D.Framework.Media.Video
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Tested)]
    [Serializable]
    public class VideoProviderNotInitializedException : Exception
    {
        /// <summary>
        ///     Initializes a new VideoProviderNotInitializedException class.
        /// </summary>
        public VideoProviderNotInitializedException()
        {
        }

        /// <summary>
        ///     Initializes a new VideoProviderNotInitializedException class.
        /// </summary>
        /// <param name="message">The Message.</param>
        public VideoProviderNotInitializedException(string message) : base(message)
        {
        }

        /// <summary>
        ///     Initializes a new VideoProviderNotInitializedException class.
        /// </summary>
        /// <param name="message">The Message.</param>
        /// <param name="inner">The InnerException.</param>
        public VideoProviderNotInitializedException(string message, Exception inner) : base(message, inner)
        {
        }

        /// <summary>
        ///     Initializes a new VideoProviderNotInitializedException class.
        /// </summary>
        /// <param name="serializationInfo">The SerializationInfo.</param>
        /// <param name="context">The StreamContext.</param>
        public VideoProviderNotInitializedException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context)
        {
        }
    }
}