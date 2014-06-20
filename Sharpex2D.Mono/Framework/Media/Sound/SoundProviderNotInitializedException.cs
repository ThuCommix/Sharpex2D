using System;
using System.Runtime.Serialization;

namespace Sharpex2D.Framework.Media.Sound
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Tested)]
    [Serializable]
    public class SoundProviderNotInitializedException : Exception
    {
        /// <summary>
        ///     Initializes a new SoundProviderNotInitializedException class.
        /// </summary>
        public SoundProviderNotInitializedException()
        {
        }

        /// <summary>
        ///     Initializes a new SoundProviderNotInitializedException class.
        /// </summary>
        /// <param name="message">The Message.</param>
        public SoundProviderNotInitializedException(string message) : base(message)
        {
        }

        /// <summary>
        ///     Initializes a new SoundProviderNotInitializedException class.
        /// </summary>
        /// <param name="message">The Message.</param>
        /// <param name="inner">The InnerException.</param>
        public SoundProviderNotInitializedException(string message, Exception inner) : base(message, inner)
        {
        }

        /// <summary>
        ///     Initializes a new SoundProviderNotInitializedException class.
        /// </summary>
        /// <param name="serializationInfo">The SerializationInfo.</param>
        /// <param name="context">The StreamContext.</param>
        public SoundProviderNotInitializedException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context)
        {
        }
    }
}