using System.Net;
using Sharpex2D.Framework.Network.Packages;

namespace Sharpex2D.Framework.Network
{
    public interface ISender
    {
        /// <summary>
        /// Sends a package to the given receivers.
        /// </summary>
        /// <param name="package">The Package.</param>
        void Send(IBasePackage package);
        /// <summary>
        /// Sends a package to the given receivers.
        /// </summary>
        /// <param name="package">The Package.</param>
        /// <param name="receiver">The Receiver.</param>
        void Send(IBasePackage package, IPAddress receiver);
    }
}
