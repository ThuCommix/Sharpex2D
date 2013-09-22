using System.Net;

namespace SharpexGL.Framework.Network
{
    public interface IConnection
    {
        /// <summary>
        /// Sets or gets the Latency.
        /// </summary>
        float Latency { set; get; }
        /// <summary>
        /// Sets or gets the IPAddress.
        /// </summary>
        IPAddress IPAddress { set; get; }
        /// <summary>
        /// A value indicating whether the connection is still available.
        /// </summary>
        bool Connected { set; get; }
    }
}
