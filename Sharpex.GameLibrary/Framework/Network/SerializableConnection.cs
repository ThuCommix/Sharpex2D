using System;
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
