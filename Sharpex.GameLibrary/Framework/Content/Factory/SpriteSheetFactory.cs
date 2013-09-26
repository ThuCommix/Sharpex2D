using System;
using System.Drawing;
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
        /// <returns></returns>
        public SpriteSheet Create(string file)
        {
            return new SpriteSheet((Bitmap) Image.FromFile(file));
        }
    }
}
