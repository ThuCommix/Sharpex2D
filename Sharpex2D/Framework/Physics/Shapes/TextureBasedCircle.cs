using Sharpex2D.Framework.Rendering;

namespace Sharpex2D.Framework.Physics.Shapes
{
    public class TextureBasedCircle : Circle
    {
        /// <summary>
        /// Gets the Texture.
        /// </summary>
        public ITexture Texture { private set; get; }
        /// <summary>
        /// Initializes a new TextureBasedCircle class.
        /// </summary>
        /// <param name="texture">The underlying Texture.</param>
        public TextureBasedCircle(ITexture texture)
        {
            Texture = texture;
        }
    }
}
