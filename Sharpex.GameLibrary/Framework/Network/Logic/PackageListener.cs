using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpexGL.Framework.Network.Packages;

namespace SharpexGL.Framework.Network.Logic
{
    public abstract class PackageListener<T>  : IPackageListener
    {
        /// <summary>
        /// Gets the ListenerType.
        /// </summary>
        public Type ListenerType { get { return typeof(T); } }
        /// <summary>
        /// Called if a package is received.
        /// </summary>
        /// <param name="binaryPackage">The BinaryPackage.</param>
        public virtual void OnPackageReceived(BinaryPackage binaryPackage)
        {
            
        }
    }
}
