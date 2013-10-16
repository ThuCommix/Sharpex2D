using SharpexGL.Framework.Events;

namespace SharpexGL.Framework.Network.Events
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
