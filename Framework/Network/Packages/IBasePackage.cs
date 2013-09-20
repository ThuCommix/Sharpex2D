using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
        object DeserializeContent();
        /// <summary>
        /// Converts the deserialized object into its base type.
        /// </summary>
        /// <typeparam name="T">The Type.</typeparam>
        /// <param name="obj">The Object.</param>
        /// <returns>The Object.</returns>
        /// <remarks>The type is determined by the Origin Property.</remarks>
        T Convert<T>(object obj);
    }
}
