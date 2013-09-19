using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpexGL.Framework.Events;

namespace SharpexGL.Framework.Network.Events
{
    public class NetworkExceptionEvent : IEvent
    {
        /// <summary>
        /// Initializes a new NetworkExceptionEvent class.
        /// </summary>
        /// <param name="message">The Message.</param>
        public NetworkExceptionEvent(string message)
        {
            Message = message;
        }
        /// <summary>
        /// Gets the message value.
        /// </summary>
        public string Message { get; private set; }
    }
}
