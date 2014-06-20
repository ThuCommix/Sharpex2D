using Sharpex2D.Framework.Rendering.Devices;
using Sharpex2D.Framework.Rendering.DirectX9.Fonts;
using Sharpex2D.Framework.Rendering.Fonts;

namespace Sharpex2D.Framework.Rendering.DirectX9
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Tested)]
    public class DirectXResourceManager : ResourceManager
    {
        /// <summary>
        ///     Creates a new Resource.
        /// </summary>
        /// <param name="color">The Color.</param>
        /// <param name="width">The Width.</param>
        /// <returns>IPen.</returns>
        public override IPen CreateResource(Color color, float width)
        {
            return new DirectXPen(color, width);
        }

        /// <summary>
        ///     Creates a new Resource.
        /// </summary>
        /// <param name="typeface">The Typeface.</param>
        /// <returns>IFont.</returns>
        public override IFont CreateResource(Typeface typeface)
        {
            return new DirectXFont(typeface);
        }
    }
}