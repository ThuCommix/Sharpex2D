using System.Net;
using Sharpex2D.Framework.Network.Logic;

namespace Sharpex2D.Framework.Network.Protocols
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Tested)]
    public interface IClient : ISender, IReceiver
    {
        /// <summary>
        ///     Connects to the local server.
        /// </summary>
        /// <param name="ip">The Serverip.</param>
        void Connect(IPAddress ip);

        /// <summary>
        ///     Disconnect from the local server.
        /// </summary>
        void Disconnect();

        /// <summary>
        ///     Subscribes to a Client.
        /// </summary>
        /// <param name="subscriber">The Subscriber.</param>
        void Subscribe(IPackageListener subscriber);

        /// <summary>
        ///     Subscribes to a Client.
        /// </summary>
        /// <param name="subscriber">The Subscriber.</param>
        void Subscribe(IClientListener subscriber);

        /// <summary>
        ///     Unsubscribes from a Client.
        /// </summary>
        /// <param name="unsubscriber">The Unsubscriber.</param>
        void Unsubscribe(IPackageListener unsubscriber);

        /// <summary>
        ///     Unsubscribes from a Client.
        /// </summary>
        /// <param name="unsubscriber">The Unsubscriber.</param>
        void Unsubscribe(IClientListener unsubscriber);
    }
}