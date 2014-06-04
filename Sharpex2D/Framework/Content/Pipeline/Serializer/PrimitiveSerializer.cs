using System;
using System.IO;

namespace Sharpex2D.Framework.Content.Pipeline.Serializer
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Tested)]
    public abstract class PrimitiveSerializer<T> : IPrimitiveSerializer
    {
        /// <summary>
        ///     Reads a value from the given Reader.
        /// </summary>
        /// <param name="reader">The BinaryReader.</param>
        /// <returns>Object</returns>
        object IPrimitiveSerializer.Read(BinaryReader reader)
        {
            return Read(reader);
        }

        /// <summary>
        ///     Writes a specified value.
        /// </summary>
        /// <param name="writer">The BinaryWriter.</param>
        /// <param name="value">The Value.</param>
        void IPrimitiveSerializer.Write(BinaryWriter writer, object value)
        {
            Write(writer, (T) value);
        }

        /// <summary>
        ///     Gets the ContentType.
        /// </summary>
        public Type ContentType
        {
            get { return typeof (T); }
        }

        /// <summary>
        ///     The ContentManager.
        /// </summary>
        public ContentManager Content
        {
            get { return SGL.Components.Get<ContentManager>(); }
        }

        /// <summary>
        ///     Reads a value from the given Reader.
        /// </summary>
        /// <param name="reader">The BinaryReader.</param>
        /// <returns>T</returns>
        public abstract T Read(BinaryReader reader);

        /// <summary>
        ///     Writes a specified value.
        /// </summary>
        /// <param name="writer">The BinaryWriter.</param>
        /// <param name="value">The Value.</param>
        public abstract void Write(BinaryWriter writer, T value);
    }
}