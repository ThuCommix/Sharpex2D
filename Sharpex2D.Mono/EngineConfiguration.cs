using Sharpex2D.Framework.Media;
using Sharpex2D.Framework.Rendering.Devices;

namespace Sharpex2D
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Tested)]
    public class EngineConfiguration
    {
        /// <summary>
        ///     Initializes a new EngineConfiguration class.
        /// </summary>
        /// <param name="renderer">The Renderer.</param>
        /// <param name="mediaInitializer">The MediaInitializer.</param>
        public EngineConfiguration(RenderDevice renderer, MediaInitializer mediaInitializer)
        {
            Renderer = renderer;
            MediaInitializer = mediaInitializer;
        }

        /// <summary>
        ///     Gets the IRenderer.
        /// </summary>
        internal RenderDevice Renderer { private set; get; }

        /// <summary>
        ///     Gets the MediaInitializer.
        /// </summary>
        internal MediaInitializer MediaInitializer { private set; get; }
    }
}