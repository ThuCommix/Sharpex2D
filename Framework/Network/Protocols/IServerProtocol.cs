using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpexGL.Framework.Network.Packages;

namespace SharpexGL.Framework.Network.Protocols
{
    public interface IServerProtocol
    {
        /// <summary>
        /// Hosts at the specified port.
        /// </summary>
        /// <param name="port">The port.</param>
        void Host(int port);
        /// <summary>
        /// Gets a value indicating whether this is hosted.
        /// </summary>
        bool Hosting { get; }
        /// <summary>
        /// Accepts a new connection.
        /// </summary>
        IConnection AcceptConnection();
        /// <summary>
        /// Sets the server.
        /// </summary>
        Server Server { set; }
    }
}
