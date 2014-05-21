using Sharpex2D.Framework.Content;

namespace Sharpex2D.Framework.Rendering
{
    public interface ITexture : IContent
    {
        /// <summary>
        /// Gets the Width.
        /// </summary>
        int Width { get; }
        /// <summary>
        /// Gets the Height.
        /// </summary>
        int Height { get; }
    }
}
