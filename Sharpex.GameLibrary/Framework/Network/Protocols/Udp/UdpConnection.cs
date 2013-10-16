using System.Net;

namespace SharpexGL.Framework.Network.Protocols.Udp
{
    public class UdpConnection : IConnection
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
        /// Initializes a new UdpConnection class.
        /// </summary>
        /// <param name="ipAddress">The IPAddress.</param>
        public UdpConnection(IPAddress ipAddress)
        {
            IPAddress = ipAddress;
            Connected = true;
            Latency = 0;
        }
    }
}
