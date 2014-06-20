using System;
using Sharpex2D.Framework.Content.Pipeline;
using Sharpex2D.Framework.Rendering.Devices;

namespace Sharpex2D.Framework.Rendering
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Tested)]
    [Content("Pencil")]
    public class Pen : IDeviceResource
    {
        #region IRendererResource Implementation

        /// <summary>
        ///     Gets or sets the DebugName.
        /// </summary>
        public string DebugName { get; set; }

        #endregion

        /// <summary>
        ///     Initializes a new Pen class.
        /// </summary>
        /// <param name="color">The Color.</param>
        /// <param name="width">The Width.</param>
        public Pen(Color color, float width)
        {
            RenderDevice rendererInstance = SGL.RenderDevice;
            Instance = rendererInstance.ResourceManager.CreateResource(color, width);
            Type = Instance.GetType();
        }

        /// <summary>
        ///     Gets the Instance.
        /// </summary>
        public IPen Instance { private set; get; }

        /// <summary>
        ///     Gets the Type.
        /// </summary>
        public Type Type { private set; get; }

        /// <summary>
        ///     Gets or sets the Color.
        /// </summary>
        public Color Color
        {
            set { Instance.Color = value; }
            get { return Instance.Color; }
        }

        /// <summary>
        ///     Gets or sets the Width.
        /// </summary>
        public float Width
        {
            set { Instance.Width = value; }
            get { return Instance.Width; }
        }
    }
}