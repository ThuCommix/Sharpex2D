namespace Sharpex2D.Framework.Math
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Tested)]
    public class PolygonCollisionResult
    {
        /// <summary>
        ///     Initializes a new PolygonCollisionResult class.
        /// </summary>
        internal PolygonCollisionResult()
        {
        }

        /// <summary>
        ///     A value indicating whether the Polygons will intersect.
        /// </summary>
        public bool WillIntersect { get; internal set; }

        /// <summary>
        ///     A value indicating whether the Polygons intersects with each other.
        /// </summary>
        public bool Intersect { get; internal set; }

        /// <summary>
        ///     Gets the MinimumTranslationVector to avoid collision.
        /// </summary>
        public Vector2 MinimumTranslationVector { get; internal set; }
    }
}