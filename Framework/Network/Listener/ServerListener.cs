using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpexGL.Framework.Network.Packages;

namespace SharpexGL.Framework.Network.Listener
{
    public abstract class ServerNotification : IServerListener
    {

        #region IServerListener Implementation
        /// <summary>
        /// Gets the type of the package.
        /// </summary>
        public Type PackageType { get; private set; }
        /// <summary>
        /// Called when the server receives a package.
        /// </summary>
        /// <param name="server">The server.</param>
        /// <param name="package">The package.</param>
        /// <param name="sender">The sender.</param>
        void IServerListener.OnReceive(IServer server, IPackage<object> package, IConnection sender)
        {
            PackageType = package.GetType();
            OnReceive(server, package, sender);
        }
        /// <summary>
        /// Called when the server is sending a package.
        /// </summary>
        /// <param name="server">The server.</param>
        /// <param name="package">The package.</param>
        /// <param name="receiver">The receiver.</param>
        void IServerListener.OnBeginSend(IServer server, IPackage<object> package, IConnection receiver)
        {
            PackageType = package.GetType();
            OnBeginSend(server, package, receiver);
        }
        /// <summary>
        /// Called when the server sent a package.
        /// </summary>
        /// <param name="server">The server.</param>
        /// <param name="package">The package.</param>
        /// <param name="receiver">The receiver.</param>
        void IServerListener.OnSent(IServer server, IPackage<object> package, IConnection receiver)
        {
            PackageType = package.GetType();
            OnSent(server, package, receiver);
        }
        /// <summary>
        /// Called when a client joined the server.
        /// </summary>
        /// <param name="server">The server.</param>
        /// <param name="connection">The connection.</param>
        void IServerListener.OnClientJoined(IServer server, IConnection connection)
        {
            OnClientJoined(server, connection);
        }
        /// <summary>
        /// Called when a client left the server.
        /// </summary>
        /// <param name="server">The server.</param>
        /// <param name="connection">The connection.</param>
        void IServerListener.OnClientLeft(IServer server, IConnection connection)
        {
            OnClientLeft(server, connection);
        }
        /// <summary>
        /// Handles a game tick.
        /// </summary>
        /// <param name="server">The server.</param>
        /// <param name="elapsed">The elapsed.</param>
        void IServerListener.Tick(IServer server, float elapsed)
        {
            Tick(server, elapsed);
        }
        #endregion

        /// <summary>
        /// Called when the server receives a package.
        /// </summary>
        /// <param name="server">The server.</param>
        /// <param name="package">The package.</param>
        /// <param name="sender">The sender.</param>
        public virtual void OnReceive(IServer server, IPackage<object> package, IConnection sender)
        {

        }
        /// <summary>
        /// Called when the server is sending a package.
        /// </summary>
        /// <param name="server">The server.</param>
        /// <param name="package">The package.</param>
        /// <param name="receiver">The receiver.</param>
        public virtual void OnBeginSend(IServer server, IPackage<object> package, IConnection receiver)
        {

        }
        /// <summary>
        /// Called when the server sent a package.
        /// </summary>
        /// <param name="server">The server.</param>
        /// <param name="package">The package.</param>
        /// <param name="receiver">The receiver.</param>
        public virtual void OnSent(IServer server, IPackage<object> package, IConnection receiver)
        {

        }
        /// <summary>
        /// Called when a client joined the server.
        /// </summary>
        /// <param name="server">The server.</param>
        /// <param name="connection">The connection.</param>
        public virtual void OnClientJoined(IServer server, IConnection connection)
        {

        }
        /// <summary>
        /// Called when a client left the server.
        /// </summary>
        /// <param name="server">The server.</param>
        /// <param name="connection">The connection.</param>
        public virtual void OnClientLeft(IServer server, IConnection connection)
        {

        }
        /// <summary>
        /// Handles a game tick.
        /// </summary>
        /// <param name="server">The server.</param>
        /// <param name="elapsed">The elapsed.</param>
        public virtual void Tick(IServer server, float elapsed)
        {

        }
    }
}
