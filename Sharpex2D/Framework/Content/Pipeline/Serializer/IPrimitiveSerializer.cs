using System;
using System.IO;

namespace Sharpex2D.Framework.Content.Pipeline.Serializer
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Tested)]
    public interface IPrimitiveSerializer
    {
        /// <summary>
        ///     Gets the ContentType.
        /// </summary>
        Type ContentType { get; }

        /// <summary>
        ///     The ContentManager.
        /// </summary>
        ContentManager Content { get; }

        /// <summary>
        ///     Starts writing contentdata.
        /// </summary>
        /// <param name="writer">The BinaryWriter</param>
        /// <param name="value">The Value.</param>
        void Write(BinaryWriter writer, object value);

        /// <summary>
        ///     Starts reading contentdata.
        /// </summary>
        /// <param name="reader">The BinaryReader.</param>
        /// <returns>Object</returns>
        object Read(BinaryReader reader);
    }
}