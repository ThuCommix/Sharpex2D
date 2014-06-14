using System.Collections.Generic;

namespace Sharpex2D.Framework.Math
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Tested)]
    public class Polygon : IGeometry
    {
        private readonly List<Vector2> _points;
        private readonly List<Vector2> _originalPoints;
        private readonly List<Vector2> _edges;
        private Vector2 _offset;
        /// <summary>
        /// Gets the Center of the Polygon.
        /// </summary>
        public Vector2 Center
        {
            get
            {
                float totalX = 0;
                float totalY = 0;
                foreach (var t in _points)
                {
                    totalX += t.X;
                    totalY += t.Y;
                }

                return new Vector2(totalX / _points.Count, totalY / _points.Count);
            }
        }
        /// <summary>
        /// A value indicating whether the Polygon is valid.
        /// </summary>
        public bool IsValid { get { return _points.Count > 2; } }
        /// <summary>
        ///      Gets the coordinates of the Polygon.
        /// </summary>
        public Vector2[] Points { get { return _points.ToArray(); } }
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
        /// Gets the Edges.
        /// </summary>
        public List<Vector2> Edges { get { return _edges; } }
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
            for (var i = 0; i < _points.Count; i++)
            {
                var p1 = _points[i];
                var p2 = i + 1 >= _points.Count ? _points[0] : _points[i + 1];
                _edges.Add(p2 - p1);
            }
        }
        /// <summary>
        ///     Updates the points.
        /// </summary>
        private void UpdatePoints()
        {
            _points.Clear();

            foreach (var p in _originalPoints)
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
            var d = polygon.Points[0].Length;
            min = d;
            max = d;
            foreach (var t in polygon.Points)
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

            var edgeCountA = polygonA.Edges.Count;
			var edgeCountB = polygonB.Edges.Count;
			var minIntervalDistance = float.PositiveInfinity;
			var translationAxis = Vector2.Zero;

			for (var edgeIndex = 0; edgeIndex < edgeCountA + edgeCountB; edgeIndex++)
            {
			    var edge = edgeIndex < edgeCountA ? polygonA.Edges[edgeIndex] : polygonB.Edges[edgeIndex - edgeCountA];

				var axis = new Vector2(-edge.Y, edge.X);
				axis.Normalize();

				float minA;
                float minB; 
                float maxA;
                float maxB;

				ProjectPolygon(axis, polygonA, out minA, out maxA);
				ProjectPolygon(axis, polygonB, out minB, out maxB);

				if (IntervalDistance(minA, maxA, minB, maxB) > 0) result.Intersect = false;


			    var velocityProjection = Vector2.Dot(axis, velocity);

				if (velocityProjection < 0) {
					minA += velocityProjection;
				} else {
					maxA += velocityProjection;
				}

				var intervalDistance = IntervalDistance(minA, maxA, minB, maxB);
				if (intervalDistance > 0) result.WillIntersect = false;

				if (!result.Intersect && !result.WillIntersect) break;

				intervalDistance = MathHelper.Abs(intervalDistance);
				if (intervalDistance < minIntervalDistance) {
					minIntervalDistance = intervalDistance;
					translationAxis = axis;

					var d = polygonA.Center - polygonB.Center;
					if (Vector2.Dot(d, translationAxis) < 0) translationAxis = -translationAxis;
				}
			}

			if (result.WillIntersect) result.MinimumTranslationVector = translationAxis * minIntervalDistance;
			
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
