using System;
using System.Net;

namespace Sharpex2D.Framework.Network.Packages
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Tested)]
    public interface IBasePackage
    {
        /// <summary>
        ///     Gets or sets the type of the package Content
        /// </summary>
        Type OriginType { get; }

        /// <summary>
        ///     Gets or sets the serialized package content.
        /// </summary>
        byte[] Content { get; }

        /// <summary>
        ///     Sets or gets the package identifer.
        /// </summary>
        /// <remarks>This is not neccessary for serialization.</remarks>
        string Identifer { set; get; }

        /// <summary>
        ///     Gets the sender.
        /// </summary>
        IPAddress Sender { get; }

        /// <summary>
        ///     Gets the receiver.
        /// </summary>
        IPAddress Receiver { get; set; }

        /// <summary>
        ///     Serializes an object into the Content.
        /// </summary>
        /// <param name="content">The Object.</param>
        void SerializeContent(object content);

        /// <summary>
        ///     Deserializes an object out of the Content.
        /// </summary>
        /// <returns>The Object.</returns>
        T DeserializeContent<T>();
    }
}