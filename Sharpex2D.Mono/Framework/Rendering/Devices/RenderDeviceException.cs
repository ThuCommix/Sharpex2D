using System;
using System.Runtime.Serialization;

namespace Sharpex2D.Framework.Rendering.Devices
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Tested)]
    public class RenderDeviceException : Exception
    {
        /// <summary>
        ///     Initializes a new RenderDeviceException class.
        /// </summary>
        public RenderDeviceException()
        {
        }

        /// <summary>
        ///     Initializes a new RenderDeviceException class.
        /// </summary>
        /// <param name="message">The Message.</param>
        public RenderDeviceException(string message) : base(message)
        {
        }

        /// <summary>
        ///     Initializes a new RenderDeviceException class.
        /// </summary>
        /// <param name="message">The Message.</param>
        /// <param name="inner">The InnerException.</param>
        public RenderDeviceException(string message, Exception inner) : base(message, inner)
        {
        }

        /// <summary>
        ///     Initializes a new RenderDeviceException class.
        /// </summary>
        /// <param name="serializationInfo">The SerializationInfo.</param>
        /// <param name="context">The StreamContext.</param>
        public RenderDeviceException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context)
        {
        }
    }
}