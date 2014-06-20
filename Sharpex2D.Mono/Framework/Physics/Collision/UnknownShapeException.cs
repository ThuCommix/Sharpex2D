using System;
using System.Runtime.Serialization;

namespace Sharpex2D.Framework.Physics.Collision
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Tested)]
    [Serializable]
    public class UnknownShapeException : Exception
    {
        /// <summary>
        ///     Initializes a new UnknownShapeException class.
        /// </summary>
        public UnknownShapeException()
        {
        }

        /// <summary>
        ///     Initializes a new UnknownShapeException class.
        /// </summary>
        /// <param name="message">The Message.</param>
        public UnknownShapeException(string message) : base(message)
        {
        }

        /// <summary>
        ///     Initializes a new UnknownShapeException class.
        /// </summary>
        /// <param name="message">The Message.</param>
        /// <param name="inner">The InnerException.</param>
        public UnknownShapeException(string message, Exception inner) : base(message, inner)
        {
        }

        /// <summary>
        ///     Initializes a new UnknownShapeException class.
        /// </summary>
        /// <param name="serializationInfo">The SerializationInfo.</param>
        /// <param name="context">The StreamContext.</param>
        public UnknownShapeException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context)
        {
        }
    }
}