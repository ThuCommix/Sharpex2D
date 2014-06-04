using Sharpex2D.Framework.Content;

namespace Sharpex2D.Framework.Rendering
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Tested)]
    public interface ITexture : IContent
    {
        /// <summary>
        ///     Gets the Width.
        /// </summary>
        int Width { get; }

        /// <summary>
        ///     Gets the Height.
        /// </summary>
        int Height { get; }
    }
}