using System;
using System.Net;
using System.Net.Sockets;

namespace Sharpex2D.Framework.Network.Protocols.Local
{
    [Serializable]
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
