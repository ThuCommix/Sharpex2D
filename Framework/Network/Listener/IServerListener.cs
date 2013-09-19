using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpexGL.Framework.Network.Packages;

namespace SharpexGL.Framework.Network.Listener
{
    public interface IServerListener
    {
        /// <summary>
        /// Gets the type of the package.
        /// </summary>
        Type PackageType { get; }
        /// <summary>
        /// Called when the server receives a package.
        /// </summary>
        /// <param name="server">The server.</param>
        /// <param name="package">The package.</param>
        /// <param name="sender">The sender.</param>
        void OnReceive(IServer server, IPackage<Object> package, IConnection sender);
        /// <summary>
        /// Called when the server is sending a package.
        /// </summary>
        /// <param name="server">The server.</param>
        /// <param name="package">The package.</param>
        /// <param name="receiver">The receiver.</param>
        void OnBeginSend(IServer server, IPackage<Object> package, IConnection receiver);
        /// <summary>
        /// Called when the server sent a package.
        /// </summary>
        /// <param name="server">The server.</param>
        /// <param name="package">The package.</param>
        /// <param name="receiver">The receiver.</param>
        void OnSent(IServer server, IPackage<Object> package, IConnection receiver);
        /// <summary>
        /// Called when a client joined the server.
        /// </summary>
        /// <param name="server">The server.</param>
        /// <param name="connection">The connection.</param>
        void OnClientJoined(IServer server, IConnection connection);
        /// <summary>
        /// Called when a client left the server.
        /// </summary>
        /// <param name="server">The server.</param>
        /// <param name="connection">The connection.</param>
        void OnClientLeft(IServer server, IConnection connection);
        /// <summary>
        /// Handles a game tick.
        /// </summary>
        /// <param name="server">The server.</param>
        /// <param name="elapsed">The elapsed.</param>
        void Tick(IServer server, float elapsed);
    }
}
