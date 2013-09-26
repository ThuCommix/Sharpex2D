using System.Net;
using SharpexGL.Framework.Network.Logic;

namespace SharpexGL.Framework.Network.Protocols
{
    public interface IClient : ISender, IReceiver
    {
        /// <summary>
        /// Connects to the local server.
        /// </summary>
        /// <param name="ip">The Serverip.</param>
        void Connect(IPAddress ip);
        /// <summary>
        /// Disconnect from the local server.
        /// </summary>
        void Disconnect();
        /// <summary>
        /// Subscribes to a Client.
        /// </summary>
        /// <param name="subscriber">The Subscriber.</param>
        void Subscribe(IPackageListener subscriber);
        /// <summary>
        /// Subscribes to a Client.
        /// </summary>
        /// <param name="subscriber">The Subscriber.</param>
        void Subscribe(IClientListener subscriber);
        /// <summary>
        /// Unsubscribes from a Client.
        /// </summary>
        /// <param name="unsubscriber">The Unsubscriber.</param>
        void Unsubscribe(IPackageListener unsubscriber);
        /// <summary>
        /// Unsubscribes from a Client.
        /// </summary>
        /// <param name="unsubscriber">The Unsubscriber.</param>
        void Unsubscribe(IClientListener unsubscriber);
    }
}
