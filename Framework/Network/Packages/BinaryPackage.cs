using System;
using System.IO;
using SharpexGL.Framework.Content;
using SharpexGL.Framework.Implementation;

namespace SharpexGL.Framework.Network.Packages
{
    [Serializable]
    public class BinaryPackage<T> : IPackage<T>
    {
        /// <summary>
        /// Gets the Package Data.
        /// </summary>
        public byte[] PackageData { get; private set; }
        /// <summary>
        /// Gets the Identifer of the Package.
        /// </summary>
        public int Identifer { get; private set; }
        /// <summary>
        /// Serializes a TClass into a package.
        /// </summary>
        /// <param name="instance">TClass</param>
        public void Serialize(T instance)
        {
            var serializer = GetSerializer();
            var mStream = new MemoryStream();
            var writer = new BinaryWriter(mStream);
            serializer.Write(writer, instance);
            PackageData = mStream.ToArray();
            Identifer = typeof (T).GetHashCode();
            writer.Close();
        }
        /// <summary>
        /// Deserializes the PackageData into TClass.
        /// </summary>
        /// <returns>TClass</returns>
        public T Deserialize()
        {
            var serializer = GetSerializer();
            var mStream = new MemoryStream(PackageData);
            var reader = new BinaryReader(mStream);
            var resultType = serializer.Read(reader);
            reader.Close();
            return resultType;
        }

        /// <summary>
        /// Gets the Serializer for TClass.
        /// </summary>
        /// <returns>Serializer</returns>
        private ContentSerializer<T> GetSerializer()
        {
            var implementations = SGL.Components.Get<ImplementationManager>().GetImplementations();
            foreach (var implementation in implementations)
            {
                var serializer = implementation as ContentSerializer<T>;
                if (serializer != null)
                {
                    return serializer;
                }
            }
            throw new InvalidOperationException("There is no serializer for " + typeof (T).FullName);
        }
    }
}
