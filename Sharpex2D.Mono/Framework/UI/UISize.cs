using Sharpex2D.Framework.Math;

namespace Sharpex2D.Framework.UI
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Tested)]
    public class UISize
    {
        private readonly Vector2 _size;

        /// <summary>
        ///     Initializes a new UISize class.
        /// </summary>
        public UISize()
        {
            _size = new Vector2(0, 0);
        }

        /// <summary>
        ///     Initializes a new UISize class.
        /// </summary>
        /// <param name="width">The Width.</param>
        /// <param name="height">The Height.</param>
        public UISize(int width, int height)
        {
            _size = new Vector2(width, height);
        }

        /// <summary>
        ///     Gets the Width.
        /// </summary>
        public int Width
        {
            get { return (int) _size.X; }
        }

        /// <summary>
        ///     Gets the Height.
        /// </summary>
        public int Height
        {
            get { return (int) _size.Y; }
        }
    }
}