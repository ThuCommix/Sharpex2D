// Copyright (c) 2012-2014 Sharpex2D - Kevin Scholz (ThuCommix)
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

using System.Collections.Generic;

namespace Sharpex2D.Math
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Tested)]
    public struct Ellipse : IGeometry
    {
        private Polygon _polygon;
        private Vector2 _position;

        /// <summary>
        /// Initializes a new Ellipse class.
        /// </summary>
        /// <param name="radiusX">The X-Radius.</param>
        /// <param name="radiusY">The Y-Radius.</param>
        /// <param name="Position">The position.</param>
        public Ellipse(float radiusX, float radiusY, Vector2 Position)
            : this()
        {
            RadiusX = radiusX;
            RadiusY = radiusY;
            _position = Position;
            _polygon = new Polygon();
            UpdateEllipse();
        }

        /// <summary>
        /// Initializes a new Ellipse class.
        /// </summary>
        /// <param name="Radiuses">Vector representing X and Y radiuses</param>
        /// <param name="radiusY">The position.</param>
        public Ellipse(Vector2 Radiuses, Vector2 Position)
            : this(Radiuses.X, Radiuses.Y, Position)
        {
        }

        /// <summary>
        /// Initializes a new Ellipse class.
        /// </summary>
        /// <param name="radiusX">The X-Radius.</param>
        /// <param name="radiusY">The Y-Radius.</param>
        public Ellipse(float radiusX, float radiusY)
            : this(radiusX, radiusY, Vector2.Zero)
        {
        }

        /// <summary>
        /// Sets or gets the Position.
        /// </summary>
        public Vector2 Position
        {
            set
            {
                _position = value;
                UpdateEllipse();
            }
            get { return _position; }
        }

        /// <summary>
        /// Gets the X-Radius.
        /// </summary>
        public float RadiusX { private set; get; }

        /// <summary>
        /// Gets the Y-Radius.
        /// </summary>
        public float RadiusY { private set; get; }

        /// <summary>
        /// Gets the points.
        /// </summary>
        public Vector2[] Points
        {
            get { return _polygon.Points; }
        }

        /// <summary>
        /// Updates the Ellipse if something changed.
        /// </summary>
        private void UpdateEllipse()
        {
            var points = new List<Vector2>();
            for (int i = 1; i <= 360; i++)
            {
                points.Add(new Vector2(RadiusX*MathHelper.Cos(i*(float) MathHelper.PiOverOneEighty) + _position.X,
                    RadiusY*MathHelper.Sin(i*(float) MathHelper.PiOverOneEighty) + _position.Y));
            }
            _polygon = new Polygon(points.ToArray());
        }
    }
}