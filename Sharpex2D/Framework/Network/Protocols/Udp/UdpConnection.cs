using System.Net;

namespace Sharpex2D.Framework.Network.Protocols.Udp
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Untested)]
    public class UdpConnection : IConnection
    {
        /// <summary>
        ///     Initializes a new UdpConnection class.
        /// </summary>
        /// <param name="ipAddress">The IPAddress.</param>
        public UdpConnection(IPAddress ipAddress)
        {
            IPAddress = ipAddress;
            Connected = true;
            Latency = 0;
        }

        /// <summary>
        ///     Sets or gets the Latency.
        /// </summary>
        public float Latency { get; set; }

        /// <summary>
        ///     Sets or gets the IPAddress.
        /// </summary>
        public IPAddress IPAddress { get; private set; }

        /// <summary>
        ///     A value indicating whether the connection is still available.
        /// </summary>
        public bool Connected { get; private set; }
    }
}