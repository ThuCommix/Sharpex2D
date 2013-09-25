using SharpexGL.Framework.Events;

namespace SharpexGL.Framework.Network.Events
{
    public class UnknownPackageExceptionEvent : IEvent
    {
        /// <summary>
        /// Initializes a new UnknownPackageExceptionEvent class.
        /// </summary>
        public UnknownPackageExceptionEvent()
        {
            Message = "";
        }
        /// <summary>
        /// Initializes a new UnknownPackageExceptionEvent class.
        /// </summary>
        /// <param name="message">The Message.</param>
        public UnknownPackageExceptionEvent(string message)
        {
            Message = message;
        }

        /// <summary>
        /// Gets the event message.
        /// </summary>
        public string Message { private set; get; }
    }
}
