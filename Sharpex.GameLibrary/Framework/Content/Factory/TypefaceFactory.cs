using System;
using System.IO;
using SharpexGL.Framework.Content.Serialization;
using SharpexGL.Framework.Rendering.Font;

namespace SharpexGL.Framework.Content.Factory
{
    public class TypefaceFactory : IFactory<Typeface>
    {
        /// <summary>
        /// Gets the FactoryType.
        /// </summary>
        public Type FactoryType {
            get { return typeof (Typeface); }
        }
        /// <summary>
        /// Creates a new Typeface from the given FilePath.
        /// </summary>
        /// <param name="file">The FilePath.</param>
        /// <returns>Typeface</returns>
        public Typeface Create(string file)
        {
            using (var fileStream = new FileStream(file, FileMode.Open))
            {
                return new TypefaceSerializer().Read(new BinaryReader(fileStream));
            }
        }
        /// <summary>
        /// Creates a new Typeface from the given Stream.
        /// </summary>
        /// <param name="stream">The Stream.</param>
        /// <returns>Typeface</returns>
        public Typeface Create(Stream stream)
        {
            using (stream)
            {
                return new TypefaceSerializer().Read(new BinaryReader(stream));
            }
        }
    }
}
