using SharpexGL.Framework.Network.Logic;

namespace SharpexGL.Framework.Network.Protocols
{
    public interface IServer : ISender
    {
        /// <summary>
        /// A value indicating whether the server is active.
        /// </summary>
        bool IsActive { get; }
        /// <summary>
        /// Subscribes to a Client.
        /// </summary>
        /// <param name="subscriber">The Subscriber.</param>
        void Subscribe(IPackageListener subscriber);
        /// <summary>
        /// Unsubscribes from a Client.
        /// </summary>
        /// <param name="unsubscriber">The Unsubscriber.</param>
        void Unsubscribe(IPackageListener unsubscriber);
    }
}
