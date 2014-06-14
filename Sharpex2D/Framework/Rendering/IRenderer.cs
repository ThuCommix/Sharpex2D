using System;
using Sharpex2D.Framework.Components;
using Sharpex2D.Framework.Math;
using Sharpex2D.Framework.Rendering.Font;

namespace Sharpex2D.Framework.Rendering
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Tested)]
    public interface IRenderer : IConstructable, IDisposable
    {
        /// <summary>
        ///     Sets or gets the GraphicsDevice.
        /// </summary>
        GraphicsDevice GraphicsDevice { get; set; }

        /// <summary>
        ///     Sets or gets whether the renderer is disposed.
        /// </summary>
        bool IsDisposed { get; }

        /// <summary>
        ///     A value indicating whether VSync is enabled.
        /// </summary>
        bool VSync { set; get; }

        /// <summary>
        ///     Begins the draw operation.
        /// </summary>
        void Begin();

        /// <summary>
        ///     Flushes the buffer.
        /// </summary>
        void Close();

        /// <summary>
        ///     Draws a string.
        /// </summary>
        /// <param name="text">The Text.</param>
        /// <param name="font">The Font.</param>
        /// <param name="rectangle">The Rectangle.</param>
        /// <param name="color">The Color.</param>
        void DrawString(string text, IFont font, Rectangle rectangle, Color color);

        /// <summary>
        ///     Draws a string.
        /// </summary>
        /// <param name="text">The Text.</param>
        /// <param name="font">The Font.</param>
        /// <param name="position">The Position.</param>
        /// <param name="color">The Color.</param>
        void DrawString(string text, IFont font, Vector2 position, Color color);

        /// <summary>
        ///     Draws a Texture.
        /// </summary>
        /// <param name="texture">The Texture.</param>
        /// <param name="position">The Position.</param>
        /// <param name="opacity">The Opacity.</param>
        /// <param name="color">The Color.</param>
        void DrawTexture(ITexture texture, Vector2 position, Color color, float opacity = 1f);

        /// <summary>
        ///     Draws a Texture.
        /// </summary>
        /// <param name="texture">The Texture.</param>
        /// <param name="rectangle">The Rectangle.</param>
        /// <param name="opacity">The Opacity.</param>
        /// <param name="color">The Color.</param>
        void DrawTexture(ITexture texture, Rectangle rectangle, Color color, float opacity = 1f);

        /// <summary>
        ///     Measures the string.
        /// </summary>
        /// <param name="text">The String.</param>
        /// <param name="font">The Font.</param>
        /// <returns>Vector2.</returns>
        Vector2 MeasureString(string text, IFont font);

        /// <summary>
        ///     Sets the Transform.
        /// </summary>
        /// <param name="matrix">The Matrix.</param>
        void SetTransform(Matrix2x3 matrix);

        /// <summary>
        ///     Resets the Transform.
        /// </summary>
        void ResetTransform();

        /// <summary>
        ///     Draws a Rectangle.
        /// </summary>
        /// <param name="pen">The Pen.</param>
        /// <param name="rectangle">The Rectangle.</param>
        void DrawRectangle(IPen pen, Rectangle rectangle);

        /// <summary>
        ///     Draws a Line between two points.
        /// </summary>
        /// <param name="pen">The Pen.</param>
        /// <param name="start">The Startpoint.</param>
        /// <param name="target">The Targetpoint.</param>
        void DrawLine(IPen pen, Vector2 start, Vector2 target);

        /// <summary>
        ///     Draws a Ellipse.
        /// </summary>
        /// <param name="pen">The Pen.</param>
        /// <param name="ellipse">The Ellipse.</param>
        void DrawEllipse(IPen pen, Ellipse ellipse);

        /// <summary>
        ///     Draws an Arc.
        /// </summary>
        /// <param name="pen">The Pen.</param>
        /// <param name="rectangle">The Rectangle.</param>
        /// <param name="startAngle">The StartAngle.</param>
        /// <param name="sweepAngle">The SweepAngle.</param>
        void DrawArc(IPen pen, Rectangle rectangle, float startAngle, float sweepAngle);

        /// <summary>
        ///     Draws a Polygon.
        /// </summary>
        /// <param name="pen">The Pen.</param>
        /// <param name="polygon">The Polygon.</param>
        void DrawPolygon(IPen pen, Polygon polygon);

        /// <summary>
        ///     Draws a corner-rounded Rectangle.
        /// </summary>
        /// <param name="pen">The Pen.</param>
        /// <param name="rectangle">The Rectangle.</param>
        /// <param name="radius">The Radius.</param>
        void DrawRoundedRectangle(IPen pen, Rectangle rectangle, int radius);

        /// <summary>
        ///     Fills a Rectangle.
        /// </summary>
        /// <param name="color">The Color.</param>
        /// <param name="rectangle">The Rectangle.</param>
        void FillRectangle(Color color, Rectangle rectangle);

        /// <summary>
        ///     Fills a Ellipse.
        /// </summary>
        /// <param name="color">The Color.</param>
        /// <param name="ellipse">The Ellipse.</param>
        void FillEllipse(Color color, Ellipse ellipse);

        /// <summary>
        ///     Fills a Polygon.
        /// </summary>
        /// <param name="color">The Color.</param>
        /// <param name="polygon">The Polygon.</param>
        void FillPolygon(Color color, Polygon polygon);

        /// <summary>
        ///     Fills a corner-rounded Rectangle.
        /// </summary>
        /// <param name="color">The Color.</param>
        /// <param name="rectangle">The Rectangle.</param>
        /// <param name="radius">The Radius.</param>
        void FillRoundedRectangle(Color color, Rectangle rectangle, int radius);
    }
}