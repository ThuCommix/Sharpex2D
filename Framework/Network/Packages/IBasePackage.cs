using System;

namespace SharpexGL.Framework.Network.Packages
{
    public interface IBasePackage
    {
        /// <summary>
        /// Gets or sets the type of the package Content
        /// </summary>
        Type OriginType { get; }
        /// <summary>
        /// Gets or sets the serialized package content.
        /// </summary>
        byte[] Content { get; }
        /// <summary>
        /// Sets or gets the package identifer.
        /// </summary>
        /// <remarks>This is not neccessary for serialization.</remarks>
        string Identifer { set; get; }
        /// <summary>
        /// Serializes an object into the Content.
        /// </summary>
        /// <param name="content">The Object.</param>
        void SerializeContent(object content);
        /// <summary>
        /// Deserializes an object out of the Content.
        /// </summary>
        /// <returns>The Object.</returns>
        T DeserializeContent<T>();
    }
}
