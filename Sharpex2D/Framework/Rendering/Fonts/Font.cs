using System;
using Sharpex2D.Framework.Content.Pipeline;
using Sharpex2D.Framework.Rendering.Devices;

namespace Sharpex2D.Framework.Rendering.Fonts
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Tested)]
    [Content("Font")]
    public class Font : IDeviceResource
    {
        #region IRendererResource Implementation

        /// <summary>
        ///     Gets or sets the DebugName.
        /// </summary>
        public string DebugName { get; set; }

        #endregion

        /// <summary>
        ///     Initializes a new Typeface.
        /// </summary>
        /// <param name="typeface">The Typeface.</param>
        public Font(Typeface typeface)
        {
            RenderDevice rendererInstance = SGL.RenderDevice;
            Instance = rendererInstance.ResourceManager.CreateResource(typeface);
        }

        /// <summary>
        ///     Gets the Instance.
        /// </summary>
        public IFont Instance { private set; get; }

        /// <summary>
        ///     Gets the Type.
        /// </summary>
        public Type Type { private set; get; }

        /// <summary>
        ///     Gets the Typeface.
        /// </summary>
        public Typeface Typeface
        {
            get { return Instance.Typeface; }
        }
    }
}