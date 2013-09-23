using System.Net;
using SharpexGL.Framework.Network.Packages;
using SharpexGL.Framework.Network.Protocols;

namespace SharpexGL.Framework.Network.Logic
{
    public abstract class ClientListener
    {
        /// <summary>
        /// Initializes a new ClientListener class.
        /// </summary>
        /// <param name="client">The Client.</param>
        protected ClientListener(IClient client)
        {
            _client = client;
        }
        /// <summary>
        /// Called if a client joined on the server.
        /// </summary>
        /// <param name="ipAddress">The IPAddress.</param>
        public virtual void OnClientJoined(IPAddress ipAddress)
        {
            
        }
        /// <summary>
        /// Called if a client exited.
        /// </summary>
        /// <param name="ipAddress">The IPAddress.</param>
        public virtual void OnClientExited(IPAddress ipAddress)
        {
            
        }
        /// <summary>
        /// Called if the server sends a client list.
        /// </summary>
        /// <param name="connections">The Connections.</param>
        public virtual void OnClientListing(IPAddress[] connections)
        {
            
        }
        /// <summary>
        /// Called if the server is closing.
        /// </summary>
        public virtual void OnServerShutdown()
        {
            
        }
        /// <summary>
        /// Sends a package to all Clients.
        /// </summary>
        /// <param name="package">The Package.</param>
        public void SendPackage(BinaryPackage package)
        {
            _client.Send(package);
        }
        /// <summary>
        /// Sends a packafe to a specified client.
        /// </summary>
        /// <param name="package">The Package.</param>
        /// <param name="target">The IPAddress.</param>
        public void SendPackage(BinaryPackage package, IPAddress target)
        {
            _client.Send(package, target);
        }

        private readonly IClient _client;
    }
}
