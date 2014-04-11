using Sharpex2D.Framework.Network.Protocols;

namespace Sharpex2D.Framework.Network.Logic
{
    public interface IClientListener
    {
        /// <summary>
        /// Called if a client joined on the server.
        /// </summary>
        /// <param name="connection">The IPAddress.</param>
        void OnClientJoined(IConnection connection);
        /// <summary>
        /// Called if a client exited.
        /// </summary>
        /// <param name="connection">The IPAddress.</param>
        void OnClientExited(IConnection connection);
        /// <summary>
        /// Called if the server sends a client list.
        /// </summary>
        /// <param name="connections">The Connections.</param>
        void OnClientListing(IConnection[] connections);
        /// <summary>
        /// Called if the server is closing.
        /// </summary>
        void OnServerShutdown();
        /// <summary>
        /// Called, if our client timed out.
        /// </summary>
        void OnClientTimedOut();
        /// <summary>
        /// Gets the client instance.
        /// </summary>
        IClient Client { get; }
    }
}
