using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace SharpexGL.Framework.Network
{
    [Serializable]
    public class SerializableConnection : IConnection
    {
        /// <summary>
        /// Sets or gets the Latency.
        /// </summary>
        public float Latency { get; set; }
        /// <summary>
        /// Sets or gets the IPAddress.
        /// </summary>
        public IPAddress IPAddress { get; private set; }
        /// <summary>
        /// A value indicating whether the connection is still available.
        /// </summary>
        public bool Connected { get; private set; }

        /// <summary>
        /// Creates a SerializableConnection from IConnection.
        /// </summary>
        /// <param name="connection">The Connection.</param>
        /// <returns>SerializableConnection</returns>
        public static SerializableConnection FromIConnection(IConnection connection)
        {
            return new SerializableConnection(connection.IPAddress, connection.Latency, connection.Connected);
        }
        /// <summary>
        /// Converts an IConnection array into SerialiableConnection array.
        /// </summary>
        /// <param name="connections">The Connections.</param>
        /// <returns>SerializableConnections</returns>
        public static IConnection[] FromIConnection(IConnection[] connections)
        {
            return connections.Select(FromIConnection).ToArray();
        }
        /// <summary>
        /// Initializes a new SerializableConnection class.
        /// </summary>
        internal SerializableConnection(IPAddress ipaddress, float latency, bool connected)
        {
            IPAddress = ipaddress;
            Latency = latency;
            Connected = connected;
        }
    }
}
