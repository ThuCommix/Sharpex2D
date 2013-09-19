using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace SharpexGL.Framework.Network.Packages
{
    public static class PackageSerializer<T>
    {
        /// <summary>
        /// Deserilizes a Stream into Package.
        /// </summary>
        /// <param name="stream">The Stream.</param>
        /// <returns>Package</returns>
        public static object Deserialize(Stream stream)
        {
            stream.Seek(0, SeekOrigin.Begin);
            return new BinaryFormatter().Deserialize(stream);
        }

        /// <summary>
        /// Serializes a Package into a Stream.
        /// </summary>
        /// <param name="package">The Package.</param>
        /// <returns>Stream.</returns>
        public static Stream Serialize(IPackage<T> package)
        {
            var mStream = new MemoryStream();
            new BinaryFormatter().Serialize(mStream, package);
            return mStream;
        }
    }
}
