using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpexGL.Framework.Network.Packages;

namespace SharpexGL.Framework.Network.Protocols
{
    public interface IClientProtocol
    {
        /// <summary>
        /// Connects to the specified ip.
        /// </summary>
        /// <param name="ip">The ip.</param>
        /// <param name="port">The port.</param>
        void Connect(string ip, int port);
        /// <summary>
        /// Disconnects the client.
        /// </summary>
        void Disconnect();
        /// <summary>
        /// Receives a package.
        /// </summary>
        IPackage<object> Receive();
        /// <summary>
        /// Sends a package.
        /// </summary>
        /// <param name="package">The Package.</param>
        void Send(IPackage<Object> package);
        /// <summary>
        /// Sets the client.
        /// </summary>
        IClient Client { set; }
        /// <summary>
        /// Gets a value indicating whether this is connected.
        /// </summary>
        bool Connected { get; }
    }
}
