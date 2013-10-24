using System;
using System.IO;
using SharpexGL.Framework.Content.Serialization;
using SharpexGL.Framework.Rendering.Font;

namespace SharpexGL.Framework.Content.Factory
{
    public class SpriteFontFactory : IFactory<SpriteFont>
    {
        /// <summary>
        /// Gets the FactoryType.
        /// </summary>
        public Type FactoryType { get { return typeof (SpriteFont); } }
        /// <summary>
        /// Creates a new SpriteFont from the given FilePath.
        /// </summary>
        /// <param name="file">The FilePath.</param>
        /// <returns>SpriteFont</returns>
        public SpriteFont Create(string file)
        {
            using (var fileStream = new FileStream(file, FileMode.Open))
            {
                return new SpriteFontSerializer().Read(new BinaryReader(fileStream));
            }
        }
        /// <summary>
        /// Creates a new SpriteFont from the given Stream.
        /// </summary>
        /// <param name="stream">The Stream.</param>
        /// <returns>SpriteFontreturns</returns>
        public SpriteFont Create(Stream stream)
        {
            using (stream)
            {
                return new SpriteFontSerializer().Read(new BinaryReader(stream));
            }
        }
    }
}
