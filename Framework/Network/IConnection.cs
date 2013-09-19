using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using SharpexGL.Framework.Network.Packages;

namespace SharpexGL.Framework.Network
{
    public interface IConnection
    {
        /// <summary>
        /// Gets the IP.
        /// </summary>
        IPAddress IP { get; }
        /// <summary>
        /// Gets or sets the latency.
        /// </summary>
        float Latency { get; set; }
        /// <summary>
        /// Disconnects the client.
        /// </summary>
        void Disconnect();
        /// <summary>
        /// Receives a package.
        /// </summary>
        IPackage<Object> Receive();
        /// <summary>
        /// Gets a value indicating whether this is connected.
        /// </summary>
        bool Connected { get; }
    }
}
