using System;
using Sharpex2D.Framework.Network.Packages;

namespace Sharpex2D.Framework.Network.Logic
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Tested)]
    public interface IPackageListener
    {
        /// <summary>
        ///     Gets the ListenerType.
        /// </summary>
        Type ListenerType { get; }

        /// <summary>
        ///     Called if a package is received.
        /// </summary>
        /// <param name="binaryPackage">The BinaryPackage.</param>
        void OnPackageReceived(BinaryPackage binaryPackage);
    }
}