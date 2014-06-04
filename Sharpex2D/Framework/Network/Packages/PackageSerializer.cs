using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Sharpex2D.Framework.Network.Packages
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Tested)]
    public class PackageSerializer
    {
        private static readonly BinaryFormatter Formatter;

        /// <summary>
        ///     Initializes a new PackageSerializer class.
        /// </summary>
        static PackageSerializer()
        {
            Formatter = new BinaryFormatter();
        }

        /// <summary>
        ///     Serializes the package in the given stream.
        /// </summary>
        /// <param name="package">The Package.</param>
        /// <param name="targetStream">The TargetStream</param>
        public static void Serialize(IBasePackage package, Stream targetStream)
        {
            Formatter.Serialize(targetStream, package);
        }

        /// <summary>
        ///     Deserializes a package from the given stream.
        /// </summary>
        /// <param name="stream">The Stream.</param>
        /// <returns>Package</returns>
        public static IBasePackage Deserialize(Stream stream)
        {
            return (IBasePackage) Formatter.Deserialize(stream);
        }
    }
}