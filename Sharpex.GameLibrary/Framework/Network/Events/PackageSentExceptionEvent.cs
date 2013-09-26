using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpexGL.Framework.Events;

namespace SharpexGL.Framework.Network.Events
{
    public class PackageSentExceptionEvent : IEvent
    {
        /// <summary>
        /// Initializes a new PackageSentExceptionEvent class.
        /// </summary>
        /// <param name="message">The Message.</param>
        public PackageSentExceptionEvent(string message)
        {
            Message = message;
        }
        /// <summary>
        /// Gets the exception message.
        /// </summary>
        public string Message {private set; get; }
    }
}
