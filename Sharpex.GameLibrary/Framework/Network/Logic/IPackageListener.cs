using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpexGL.Framework.Network.Packages;

namespace SharpexGL.Framework.Network.Logic
{
    public interface IPackageListener
    {
        /// <summary>
        /// Gets the ListenerType.
        /// </summary>
        Type ListenerType { get; }
        /// <summary>
        /// Called if a package is received.
        /// </summary>
        /// <param name="binaryPackage">The BinaryPackage.</param>
        void OnPackageReceived(BinaryPackage binaryPackage);
    }
}
