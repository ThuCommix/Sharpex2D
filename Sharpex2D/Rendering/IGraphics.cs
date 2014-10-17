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

using Sharpex2D.Content.Pipeline;
using Sharpex2D.Math;

namespace Sharpex2D.Rendering
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Tested)]
    public interface IGraphics
    {
        /// <summary>
        /// Gets the ResourceManager.
        /// </summary>
        ResourceManager ResourceManager { get; }

        /// <summary>
        /// Gets the ContentProcessors.
        /// </summary>
        IContentProcessor[] ContentProcessors { get; }

        /// <summary>
        /// Gets or sets the SmoothingMode.
        /// </summary>
        SmoothingMode SmoothingMode { get; set; }

        /// <summary>
        /// Gets or sets the InterpolationMode.
        /// </summary>
        InterpolationMode InterpolationMode { get; set; }

        /// <summary>
        /// Initializes the graphics.
        /// </summary>
        void Initialize();

        /// <summary>
        /// Begins the draw operation.
        /// </summary>
        void Begin();

        /// <summary>
        /// Ends the draw operation.
        /// </summary>
        void End();

        /// <summary>
        /// Draws a string.
        /// </summary>
        /// <param name="text">The Text.</param>
        /// <param name="font">The Font.</param>
        /// <param name="rectangle">The Rectangle.</param>
        /// <param name="color">The Color.</param>
        void DrawString(string text, Font font, Rectangle rectangle, Color color);

        /// <summary>
        /// Draws a string.
        /// </summary>
        /// <param name="text">The Text.</param>
        /// <param name="font">The Font.</param>
        /// <param name="position">The Position.</param>
        /// <param name="color">The Color.</param>
        void DrawString(string text, Font font, Vector2 position, Color color);

        /// <summary>
        /// Draws a Texture.
        /// </summary>
        /// <param name="texture">The Texture.</param>
        /// <param name="position">The Position.</param>
        /// <param name="opacity">The Opacity.</param>
        /// <param name="color">The Color.</param>
        void DrawTexture(Texture2D texture, Vector2 position, Color color, float opacity = 1f);

        /// <summary>
        /// Draws a Texture.
        /// </summary>
        /// <param name="texture">The Texture.</param>
        /// <param name="rectangle">The Rectangle.</param>
        /// <param name="opacity">The Opacity.</param>
        /// <param name="color">The Color.</param>
        void DrawTexture(Texture2D texture, Rectangle rectangle, Color color, float opacity = 1f);

        /// <summary>
        /// Draws a Texture.
        /// </summary>
        /// <param name="spriteSheet">The SpriteSheet.</param>
        /// <param name="position">The Position.</param>
        /// <param name="color">The Color.</param>
        /// <param name="opacity">The Opacity.</param>
        void DrawTexture(SpriteSheet spriteSheet, Vector2 position, Color color, float opacity = 1f);

        /// <summary>
        /// Draws a Texture.
        /// </summary>
        /// <param name="spriteSheet">The SpriteSheet.</param>
        /// <param name="rectangle">The Rectangle.</param>
        /// <param name="color">The Color.</param>
        /// <param name="opacity">The Opacity.</param>
        void DrawTexture(SpriteSheet spriteSheet, Rectangle rectangle, Color color, float opacity = 1f);

        /// <summary>
        /// Draws a Texture.
        /// </summary>
        /// <param name="texture">The Texture.</param>
        /// <param name="source">The SourceRectangle.</param>
        /// <param name="destination">The DestinationRectangle.</param>
        /// <param name="color">The Color.</param>
        /// <param name="opacity">The Opacity.</param>
        void DrawTexture(Texture2D texture, Rectangle source, Rectangle destination, Color color,
            float opacity = 1f);

        /// <summary>
        /// Measures the string.
        /// </summary>
        /// <param name="text">The String.</param>
        /// <param name="font">The Font.</param>
        /// <returns>Vector2.</returns>
        Vector2 MeasureString(string text, Font font);

        /// <summary>
        /// Sets the Transform.
        /// </summary>
        /// <param name="matrix">The Matrix.</param>
        void SetTransform(Matrix2x3 matrix);

        /// <summary>
        /// Resets the Transform.
        /// </summary>
        void ResetTransform();

        /// <summary>
        /// Draws a Rectangle.
        /// </summary>
        /// <param name="pen">The Pen.</param>
        /// <param name="rectangle">The Rectangle.</param>
        void DrawRectangle(Pen pen, Rectangle rectangle);

        /// <summary>
        /// Draws a Line between two points.
        /// </summary>
        /// <param name="pen">The Pen.</param>
        /// <param name="start">The Startpoint.</param>
        /// <param name="target">The Targetpoint.</param>
        void DrawLine(Pen pen, Vector2 start, Vector2 target);

        /// <summary>
        /// Draws a Ellipse.
        /// </summary>
        /// <param name="pen">The Pen.</param>
        /// <param name="ellipse">The Ellipse.</param>
        void DrawEllipse(Pen pen, Ellipse ellipse);

        /// <summary>
        /// Draws an Arc.
        /// </summary>
        /// <param name="pen">The Pen.</param>
        /// <param name="rectangle">The Rectangle.</param>
        /// <param name="startAngle">The StartAngle.</param>
        /// <param name="sweepAngle">The SweepAngle.</param>
        void DrawArc(Pen pen, Rectangle rectangle, float startAngle, float sweepAngle);

        /// <summary>
        /// Draws a Polygon.
        /// </summary>
        /// <param name="pen">The Pen.</param>
        /// <param name="polygon">The Polygon.</param>
        void DrawPolygon(Pen pen, Polygon polygon);

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
        /// <param name="ellipse">The Ellipse.</param>
        void FillEllipse(Color color, Ellipse ellipse);

        /// <summary>
        /// Fills a Polygon.
        /// </summary>
        /// <param name="color">The Color.</param>
        /// <param name="polygon">The Polygon.</param>
        void FillPolygon(Color color, Polygon polygon);
    }
}