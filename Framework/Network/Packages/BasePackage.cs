using System;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;

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
        /// <typeparam name="T">The Type.</typeparam>
        /// <remarks>The type is determined by the OriginType Property.</remarks>
        /// <returns>The Object.</returns>
        public T DeserializeContent<T>()
        {
            using (var mStream = new MemoryStream(Content))
            {
                return (T)new BinaryFormatter().Deserialize(mStream);
            }
        }
        /// <summary>
        /// Gets the sender.
        /// </summary>
        public IPAddress Sender { internal set; get; }
        /// <summary>
        /// Gets the receiver.
        /// </summary>
        public IPAddress Receiver {  set; get; }
    }
}
