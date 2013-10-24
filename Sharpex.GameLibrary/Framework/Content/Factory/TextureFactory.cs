using System;
using System.IO;
using SharpexGL.Framework.Content.Serialization;
using SharpexGL.Framework.Rendering;

namespace SharpexGL.Framework.Content.Factory
{
    public class TextureFactory : IFactory<Texture>
    {
        /// <summary>
        /// Gets the FactoryType.
        /// </summary>
        public Type FactoryType { get { return typeof(Texture); } }
        /// <summary>
        /// Creates a new Texture from the given FilePath.
        /// </summary>
        /// <param name="file">The FilePath.</param>
        /// <returns>Texture</returns>
        public Texture Create(string file)
        {
            using (var fileStream = new FileStream(file, FileMode.Open))
            {
                return new TextureSerializer().Read(new BinaryReader(fileStream));
            }
        }
        /// <summary>
        /// Creates a new Texture from the given Stream.
        /// </summary>
        /// <param name="stream">The Stream.</param>
        /// <returns>Texture</returns>
        public Texture Create(Stream stream)
        {
            using (stream)
            {
                return new TextureSerializer().Read(new BinaryReader(stream)); 
            }
        }
    }
}
