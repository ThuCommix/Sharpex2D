using System.Net;
using System;

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
        IPAddress IPAddress { get; }
        /// <summary>
        /// A value indicating whether the connection is still available.
        /// </summary>
        bool Connected { get; }
    }
}
