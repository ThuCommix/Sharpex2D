using Sharpex2D.Framework.Content;

namespace Sharpex2D.Framework.Rendering.Font
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Tested)]
    public interface IFont : IContent
    {
        /// <summary>
        ///     Sets or gets the Typeface.
        /// </summary>
        Typeface Typeface { set; get; }
    }
}