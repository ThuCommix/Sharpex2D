using Sharpex2D.Framework.Network.Logic;

namespace Sharpex2D.Framework.Network.Protocols
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Tested)]
    public interface IServer : ISender
    {
        /// <summary>
        ///     A value indicating whether the server is active.
        /// </summary>
        bool IsActive { get; }

        /// <summary>
        ///     Subscribes to a Client.
        /// </summary>
        /// <param name="subscriber">The Subscriber.</param>
        void Subscribe(IPackageListener subscriber);

        /// <summary>
        ///     Unsubscribes from a Client.
        /// </summary>
        /// <param name="unsubscriber">The Unsubscriber.</param>
        void Unsubscribe(IPackageListener unsubscriber);
    }
}