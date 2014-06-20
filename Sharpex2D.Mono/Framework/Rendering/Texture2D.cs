using Sharpex2D.Framework.Content.Pipeline;
using Sharpex2D.Framework.Rendering.Devices;

namespace Sharpex2D.Framework.Rendering
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Untested)]
    [Content("Texture2D")]
    public abstract class Texture2D : IDeviceResource
    {
        #region IRendererResource Implementation

        /// <summary>
        ///     Gets or sets the DebugName.
        /// </summary>
        public string DebugName { get; set; }

        #endregion

        /// <summary>
        ///     Gets the Width.
        /// </summary>
        public abstract int Width { get; }

        /// <summary>
        ///     Gets the Height.
        /// </summary>
        public abstract int Height { get; }
    }
}