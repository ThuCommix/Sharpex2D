// Copyright (c) 2012-2015 Sharpex2D - Kevin Scholz (ThuCommix)
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the 'Software'), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED 'AS IS', WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using System;

namespace Sharpex2D.Framework
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Tested)]
    public struct Rectangle
    {
        #region Empty Rectangle

        /// <summary>
        /// Gets an empty Rectangle instance.
        /// </summary>
        public static Rectangle Empty { get; } = new Rectangle(0, 0, 0, 0);

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Rectangle" /> struct.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        public Rectangle(float x, float y, float width, float height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Rectangle" /> struct.
        /// </summary>
        /// <param name="a">The first vector.</param>
        /// <param name="b">The second vector.</param>
        public Rectangle(Vector2 a, Vector2 b)
        {
            X = a.X;
            Y = a.Y;
            Width = b.X;
            Height = b.Y;
        }

        #endregion

        #region Coordinates and Size

        /// <summary>
        /// Gets or sets the height.
        /// </summary>
        public float Height;

        /// <summary>
        /// Gets or sets the width.
        /// </summary>
        public float Width;

        /// <summary>
        /// Gets or sets the X-coordinate.
        /// </summary>
        public float X;

        /// <summary>
        /// Gets or sets the Y-coordinate.
        /// </summary>
        public float Y;

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
            get { return new Vector2(X + Width*0.5f, Y + Height*0.5f); }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Determines whether this instance contains the specified Rectangle.
        /// </summary>
        /// <param name="value">The value.</param>
        public bool Contains(Rectangle value)
        {
            return
                value.X >= X &&
                value.Y >= Y &&
                value.X + value.Width <= Right &&
                value.Y + value.Height <= Bottom;
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
        /// Intersectses the specified Rectangle.
        /// </summary>
        /// <param name="rectangle">The Rectangle.</param>
        public bool Intersects(Rectangle rectangle)
        {
            return
                !(Left > rectangle.Right ||
                  Right < rectangle.Left ||
                  Top > rectangle.Bottom ||
                  Bottom < rectangle.Top);
        }

        /// <summary>
        /// Intersects the specified Rectangle.
        /// </summary>
        /// <param name="rectangle">The Rectangle.</param>
        public Rectangle Intersect(Rectangle rectangle)
        {
            if (!Intersects(rectangle))
            {
                return Empty;
            }

            float[] horizontal = {Left, Right, rectangle.Left, rectangle.Right};
            float[] vertical = {Bottom, Top, rectangle.Bottom, rectangle.Top};

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
        /// <param name="rectangle">The Rectangle.</param>
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
        /// <param name="rectangle">The Rectangle.</param>
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
        /// <param name="rectangle">The Rectangle.</param>
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
        /// <param name="rectangle">The Rectangle.</param>
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
        /// <param name="rectangle">The Rectangle.</param>
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
        /// <param name="rectangle">The Rectangle.</param>
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