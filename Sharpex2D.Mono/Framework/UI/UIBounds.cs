namespace Sharpex2D.Framework.UI
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Tested)]
    public class UIBounds
    {
        /// <summary>
        ///     Initializes a new UIBounds class.
        /// </summary>
        public UIBounds()
        {
            Height = 0;
            Width = 0;
        }

        /// <summary>
        ///     Initializes a new UIBounds class.
        /// </summary>
        /// <param name="x">The X-Coord.</param>
        /// <param name="y">The Y-Coord.</param>
        /// <param name="width">The Width.</param>
        /// <param name="height">The Height.</param>
        public UIBounds(int x, int y, int width, int height)
        {
            Width = width;
            Height = height;
            X = x;
            Y = y;
        }

        /// <summary>
        ///     Sets or gets the Width.
        /// </summary>
        public int Width { set; get; }

        /// <summary>
        ///     Sets or gets the Height.
        /// </summary>
        public int Height { set; get; }

        /// <summary>
        ///     Sets or gets the X-Coord.
        /// </summary>
        public int X { set; get; }

        /// <summary>
        ///     Sets or gets the Y-Coord.
        /// </summary>
        public int Y { set; get; }
    }
}