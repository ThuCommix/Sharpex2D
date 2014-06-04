using System;

namespace Sharpex2D.Framework.Network.Packages.System
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Tested)]
    [Serializable]
    internal class UdpPackage : BasePackage
    {
        /// <summary>
        ///     Initializes a new UdpPackage class.
        /// </summary>
        /// <param name="notify">The UdpNotifyMode.</param>
        public UdpPackage(UdpNotify notify)
        {
            NotifyMode = notify;
        }

        /// <summary>
        ///     Gets the notify mode of this package.
        /// </summary>
        public UdpNotify NotifyMode { get; private set; }
    }
}