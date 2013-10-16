using System;

namespace SharpexGL.Framework.Network.Packages.System
{
    [Serializable]
    internal class UdpPackage : BasePackage
    {
        /// <summary>
        /// Initializes a new UdpPackage class.
        /// </summary>
        /// <param name="notify">The UdpNotifyMode.</param>
        public UdpPackage(UdpNotify notify)
        {
            NotifyMode = notify;
        }
        /// <summary>
        /// Gets the notify mode of this package.
        /// </summary>
        public UdpNotify NotifyMode { get; private set; }
    }
}
