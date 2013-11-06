using SharpexGL.Framework.Math;

namespace SharpexGL.Framework.Rendering.Geometry
{
    public interface IGeometryRenderer
    {
        /// <summary>
        /// Draws a Rectangle.
        /// </summary>
        /// <param name="color">The Color.</param>
        /// <param name="rectangle">The Rectangle.</param>
        void DrawRectangle(Color color, Rectangle rectangle);
        /// <summary>
        /// Draws a Line between two points.
        /// </summary>
        /// <param name="color">The Color.</param>
        /// <param name="start">The Startpoint.</param>
        /// <param name="target">The Targetpoint.</param>
        void DrawLine(Color color, Vector2 start, Vector2 target);
        /// <summary>
        /// Draws a Ellipse.
        /// </summary>
        /// <param name="color">The Color.</param>
        /// <param name="rectangle">The Rectangle.</param>
        void DrawEllipse(Color color, Rectangle rectangle);
        /// <summary>
        /// Draws an Arc.
        /// </summary>
        /// <param name="color">The Color.</param>
        /// <param name="rectangle">The Rectangle.</param>
        /// <param name="startAngle">The StartAngle.</param>
        /// <param name="sweepAngle">The SweepAngle.</param>
        void DrawArc(Color color, Rectangle rectangle, float startAngle, float sweepAngle);
        /// <summary>
        /// Draws a Polygon.
        /// </summary>
        /// <param name="color">The Color.</param>
        /// <param name="points">The Points.</param>
        void DrawPolygon(Color color, Vector2[] points);
        /// <summary>
        /// Draws a corner-rounded Rectangle.
        /// </summary>
        /// <param name="color">The Color.</param>
        /// <param name="rectangle">The Rectangle.</param>
        /// <param name="radius">The Radius.</param>
        void DrawRoundedRectangle(Color color, Rectangle rectangle, int radius);
        /// <summary>
        /// Fills a Rectangle.
        /// </summary>
        /// <param name="color">The Color.</param>
        /// <param name="rectangle">The Rectangle.</param>
        void FillRectangle(Color color, Rectangle rectangle);
        /// <summary>
        /// Fills a Ellipse.
        /// </summary>
        /// <param name="color">The Color.</param>
        /// <param name="rectangle">The Rectangle.</param>
        void FillEllipse(Color color, Rectangle rectangle);
        /// <summary>
        /// Fills an Arc.
        /// </summary>
        /// <param name="color">The Color.</param>
        /// <param name="rectangle">The Rectangle.</param>
        /// <param name="startAngle">The StartAngle.</param>
        /// <param name="sweepAngle">The SweepAngle.</param>
        void FillArc(Color color, Rectangle rectangle, float startAngle, float sweepAngle);
        /// <summary>
        /// Fills a Polygon.
        /// </summary>
        /// <param name="color">The Color.</param>
        /// <param name="points">The Points.</param>
        void FillPolygon(Color color, Vector2[] points);
        /// <summary>
        /// Fills a corner-rounded Rectangle.
        /// </summary>
        /// <param name="color">The Color.</param>
        /// <param name="rectangle">The Rectangle.</param>
        /// <param name="radius">The Radius.</param>
        void FillRoundedRectangle(Color color, Rectangle rectangle, int radius);
    }
}
