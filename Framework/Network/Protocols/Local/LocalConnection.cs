using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SharpexGL.Framework.Network.Protocols.Local
{
    public class LocalConnection : IConnection
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
        public bool Connected { get { return Client.Connected; } }
        /// <summary>
        /// Initializes a new LocalConnection class.
        /// </summary>
        /// <param name="tcpClient">The Client.</param>
        public LocalConnection(TcpClient tcpClient)
        {
            Client = tcpClient;
            Latency = 0;
            IPAddress = ((IPEndPoint) tcpClient.Client.LocalEndPoint).Address;
        }

        public TcpClient Client { get; private set; }
    }
}
