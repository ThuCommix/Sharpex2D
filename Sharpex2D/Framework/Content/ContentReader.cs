using System;
using System.IO;

namespace Sharpex2D.Framework.Content
{
    public abstract class ContentReader<T> : IContentReader
    {
        /// <summary>
        /// Reads a value from the given Reader.
        /// </summary>
        /// <param name="reader">The BinaryReader.</param>
        /// <returns></returns>
        object IContentReader.Read(BinaryReader reader)
        {
            return Read(reader);
        }
        /// <summary>
        /// Reads a value from the given Reader.
        /// </summary>
        /// <param name="reader">The BinaryReader.</param>
        /// <returns></returns>
        public abstract object Read(BinaryReader reader);
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
