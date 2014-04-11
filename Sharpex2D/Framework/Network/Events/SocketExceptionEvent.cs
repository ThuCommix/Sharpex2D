using Sharpex2D.Framework.Events;

namespace Sharpex2D.Framework.Network.Events
{
    public class SocketExceptionEvent : IEvent
    {
        /// <summary>
        /// Initializes a new SocketExceptionEvent class.
        /// </summary>
        /// <param name="message">The Message.</param>
        public SocketExceptionEvent(string message)
        {
            Message = message;
        }
        /// <summary>
        /// Gets the exception message.
        /// </summary>
        public string Message { private set; get; }
    }
}
