using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace SharpexGL.Framework.Network.Packages
{
    [Serializable]
    public class BasePackage : IBasePackage
    {
        /// <summary>
        /// Gets or sets the type of the package Content
        /// </summary>
        public Type OriginType { get; private set; }
        /// <summary>
        /// Gets or sets the serialized package content.
        /// </summary>
        public byte[] Content { get; private set; }
        /// <summary>
        /// Sets or gets the package identifer.
        /// </summary>
        /// <remarks>This is not neccessary for serialization.</remarks>
        public string Identifer { get; set; }
        /// <summary>
        /// Serializes an object into the Content.
        /// </summary>
        /// <param name="content">The Object.</param>
        public void SerializeContent(object content)
        {
            using (var mStream = new MemoryStream())
            {
                var binaryFormatter = new BinaryFormatter();
                binaryFormatter.Serialize(mStream, content);
                Content = mStream.ToArray();
                OriginType = content.GetType();
            }
        }
        /// <summary>
        /// Deserializes an object out of the Content.
        /// </summary>
        /// <returns>The Object.</returns>
        public object DeserializeContent()
        {
            using (var mStream = new MemoryStream(Content))
            {
                return new BinaryFormatter().Deserialize(mStream);
            }
        }
        /// <summary>
        /// Converts the deserialized object into its base type.
        /// </summary>
        /// <typeparam name="T">The Type.</typeparam>
        /// <param name="obj">The Object.</param>
        /// <returns>The Object.</returns>
        /// <remarks>The type is determined by the Origin Property.</remarks>
        public T Convert<T>(object obj)
        {
            return (T) obj;
        }
    }
}
