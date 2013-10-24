using System;
using System.Drawing;
using System.IO;
using SharpexGL.Framework.Rendering;
using SharpexGL.Framework.Rendering.Sprites;

namespace SharpexGL.Framework.Content.Factory
{
    public class SpriteSheetFactory : IFactory<SpriteSheet>
    {
        /// <summary>
        /// Gets the FactoryType.
        /// </summary>
        public Type FactoryType
        {
            get { return typeof (SpriteSheet); }
        }

        /// <summary>
        /// Creates a new SpriteSheet Instance.
        /// </summary>
        /// <param name="file">The FilePath.</param>
        /// <returns>SpriteSheet</returns>
        public SpriteSheet Create(string file)
        {
            return new SpriteSheet((Bitmap) Image.FromFile(file));
        }

        /// <summary>
        /// Creates a new SpriteSheet Instance.
        /// </summary>
        /// <param name="texture">The Texture.</param>
        /// <returns>SpriteSheet</returns>
        public SpriteSheet Create(Texture texture)
        {
            return new SpriteSheet(texture.Texture2D);
        }
        /// <summary>
        /// Creates a new SpriteSheet Instance.
        /// </summary>
        /// <param name="stream">The Stream.</param>
        /// <returns>SpriteSheet</returns>
        public SpriteSheet Create(Stream stream)
        {
            using (stream)
            {
                return new SpriteSheet((Bitmap) Image.FromStream(stream));
            }
        }
    }
}
