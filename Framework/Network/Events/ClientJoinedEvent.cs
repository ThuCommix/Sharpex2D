using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpexGL.Framework.Events;

namespace SharpexGL.Framework.Network.Events
{
    public class ClientJoinedEvent : IEvent
    {
        /// <summary>
        /// Creates a new ClientJoined Event.
        /// </summary>
        /// <param name="connection">The Connection.</param>
        public ClientJoinedEvent(IConnection connection)
        {
            Connection = connection;
        }
        /// <summary>
        /// Gets the connection.
        /// </summary>
        public IConnection Connection { get; private set; }
    }
}
