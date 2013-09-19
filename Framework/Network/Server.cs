using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpexGL.Framework.Network.Listener;
using SharpexGL.Framework.Network.Packages;
using SharpexGL.Framework.Network.Protocols;
using SharpexGL.Framework.Rendering;

namespace SharpexGL.Framework.Network
{
    public class Server : IServer
    {
        #region IGameHandler, IConstructable Implementation
        /// <summary>
        /// Constructs the Component
        /// </summary>
        public void Construct()
        {
            
        }
        /// <summary>
        /// Processes a Game tick.
        /// </summary>
        /// <param name="elapsed">The Elapsed.</param>
        public void Tick(float elapsed)
        {
           
        }
        /// <summary>
        /// Processes a Render.
        /// </summary>
        /// <param name="renderer">The GraphicRenderer.</param>
        /// <param name="elapsed">The Elapsed.</param>
        public void Render(IGraphicRenderer renderer, float elapsed)
        {
            
        }
        #endregion

        #region IServer Implementation
        /// <summary>
        /// Subscribes the Listener to the Server.
        /// </summary>
        /// <param name="subscriber">The Listener.</param>
        public void Subscribe(IServerListener subscriber)
        {
            _subscribers.Add(subscriber);
        }
        /// <summary>
        /// Unsubscribes the Listener from the Server.
        /// </summary>
        /// <param name="unsubscriber">The Unsubscriber.</param>
        public void Unsubscribe(IServerListener unsubscriber)
        {
            _subscribers.Remove(unsubscriber);
        }
        /// <summary>
        /// Gets the protocol.
        /// </summary>
        public IServerProtocol Protocol { get; private set; }
        /// <summary>
        /// Gets the connections.
        /// </summary>
        public IList<IConnection> Connections { get; private set; }
        /// <summary>
        /// Sends the specified package.
        /// </summary>
        /// <param name="package">The package.</param>
        public void Send(IPackage<object> package)
        {
            
        }
        /// <summary>
        /// Sends the specified package to the receiver.
        /// </summary>
        /// <param name="package">The package.</param>
        /// <param name="receiver">The receiver.</param>
        public void Send(IPackage<object> package, IConnection receiver)
        {
            
        }

        #endregion

        #region Server

        private readonly List<IServerListener> _subscribers;
        private readonly ConnectionProvider _conProvider;

        public bool IsRunning { get; private set; }

        public Server(IServerProtocol protocol)
        {
            _subscribers = new List<IServerListener>();
            Connections = new List<IConnection>();
            _conProvider = new ConnectionProvider(this);
            IsRunning = true;
            Protocol = protocol;
            Protocol.Server = this;
            _conProvider.BeginAcceptingConnections();
        }

        internal void OnClientJoined(IConnection connection)
        {
            foreach (var subscriber in _subscribers)
            {
                subscriber.OnClientJoined(this, connection);
            }
        }

        internal void OnPackageReceived(IConnection connection, IPackage<object> package)
        {
            foreach (var subscriber in _subscribers)
            {
                subscriber.OnReceive(this, package, connection);
            }
        }

        internal void OnClientLeft(IConnection connection)
        {
            foreach (var subscriber in _subscribers)
            {
                subscriber.OnClientLeft(this, connection);
            }
        }

        #endregion
    }
}
