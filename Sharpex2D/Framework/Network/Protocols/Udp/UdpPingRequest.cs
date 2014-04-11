using System;
using System.Net;

namespace Sharpex2D.Framework.Network.Protocols.Udp
{
    internal class UdpPingRequest
    {
        /// <summary>
        /// Initializes a new UdpPingRequest class.
        /// </summary>
        /// <param name="ipAddress">The IPAddress.</param>
        /// <param name="timestamp">The Timestamp.</param>
        public UdpPingRequest(IPAddress ipAddress, DateTime timestamp)
        {
            IP = ipAddress;
            Timestamp = timestamp;
        }

        /// <summary>
        /// Gets the IPAddress.
        /// </summary>
        public IPAddress IP { get; private set; }
        /// <summary>
        /// Gets the Timestamp.
        /// </summary>
        public DateTime Timestamp { get; private set; }
    }
}
