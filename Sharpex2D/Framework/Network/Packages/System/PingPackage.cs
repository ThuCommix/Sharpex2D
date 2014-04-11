using System;

namespace Sharpex2D.Framework.Network.Packages.System
{
    [Serializable]
    internal class PingPackage : BasePackage
    {
        /// <summary>
        /// Initializes a new PingPackage class.
        /// </summary>
        /// <remarks>We use our PC-Time to determine the creation of the package. The clients sends the same package back
        /// and we calculate the time differences.</remarks>
        public PingPackage()
        {
            TimeStamp = DateTime.Now;
        }
        /// <summary>
        /// Gets the sending time of the PingPackage.
        /// </summary>
        public DateTime TimeStamp { private set; get; }
    }
}
