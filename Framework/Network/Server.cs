using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpexGL.Framework.Events;
using SharpexGL.Framework.Game.Timing;
using SharpexGL.Framework.Network.Events;
using SharpexGL.Framework.Network.Listener;
using SharpexGL.Framework.Network.Protocols;
using SharpexGL.Framework.Rendering;

namespace SharpexGL.Framework.Network
{
    public class Server : IServer
    {
        #region IServer Implementation
        /// <summary>
        /// Gets the Server Protocol.
        /// </summary>
        public IServerProtocol Protocol { get; private set; }
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

        #endregion

        #region IGameHandler Implementation
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
            foreach (var subscriber in _subscribers)
            {
                subscriber.Tick(this, elapsed);
            }
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

        private List<IServerListener> _subscribers;
        private List<IConnection> _connections;


        public Server(IServerProtocol protocol)
        {
            Protocol = protocol;
            _subscribers = new List<IServerListener>();
            _connections = new List<IConnection>();
            protocol.Server = this;
            //Subscribe to gameloop finally
            SGL.Components.Get<GameLoop>().Subscribe(this);
            protocol.Host(2589);
        }
    }
}
