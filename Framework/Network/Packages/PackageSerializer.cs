using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace SharpexGL.Framework.Network.Packages
{
    public class PackageSerializer
    {
        /// <summary>
        /// Serializes the package in the given stream.
        /// </summary>
        /// <param name="package">The Package.</param>
        /// <param name="targetStream">The TargetStream</param>
        public static void Serialize(IBasePackage package, Stream targetStream)
        {
            new BinaryFormatter().Serialize(targetStream, package);
        }

        /// <summary>
        /// Deserializes a package from the given stream.
        /// </summary>
        /// <param name="stream">The Stream.</param>
        /// <returns>Package</returns>
        public static IBasePackage Deserialize(Stream stream)
        {
            return (IBasePackage) new BinaryFormatter().Deserialize(stream);
        }
    }
}
