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

using System;
using System.Collections.Generic;

namespace Sharpex2D.Math
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Tested)]
    public struct Polygon : IGeometry
    {
        private readonly List<Vector2> _points;

        /// <summary>
        ///     Initializes a new Polygon class.
        /// </summary>
        /// <param name="points">The Points.</param>
        public Polygon(params Vector2[] points) : this()
        {
            _points = new List<Vector2>();
            _points.AddRange(points);
        }

        /// <summary>
        ///     Initializes a new Polygon class.
        /// </summary>
        /// <param name="points">The Points.</param>
        public Polygon(IEnumerable<Vector2> points) : this()
        {
            _points = new List<Vector2>();
            _points.AddRange(points);
        }

        /// <summary>
        ///     Gets the Center of the Polygon.
        /// </summary>
        public Vector2 Center
        {
            get
            {
                float totalX = 0;
                float totalY = 0;
                foreach (Vector2 t in _points)
                {
                    totalX += t.X;
                    totalY += t.Y;
                }

                return new Vector2(totalX/_points.Count, totalY/_points.Count);
            }
        }

        /// <summary>
        ///     A value indicating whether the Polygon is valid.
        /// </summary>
        public bool IsValid
        {
            get { return _points.Count > 2; }
        }

        /// <summary>
        ///     Gets the coordinates of the Polygon.
        /// </summary>
        public Vector2[] Points
        {
            get { return _points.ToArray(); }
        }

        /// <summary>
        ///     Projects an axis.
        /// </summary>
        /// <param name="axis">The Axis.</param>
        /// <param name="min">The Minimum.</param>
        /// <param name="max">The Maximum.</param>
        private void ProjectTo(Vector2 axis, out float min, out float max)
        {
            min = Vector2.Dot(axis, Points[0]);
            max = min;

            for (int i = 1; i < Points.Length; i++)
            {
                float d = Vector2.Dot(axis, Points[i]);

                if (d < min)
                    min = d;
                if (d > max)
                    max = d;
            }
        }

        /// <summary>
        ///     A value indicating whether the Polygon has a seperating axis.
        /// </summary>
        /// <param name="other">The other Polygon.</param>
        /// <param name="minOverlap">The MinimumOverlap.</param>
        /// <param name="axis">The Axis.</param>
        /// <returns>True of seperating axis.</returns>
        private bool HasSeparatingAxisTo(Polygon other, ref float minOverlap, ref Vector2 axis)
        {
            int prev = Points.Length - 1;
            for (int i = 0; i < Points.Length; i++)
            {
                Vector2 edge = Points[i] - Points[prev];

                var v = new Vector2(edge.X, edge.Y);
                v = v.CrossProduct();
                v.Normalize();

                float aMin, aMax, bMin, bMax;
                ProjectTo(v, out aMin, out aMax);
                other.ProjectTo(v, out bMin, out bMax);

                if ((aMax < bMin) || (bMax < aMin))
                    return true;


                float overlapping = aMax < bMax ? aMax - bMin : bMax - aMin;
                if (overlapping < minOverlap)
                {
                    minOverlap = overlapping;
                    axis = v;
                }

                prev = i;
            }

            return false;
        }

        /// <summary>
        ///     Checks if this Polygon intersects with another Polygon.
        /// </summary>
        /// <param name="other">The other Polygon.</param>
        /// <param name="minimumTranslationVector">
        ///     Returns a vector with which the Polygon has to be translated at minimum to not
        ///     collide with the second Polygon.
        /// </param>
        /// <returns>True of intersecting.</returns>
        public bool Intersects(Polygon other, out Vector2 minimumTranslationVector)
        {
            if (!IsValid || !other.IsValid)
                throw new InvalidOperationException();

            minimumTranslationVector = default(Vector2);
            float minOverlap = float.MaxValue;

            if (HasSeparatingAxisTo(other, ref minOverlap, ref minimumTranslationVector))
                return false;

            if (other.HasSeparatingAxisTo(this, ref minOverlap, ref minimumTranslationVector))
                return false;

            Vector2 d = Center - other.Center;
            if (Vector2.Dot(d, minimumTranslationVector) > 0)
                minimumTranslationVector = -minimumTranslationVector;
            minimumTranslationVector *= minOverlap;

            return true;
        }

        /// <summary>
        ///     Checks if this Polygon intersects with another Polygon.
        /// </summary>
        /// <param name="other">The other Polygon.</param>
        /// <returns>True if intersecting.</returns>
        public bool Intersects(Polygon other)
        {
            Vector2 mtv;
            return Intersects(other, out mtv);
        }

        /// <summary>
        ///     Creates a new Polygon from rectangle.
        /// </summary>
        /// <param name="rectangle">The Rectangle.</param>
        /// <returns>Polygon.</returns>
        public static Polygon FromRectangle(Rectangle rectangle)
        {
            return new Polygon(new Vector2(rectangle.X, rectangle.Y),
                new Vector2(rectangle.X + rectangle.Width, rectangle.Y),
                new Vector2(rectangle.X + rectangle.Width, rectangle.Y + rectangle.Height),
                new Vector2(rectangle.X, rectangle.Y + rectangle.Height));
        }

        /// <summary>
        ///     Creates a new Polygon from ellipse.
        /// </summary>
        /// <param name="ellipse">The Ellipse.</param>
        /// <returns>Polygon.</returns>
        public static Polygon FromEllipse(Ellipse ellipse)
        {
            return new Polygon(ellipse.Points);
        }
    }
}