using Sharpex2D.Framework.Rendering;

namespace Sharpex2D.Framework.Physics.Shapes
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Tested)]
    public class TextureBasedCircle : Circle
    {
        /// <summary>
        ///     Initializes a new TextureBasedCircle class.
        /// </summary>
        /// <param name="texture">The underlying Texture.</param>
        public TextureBasedCircle(ITexture texture)
        {
            Texture = texture;
        }

        /// <summary>
        ///     Gets the Texture.
        /// </summary>
        public ITexture Texture { private set; get; }
    }
}