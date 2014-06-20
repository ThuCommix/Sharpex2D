using System;

namespace Sharpex2D.Framework.Network.Packages
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Tested)]
    [Serializable]
    public class BinaryPackage : BasePackage
    {
        /// <summary>
        ///     Initializes a new BinaryPackage class.
        /// </summary>
        internal BinaryPackage()
        {
        }

        /// <summary>
        ///     Creates a BinaryPackage.
        /// </summary>
        /// <typeparam name="T">The Type.</typeparam>
        /// <param name="obj">The Object.</param>
        /// <returns>BinaryPackage</returns>
        public static BinaryPackage Create<T>(T obj)
        {
            var binaryPackage = new BinaryPackage();
            binaryPackage.SerializeContent(obj);
            return binaryPackage;
        }
    }
}