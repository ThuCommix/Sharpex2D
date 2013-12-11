using SharpexGL.Framework.Content;

namespace SharpexGL.Framework.Rendering
{
    public interface ISpriteSheet : IContent
    {
        /// <summary>
        /// Gets texture on which the SpriteSheet is based.
        /// </summary>
        ITexture Texture { get; }
        /// <summary>
        /// Gets a textzre based on location and size.
        /// </summary>
        /// <param name="x">The X-Coord.</param>
        /// <param name="y">The Y-Coord.</param>
        /// <param name="width">The Width.</param>
        /// <param name="height">The Height.</param>
        /// <returns>ITexture</returns>
        ITexture GetSprite(int x, int y, int width, int height);
    }
}
