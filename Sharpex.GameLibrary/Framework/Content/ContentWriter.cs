using System;
using System.IO;

namespace SharpexGL.Framework.Content
{
    public abstract class ContentWriter<T> : IContentWriter
    {
        /// <summary>
        /// Writes a specified value.
        /// </summary>
        /// <param name="writer">The BinaryWriter.</param>
        /// <param name="value">The Value.</param>
        void IContentWriter.Write(BinaryWriter writer, object value)
        {
            Write(writer, (T) value);
        }
        /// <summary>
        /// Writes a specified value.
        /// </summary>
        /// <param name="writer">The BinaryWriter.</param>
        /// <param name="value">The Value.</param>
        public abstract void Write(BinaryWriter writer, T value);

        /// <summary>
        /// Gets the ContentType.
        /// </summary>
        public Type ContentType
        {
            get { return typeof(T); }
        }
        /// <summary>
        /// The ContentManager.
        /// </summary>
        public ContentManager Content
        {
            get { return SGL.Components.Get<ContentManager>(); }
        }
    }
}
