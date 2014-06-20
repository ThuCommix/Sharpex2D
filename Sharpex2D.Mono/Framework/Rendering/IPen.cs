using Sharpex2D.Framework.Content;

namespace Sharpex2D.Framework.Rendering
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Tested)]
    public interface IPen : IContent
    {
        /// <summary>
        ///     Sets or gets the Size of the Pen.
        /// </summary>
        float Width { set; get; }

        /// <summary>
        ///     Sets or gets the Color of the Pen.
        /// </summary>
        Color Color { set; get; }
    }
}