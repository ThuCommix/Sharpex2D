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
using Sharpex2D.Math;

namespace Sharpex2D.Rendering
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Tested)]
    public class SpriteBatch : IComponent
    {
        internal readonly IRenderer Renderer;

        /// <summary>
        /// Initializes a new SpriteBatch class.
        /// </summary>
        /// <param name="graphicsManager">The GraphicsManager.</param>
        public SpriteBatch(GraphicsManager graphicsManager)
        {
            if (!graphicsManager.IsSupported)
                throw new NotSupportedException("The specified GraphicsManager is not supported.");

            Renderer = graphicsManager.Create();
            Renderer.Initialize();
        }

        /// <summary>
        /// Gets the GraphicsDevice.
        /// </summary>
        public GraphicsDevice GraphicsDevice { internal set; get; }

        /// <summary>
        /// Gets or sets the SmoothingMode.
        /// </summary>
        public SmoothingMode SmoothingMode
        {
            get { return Renderer.SmoothingMode; }
            set { Renderer.SmoothingMode = value; }
        }

        /// <summary>
        /// Gets or sets the InterpolationMode.
        /// </summary>
        public InterpolationMode InterpolationMode
        {
            get { return Renderer.InterpolationMode; }
            set { Renderer.InterpolationMode = value; }
        }

        /// <summary>
        /// Gets the Guid.
        /// </summary>
        public Guid Guid
        {
            get { return new Guid("CCAC7DA7-25FE-4450-94B7-253E8B6D94DE"); }
        }

        /// <summary>
        /// Begins the draw operation.
        /// </summary>
        public void Begin()
        {
            Renderer.Begin();
        }

        /// <summary>
        /// Ends the draw operation.
        /// </summary>
        public void End()
        {
            Renderer.End();
        }

        /// <summary>
        /// Draws a string.
        /// </summary>
        /// <param name="text">The Text.</param>
        /// <param name="font">The Font.</param>
        /// <param name="rectangle">The Rectangle.</param>
        /// <param name="color">The Color.</param>
        public void DrawString(string text, Font font, Rectangle rectangle, Color color)
        {
            Renderer.DrawString(text, font.Instance, rectangle, color);
        }

        /// <summary>
        /// Draws a string.
        /// </summary>
        /// <param name="text">The Text.</param>
        /// <param name="font">The Font.</param>
        /// <param name="position">The Position.</param>
        /// <param name="color">The Color.</param>
        public void DrawString(string text, Font font, Vector2 position, Color color)
        {
            Renderer.DrawString(text, font.Instance, position, color);
        }

        /// <summary>
        /// Draws a Texture.
        /// </summary>
        /// <param name="texture">The Texture.</param>
        /// <param name="position">The Position.</param>
        /// <param name="opacity">The Opacity.</param>
        /// <param name="color">The Color.</param>
        public void DrawTexture(Texture2D texture, Vector2 position, Color color, float opacity = 1f)
        {
            Renderer.DrawTexture(texture.Texture, position, color, opacity);
        }

        /// <summary>
        /// Draws a Texture.
        /// </summary>
        /// <param name="texture">The Texture.</param>
        /// <param name="position">The Position.</param>
        /// <param name="opacity">The Opacity.</param>
        public void DrawTexture(Texture2D texture, Vector2 position, float opacity = 1f)
        {
            DrawTexture(texture, position, Color.White, opacity);
        }

        /// <summary>
        /// Draws a Texture.
        /// </summary>
        /// <param name="texture">The Texture.</param>
        /// <param name="rectangle">The Rectangle.</param>
        /// <param name="opacity">The Opacity.</param>
        /// <param name="color">The Color.</param>
        public void DrawTexture(Texture2D texture, Rectangle rectangle, Color color, float opacity = 1f)
        {
            Renderer.DrawTexture(texture.Texture, rectangle, color, opacity);
        }

        /// <summary>
        /// Draws a Texture.
        /// </summary>
        /// <param name="texture">The Texture.</param>
        /// <param name="rectangle">The Rectangle.</param>
        /// <param name="opacity">The Opacity.</param>
        public virtual void DrawTexture(Texture2D texture, Rectangle rectangle, float opacity = 1f)
        {
            DrawTexture(texture, rectangle, Color.White, opacity);
        }

        /// <summary>
        /// Draws a Texture.
        /// </summary>
        /// <param name="spriteSheet">The SpriteSheet.</param>
        /// <param name="position">The Position.</param>
        /// <param name="color">The Color.</param>
        /// <param name="opacity">The Opacity.</param>
        public void DrawTexture(SpriteSheet spriteSheet, Vector2 position, Color color, float opacity = 1f)
        {
            Renderer.DrawTexture(spriteSheet, position, color, opacity);
        }

        /// <summary>
        /// Draws a Texture.
        /// </summary>
        /// <param name="spriteSheet">The SpriteSheet.</param>
        /// <param name="position">The Position.</param>
        /// <param name="opacity">The Opacity.</param>
        public void DrawTexture(SpriteSheet spriteSheet, Vector2 position, float opacity = 1f)
        {
            DrawTexture(spriteSheet, position, Color.White, opacity);
        }

        /// <summary>
        /// Draws a Texture.
        /// </summary>
        /// <param name="spriteSheet">The SpriteSheet.</param>
        /// <param name="rectangle">The Rectangle.</param>
        /// <param name="color">The Color.</param>
        /// <param name="opacity">The Opacity.</param>
        public void DrawTexture(SpriteSheet spriteSheet, Rectangle rectangle, Color color, float opacity = 1f)
        {
            Renderer.DrawTexture(spriteSheet, rectangle, color, opacity);
        }

        /// <summary>
        /// Draws a Texture.
        /// </summary>
        /// <param name="spriteSheet">The SpriteSheet.</param>
        /// <param name="rectangle">The Rectangle.</param>
        /// <param name="opacity">The Opacity.</param>
        public void DrawTexture(SpriteSheet spriteSheet, Rectangle rectangle, float opacity = 1f)
        {
            DrawTexture(spriteSheet, rectangle, Color.White, opacity);
        }

        /// <summary>
        /// Draws a Texture.
        /// </summary>
        /// <param name="texture">The Texture.</param>
        /// <param name="source">The SourceRectangle.</param>
        /// <param name="destination">The DestinationRectangle.</param>
        /// <param name="color">The Color.</param>
        /// <param name="opacity">The Opacity.</param>
        public void DrawTexture(Texture2D texture, Rectangle source, Rectangle destination, Color color,
            float opacity = 1f)
        {
            Renderer.DrawTexture(texture.Texture, source, destination, color, opacity);
        }

        /// <summary>
        /// Draws a Texture.
        /// </summary>
        /// <param name="texture">The Texture.</param>
        /// <param name="source">The SourceRectangle.</param>
        /// <param name="destination">The DestinationRectangle.</param>
        /// <param name="opacity">The Opacity.</param>
        public void DrawTexture(Texture2D texture, Rectangle source, Rectangle destination, float opacity = 1f)
        {
            DrawTexture(texture, source, destination, Color.White, opacity);
        }

        /// <summary>
        /// Measures the string.
        /// </summary>
        /// <param name="text">The String.</param>
        /// <param name="font">The Font.</param>
        /// <returns>Vector2.</returns>
        public Vector2 MeasureString(string text, Font font)
        {
            return Renderer.MeasureString(text, font.Instance);
        }

        /// <summary>
        /// Sets the Transform.
        /// </summary>
        /// <param name="matrix">The Matrix.</param>
        public void SetTransform(Matrix2x3 matrix)
        {
            Renderer.SetTransform(matrix);
        }

        /// <summary>
        /// Resets the Transform.
        /// </summary>
        public void ResetTransform()
        {
            Renderer.ResetTransform();
        }
    }
}