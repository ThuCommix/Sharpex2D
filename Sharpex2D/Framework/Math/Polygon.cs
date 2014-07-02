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

namespace Sharpex2D.Framework.Math
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Tested)]
    public class Polygon : IGeometry
    {
        private readonly List<Vector2> _edges;
        private readonly List<Vector2> _originalPoints;
        private readonly List<Vector2> _points;
        private Vector2 _offset;

        /// <summary>
        ///     Initializes a new Polygon class.
        /// </summary>
        public Polygon()
        {
            _points = new List<Vector2>();
            _edges = new List<Vector2>();
            _originalPoints = new List<Vector2>();
            _offset = new Vector2(0, 0);
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
        ///     Sets or gets the Position.
        /// </summary>
        public Vector2 Offset
        {
            set
            {
                _offset = value;
                UpdatePoints();
            }
            get { return _offset; }
        }

        /// <summary>
        ///     Gets the Edges.
        /// </summary>
        public List<Vector2> Edges
        {
            get { return _edges; }
        }

        /// <summary>
        ///     Adds a Vector to the Polygon.
        /// </summary>
        /// <param name="point">The Vector.</param>
        public void Add(Vector2 point)
        {
            _originalPoints.Add(point);
            UpdatePoints();
            UpdateEdges();
        }

        /// <summary>
        ///     Adds a array of Vector to the Polygon.
        /// </summary>
        /// <param name="points">The Vector.</param>
        public void Add(params Vector2[] points)
        {
            _originalPoints.AddRange(points);
            UpdatePoints();
            UpdateEdges();
        }

        /// <summary>
        ///     Resets the Polygon.
        /// </summary>
        public void Reset()
        {
            _points.Clear();
            _originalPoints.Clear();
            _edges.Clear();
        }

        /// <summary>
        ///     Updates the edges.
        /// </summary>
        private void UpdateEdges()
        {
            if (!IsValid) return;

            _edges.Clear();
            for (int i = 0; i < _points.Count; i++)
            {
                Vector2 p1 = _points[i];
                Vector2 p2 = i + 1 >= _points.Count ? _points[0] : _points[i + 1];
                _edges.Add(p2 - p1);
            }
        }

        /// <summary>
        ///     Updates the points.
        /// </summary>
        private void UpdatePoints()
        {
            _points.Clear();

            foreach (Vector2 p in _originalPoints)
            {
                _points.Add(new Vector2(p.X + _offset.X, p.Y + _offset.Y));
            }
        }

        /// <summary>
        ///     Indicating whether the polygon is intersecting with another one.
        /// </summary>
        /// <param name="otherPolygon">The Polygon.</param>
        /// <returns>True if the polygons are intersecting.</returns>
        public bool Intersects(Polygon otherPolygon)
        {
            return PolygonCollision(this, otherPolygon, Vector2.Zero).Intersect;
        }

        /// <summary>
        ///     Gets the collision result between two polygons.
        /// </summary>
        /// <param name="otherPolygon">The Polygon.</param>
        /// <returns>PolygonCollisionResult.</returns>
        public PolygonCollisionResult GetCollisionResult(Polygon otherPolygon)
        {
            return PolygonCollision(this, otherPolygon, new Vector2(0, 0));
        }

        /// <summary>
        ///     Gets the collision result between two polygons.
        /// </summary>
        /// <param name="otherPolygon">The Polygon.</param>
        /// <param name="velocity">The Velocity.</param>
        /// <returns>PolygonCollisionResult.</returns>
        public PolygonCollisionResult GetCollisionResult(Polygon otherPolygon, Vector2 velocity)
        {
            return PolygonCollision(this, otherPolygon, velocity);
        }

        /// <summary>
        ///     Gets the interval distance.
        /// </summary>
        /// <param name="minA">The MinimumA.</param>
        /// <param name="maxA">The MaximumA.</param>
        /// <param name="minB">The MinimumB.</param>
        /// <param name="maxB">The MaximumB.</param>
        /// <returns>Float.</returns>
        private static float IntervalDistance(float minA, float maxA, float minB, float maxB)
        {
            if (minA < minB)
            {
                return minB - maxA;
            }

            return minA - maxB;
        }

        /// <summary>
        ///     Projects the Polygon.
        /// </summary>
        /// <param name="axis">The Axis.</param>
        /// <param name="polygon">The Polygon.</param>
        /// <param name="min">The Minimum.</param>
        /// <param name="max">The Maximum.</param>
        private static void ProjectPolygon(Vector2 axis, Polygon polygon, out float min, out float max)
        {
            float d = polygon.Points[0].Length;
            min = d;
            max = d;
            foreach (Vector2 t in polygon.Points)
            {
                d = Vector2.Dot(t, axis);
                if (d < min)
                {
                    min = d;
                }
                else
                {
                    if (d > max)
                    {
                        max = d;
                    }
                }
            }
        }

        /// <summary>
        ///     Calculates the Polygon collision.
        /// </summary>
        /// <param name="polygonA">The first Polygon.</param>
        /// <param name="polygonB">The second Polygon.</param>
        /// <param name="velocity">The Velocity.</param>
        /// <returns>PolygonCollisionResult.</returns>
        private static PolygonCollisionResult PolygonCollision(Polygon polygonA, Polygon polygonB, Vector2 velocity)
        {
            var result = new PolygonCollisionResult {Intersect = true, WillIntersect = true};

            int edgeCountA = polygonA.Edges.Count;
            int edgeCountB = polygonB.Edges.Count;
            float minIntervalDistance = float.PositiveInfinity;
            Vector2 translationAxis = Vector2.Zero;

            for (int edgeIndex = 0; edgeIndex < edgeCountA + edgeCountB; edgeIndex++)
            {
                Vector2 edge = edgeIndex < edgeCountA
                    ? polygonA.Edges[edgeIndex]
                    : polygonB.Edges[edgeIndex - edgeCountA];

                var axis = new Vector2(-edge.Y, edge.X);
                axis.Normalize();

                float minA;
                float minB;
                float maxA;
                float maxB;

                ProjectPolygon(axis, polygonA, out minA, out maxA);
                ProjectPolygon(axis, polygonB, out minB, out maxB);

                if (IntervalDistance(minA, maxA, minB, maxB) > 0) result.Intersect = false;


                float velocityProjection = Vector2.Dot(axis, velocity);

                if (velocityProjection < 0)
                {
                    minA += velocityProjection;
                }
                else
                {
                    maxA += velocityProjection;
                }

                float intervalDistance = IntervalDistance(minA, maxA, minB, maxB);
                if (intervalDistance > 0) result.WillIntersect = false;

                if (!result.Intersect && !result.WillIntersect) break;

                intervalDistance = MathHelper.Abs(intervalDistance);
                if (intervalDistance < minIntervalDistance)
                {
                    minIntervalDistance = intervalDistance;
                    translationAxis = axis;

                    Vector2 d = polygonA.Center - polygonB.Center;
                    if (Vector2.Dot(d, translationAxis) < 0) translationAxis = -translationAxis;
                }
            }

            if (result.WillIntersect) result.MinimumTranslationVector = translationAxis*minIntervalDistance;

            return result;
        }

        /// <summary>
        ///     Creates a new Polygon from rectangle.
        /// </summary>
        /// <param name="rectangle">The Rectangle.</param>
        /// <returns>Polygon.</returns>
        public static Polygon FromRectangle(Rectangle rectangle)
        {
            var polygon = new Polygon {Offset = new Vector2(0, 0)};
            polygon.Add(new Vector2(rectangle.X, rectangle.Y), new Vector2(rectangle.X + rectangle.Width, rectangle.Y),
                new Vector2(rectangle.X + rectangle.Width, rectangle.Y + rectangle.Height),
                new Vector2(rectangle.X, rectangle.Y + rectangle.Height));
            return polygon;
        }

        /// <summary>
        ///     Creates a new Polygon from ellipse.
        /// </summary>
        /// <param name="ellipse">The Ellipse.</param>
        /// <returns>Polygon.</returns>
        public static Polygon FromEllipse(Ellipse ellipse)
        {
            var polygon = new Polygon {Offset = new Vector2(0, 0)};
            polygon.Add(ellipse.Points);
            return polygon;
        }
    }
}