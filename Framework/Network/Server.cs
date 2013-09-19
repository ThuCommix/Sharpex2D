using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpexGL.Framework.Network.Listener;
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

        #endregion

        #region Server

        private readonly List<IServerListener> _subscribers;

        public Server()
        {
            _subscribers = new List<IServerListener>();
        }

        #endregion
    }
}
