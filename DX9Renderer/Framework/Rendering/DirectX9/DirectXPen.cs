using Sharpex2D.Framework.Content.Pipeline;

namespace Sharpex2D.Framework.Rendering.DirectX9
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Tested)]
    [Content("DirectX9 Pen")]
    public class DirectXPen : IPen
    {
        #region IPen Implementation
        /// <summary>
        /// Gets the Width.
        /// </summary>
        public float Width { get; set; }
        /// <summary>
        /// Gets the Color.
        /// </summary>
        public Color Color { get; set; }
        #endregion

        /// <summary>
        /// Initializes a new DirectXPen class.
        /// </summary>
        public DirectXPen()
        {
            
        }
        /// <summary>
        /// Initializes a new DirectXPen class.
        /// </summary>
        /// <param name="color"></param>
        /// <param name="width"></param>
        public DirectXPen(Color color, float width)
        {
            Color = color;
            Width = width;
        }
    }
}
