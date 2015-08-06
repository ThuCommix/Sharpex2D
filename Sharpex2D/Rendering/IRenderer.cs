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
using System.Collections.Generic;
using System.IO;

namespace Sharpex2D.Framework.Rendering
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Tested)]
    public interface IRenderer : IDisposable
    {
        /// <summary>
        /// Initializes the renderer.
        /// </summary>
        /// <param name="game">The Game.</param>
        void Initialize(Game game);

        /// <summary>
        /// Clears the buffer.
        /// </summary>
        void Clear();

        /// <summary>
        /// Draws a Texture.
        /// </summary>
        /// <param name="texture">The Texture.</param>
        /// <param name="position">The Position.</param>
        /// <param name="opacity">The Opacity.</param>
        /// <param name="color">The Color.</param>
        void DrawTexture(ITexture texture, Vector2 position, Color color, float opacity = 1f);

        /// <summary>
        /// Draws a Texture.
        /// </summary>
        /// <param name="texture">The Texture.</param>
        /// <param name="rectangle">The Rectangle.</param>
        /// <param name="opacity">The Opacity.</param>
        /// <param name="color">The Color.</param>
        void DrawTexture(ITexture texture, Rectangle rectangle, Color color, float opacity = 1f);

        /// <summary>
        /// Draws a Texture.
        /// </summary>
        /// <param name="texture">The Texture.</param>
        /// <param name="spriteSheet">The SpriteSheet.</param>
        /// <param name="position">The Position.</param>
        /// <param name="color">The Color.</param>
        /// <param name="opacity">The Opacity.</param>
        void DrawTexture(ITexture texture, SpriteSheet spriteSheet, Vector2 position, Color color, float opacity = 1f);

        /// <summary>
        /// Draws a Texture.
        /// </summary>
        /// <param name="texture">The Texture.</param>
        /// <param name="spriteSheet">The SpriteSheet.</param>
        /// <param name="rectangle">The Rectangle.</param>
        /// <param name="color">The Color.</param>
        /// <param name="opacity">The Opacity.</param>
        void DrawTexture(ITexture texture, SpriteSheet spriteSheet, Rectangle rectangle, Color color, float opacity = 1f);

        /// <summary>
        /// Draws a Texture.
        /// </summary>
        /// <param name="texture">The Texture.</param>
        /// <param name="source">The SourceRectangle.</param>
        /// <param name="destination">The DestinationRectangle.</param>
        /// <param name="color">The Color.</param>
        /// <param name="opacity">The Opacity.</param>
        void DrawTexture(ITexture texture, Rectangle source, Rectangle destination, Color color,
            float opacity = 1f);

        /// <summary>
        /// Draws a range of textures.
        /// </summary>
        /// <param name="drawOperations">The DrawOperations.</param>
        void DrawTextures(IEnumerable<DrawOperation> drawOperations);

        /// <summary>
        /// Presents the buffer.
        /// </summary>
        void Present();

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
        /// Creates a new Resource.
        /// </summary>
        /// <param name="path">The Path.</param>
        /// <returns>ITexture.</returns>
        ITexture CreateResource(string path);

        /// <summary>
        /// Creates a new Resource.
        /// </summary>
        /// <param name="width">The Width.</param>
        /// <param name="height">The Height.</param>
        /// <returns>ITexture.</returns>
        ITexture CreateResource(int width, int height);

        /// <summary>
        /// Creates a new Resource.
        /// </summary>
        /// <param name="stream">The Stream.</param>
        /// <returns>ITexture.</returns>
        ITexture CreateResource(Stream stream);
    }
}