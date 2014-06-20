using Sharpex2D.Framework.Rendering.Fonts;

namespace Sharpex2D.Framework.Rendering.Devices
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Tested)]
    public abstract class ResourceManager
    {
        /// <summary>
        ///     Creates a new Resource.
        /// </summary>
        /// <param name="color">The Color.</param>
        /// <param name="width">The Width.</param>
        /// <returns>IPen.</returns>
        public abstract IPen CreateResource(Color color, float width);

        /// <summary>
        ///     Creates a new Resource.
        /// </summary>
        /// <param name="typeface">The Typeface.</param>
        /// <returns>IFont.</returns>
        public abstract IFont CreateResource(Typeface typeface);
    }
}