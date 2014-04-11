using System;

namespace Sharpex2D.Framework.Math
{
    public struct Rectangle
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Rectangle"/> struct.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        public Rectangle(float x, float y, float width, float height)
        {
            _x = x;
            _y = y;
            _width = width;
            _height = height;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Rectangle"/> struct.
        /// </summary>
        /// <param name="a">The first vector.</param>
        /// <param name="b">The second vector.</param>
        public Rectangle(Vector2 a, Vector2 b)
        {
            _x = a.X;
            _y = a.Y;
            _width = MathHelper.Abs(a.X - b.X);
            _height = MathHelper.Abs(a.Y - b.Y);
        }
        #endregion

        #region Fields
        private float _x;
        private float _y;
        private float _width;
        private float _height;
        #endregion

        #region Coordinates and Size
        /// <summary>
        /// Gets or sets the X-coordinate.
        /// </summary>
        public float X
        {
            get { return _x; }
            set { _x = value; }
        }
        /// <summary>
        /// Gets or sets the Y-coordinate.
        /// </summary>
        public float Y
        {
            get { return _y; }
            set { _y = value; }
        }
        /// <summary>
        /// Gets or sets the width.
        /// </summary>
        public float Width
        {
            get { return _width; }
            set { _width = value; }
        }
        /// <summary>
        /// Gets or sets the height.
        /// </summary>
        public float Height
        {
            get { return _height; }
            set { _height = value; }
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the left.
        /// </summary>
        public float Left
        {
            get { return X; }
        }
        /// <summary>
        /// Gets the right.
        /// </summary>
        public float Right
        {
            get { return X + Width; }
        }
        /// <summary>
        /// Gets the top.
        /// </summary>
        public float Top
        {
            get { return Y; }
        }
        /// <summary>
        /// Gets the bottom.
        /// </summary>
        public float Bottom
        {
            get { return Y + Height; }
        }
        /// <summary>
        /// Gets the location.
        /// </summary>
        public Vector2 Location
        {
            get { return new Vector2(X, Y); }
        }
        /// <summary>
        /// Gets the center.
        /// </summary>
        public Vector2 Center
        {
            get { return new Vector2(X + Width * 0.5f, Y + Height * 0.5f); }
        }
        #endregion

        #region Empty Rectangle
        private static readonly Rectangle _empty = new Rectangle(0, 0, 0, 0);
        /// <summary>
        /// Gets an empty rectangle instance.
        /// </summary>
        public static Rectangle Empty
        {
            get { return _empty; }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Determines whether this instance contains the specified rectangle.
        /// </summary>
        /// <param name="value">The value.</param>
        public bool Contains(Rectangle value)
        {
            return
                value.X >= X &&
                value.Y >= Y &&
                value.X + value.Width <= Right &&
                value.Y + Height <= Bottom;
        }
        /// <summary>
        /// Determines whether this instance contains the specified vector.
        /// </summary>
        /// <param name="vector">The vector.</param>
        public bool Contains(Vector2 vector)
        {
            return vector.X > X &&
                   vector.X < Right &&
                   vector.Y > Y &&
                   vector.Y < Bottom;
        }
        /// <summary>
        /// Intersectses the specified rectangle.
        /// </summary>
        /// <param name="rectangle">The rectangle.</param>
        public bool Intersects(Rectangle rectangle)
        {
            return
                !(Left > rectangle.Right ||
                Right < rectangle.Left ||
                Top > rectangle.Bottom ||
                Bottom < rectangle.Top);
        }
        /// <summary>
        /// Intersects the specified rectangle.
        /// </summary>
        /// <param name="rectangle">The rectangle.</param>
        public Rectangle Intersect(Rectangle rectangle)
        {
            if (!Intersects(rectangle))
            {
                return Empty;
            }

            float[] horizontal = { Left, Right, rectangle.Left, rectangle.Right };
            float[] vertical = { Bottom, Top, rectangle.Bottom, rectangle.Top };

            Array.Sort(horizontal);
            Array.Sort(vertical);

            float left = horizontal[1];
            float bottom = vertical[1];
            float right = horizontal[2];
            float top = vertical[2];

            return new Rectangle(left, top, right - left, bottom - top);
        }
        /// <summary>
        /// Converts the Rectangle to a string.
        /// </summary>
        /// <returns>String</returns>
        public override string ToString()
        {
            return X + ";" + Y + ";" + Width + ";" + Height;
        }
        #endregion

        #region Operators
        /// <summary>
        /// Implements the operator +.
        /// </summary>
        /// <param name="rectangle">The rectangle.</param>
        /// <param name="vector">The vector.</param>
        public static Rectangle operator +(Rectangle rectangle, Vector2 vector)
        {
            rectangle.X += vector.X;
            rectangle.Y += vector.Y;

            return rectangle;
        }
        /// <summary>
        /// Implements the operator -.
        /// </summary>
        /// <param name="rectangle">The rectangle.</param>
        /// <param name="vector">The vector.</param>
        public static Rectangle operator -(Rectangle rectangle, Vector2 vector)
        {
            rectangle.X -= vector.X;
            rectangle.Y -= vector.Y;

            return rectangle;
        }
        /// <summary>
        /// Implements the operator *.
        /// </summary>
        /// <param name="rectangle">The rectangle.</param>
        /// <param name="vector">The vector.</param>
        public static Rectangle operator *(Rectangle rectangle, Vector2 vector)
        {
            rectangle.X *= vector.X;
            rectangle.Y *= vector.Y;
            rectangle.Width *= vector.X;
            rectangle.Height *= vector.Y;

            return rectangle;
        }
        /// <summary>
        /// Implements the operator *.
        /// </summary>
        /// <param name="rectangle">The rectangle.</param>
        /// <param name="scale">The scale.</param>
        public static Rectangle operator *(Rectangle rectangle, float scale)
        {
            rectangle.X *= scale;
            rectangle.Y *= scale;
            rectangle.Width *= scale;
            rectangle.Height *= scale;

            return rectangle;
        }
        /// <summary>
        /// Implements the operator /.
        /// </summary>
        /// <param name="rectangle">The rectangle.</param>
        /// <param name="vector">The vector.</param>
        public static Rectangle operator /(Rectangle rectangle, Vector2 vector)
        {
            rectangle.X /= vector.X;
            rectangle.Y /= vector.Y;
            rectangle.Width /= vector.X;
            rectangle.Height /= vector.Y;

            return rectangle;
        }
        /// <summary>
        /// Implements the operator /.
        /// </summary>
        /// <param name="rectangle">The rectangle.</param>
        /// <param name="scale">The scale.</param>
        public static Rectangle operator /(Rectangle rectangle, float scale)
        {
            rectangle.X /= scale;
            rectangle.Y /= scale;
            rectangle.Width /= scale;
            rectangle.Height /= scale;

            return rectangle;
        }
        #endregion
    }
}
