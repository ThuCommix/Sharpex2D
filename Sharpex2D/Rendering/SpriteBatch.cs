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
using System.Linq;

namespace Sharpex2D.Framework.Rendering
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Tested)]
    public class SpriteBatch : IComponent
    {
        internal readonly IRenderer Renderer;
        private SpriteSortMode _currentSortMode;
        private bool _beginCalled;
        private bool _endCalled = true;
        private readonly List<DrawOperation> _currentDrawOperations; 

        /// <summary>
        /// Initializes a new SpriteBatch class.
        /// </summary>
        /// <param name="graphicsManager">The GraphicsManager.</param>
        public SpriteBatch(GraphicsManager graphicsManager)
        {
            if (!graphicsManager.IsSupported)
                throw new NotSupportedException("The specified GraphicsManager is not supported.");

            _currentDrawOperations = new List<DrawOperation>();
            Renderer = graphicsManager.Create();
            Renderer.Initialize(GameHost.GameInstance);
        }

        /// <summary>
        /// Gets the GraphicsDevice.
        /// </summary>
        public GraphicsDevice GraphicsDevice { internal set; get; }

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
            Begin(SpriteSortMode.Immediate);
        }

        /// <summary>
        /// Begins the draw operation.
        /// </summary>
        public void Begin(SpriteSortMode mode)
        {
            if (!_endCalled)
                throw new GraphicsException("End must be called before Begin can be called.");

            _currentSortMode = mode;
            _beginCalled = true;
        }

        /// <summary>
        /// Ends the draw operation.
        /// </summary>
        public void End()
        {
            if (!_beginCalled)
                throw new GraphicsException("Begin must be called before End can be called.");

            if (_currentSortMode != SpriteSortMode.Immediate)
            {
                Renderer.DrawTextures(_currentSortMode == SpriteSortMode.Alpha
                    ? (IEnumerable<DrawOperation>) _currentDrawOperations.OrderBy(x => x.Opacity)
                    : _currentDrawOperations);
                _currentDrawOperations.Clear();
            }

            _endCalled = true;
        }

        /// <summary>
        /// Presents the buffer.
        /// </summary>
        internal void Present()
        {
            Renderer.Present();
        }

        /// <summary>
        /// Clears the buffer.
        /// </summary>
        internal void Clear()
        {
            Renderer.Clear();
        }

        /// <summary>
        /// Draws a string.
        /// </summary>
        /// <param name="text">The Text.</param>
        /// <param name="font">The SpriteFont.</param>
        /// <param name="position">The Position.</param>
        /// <param name="color">The Color.</param>
        /// <param name="opacity">The Opacity.</param>
        public void DrawString(string text, SpriteFont font, Vector2 position, Color color, float opacity = 1f)
        {
            //already buffered since it uses the drawtexture calls internal
            font.DrawText(this, text, position, color, opacity);
        }

        /// <summary>
        /// Draws a string.
        /// </summary>
        /// <param name="text">The Text.</param>
        /// <param name="font">The SpriteFont.</param>
        /// <param name="layoutRectangle">The LayoutRectangle.</param>
        /// <param name="format">The TextFormat.</param>
        /// <param name="color">The Color.</param>
        /// <param name="opacity">The Opacity.</param>
        public void DrawString(string text, SpriteFont font, Rectangle layoutRectangle, TextFormat format, Color color,
            float opacity = 1f)
        {
            //already buffered since it uses the drawtexture calls internal
            font.DrawText(this, text, layoutRectangle, format, color, opacity);
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
            if (_currentSortMode != SpriteSortMode.Immediate)
            {
                _currentDrawOperations.Add(new DrawOperation(texture.Texture,
                    new Rectangle(0, 0, texture.Width, texture.Height),
                    new Rectangle(position.X, position.Y, texture.Width, texture.Height), color, opacity));
            }
            else
            {
                Renderer.DrawTexture(texture.Texture, position, color, opacity);
            }
        }

        /// <summary>
        /// Draws a Texture.
        /// </summary>
        /// <param name="texture">The Texture.</param>
        /// <param name="position">The Position.</param>
        /// <param name="opacity">The Opacity.</param>
        public void DrawTexture(Texture2D texture, Vector2 position, float opacity = 1f)
        {
            if (_currentSortMode != SpriteSortMode.Immediate)
            {
                _currentDrawOperations.Add(new DrawOperation(texture.Texture,
                    new Rectangle(0, 0, texture.Width, texture.Height),
                    new Rectangle(position.X, position.Y, texture.Width, texture.Height), Color.White, opacity));
            }
            else
            {
                DrawTexture(texture, position, Color.White, opacity);
            }
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
            if (_currentSortMode != SpriteSortMode.Immediate)
            {
                _currentDrawOperations.Add(new DrawOperation(texture.Texture,
                    new Rectangle(0, 0, texture.Width, texture.Height),
                    rectangle, color, opacity));
            }
            else
            {
                Renderer.DrawTexture(texture.Texture, rectangle, color, opacity);
            }
        }

        /// <summary>
        /// Draws a Texture.
        /// </summary>
        /// <param name="texture">The Texture.</param>
        /// <param name="rectangle">The Rectangle.</param>
        /// <param name="opacity">The Opacity.</param>
        public void DrawTexture(Texture2D texture, Rectangle rectangle, float opacity = 1f)
        {
            if (_currentSortMode != SpriteSortMode.Immediate)
            {
                _currentDrawOperations.Add(new DrawOperation(texture.Texture,
                    new Rectangle(0, 0, texture.Width, texture.Height),
                    rectangle, Color.White, opacity));
            }
            else
            {
                DrawTexture(texture, rectangle, Color.White, opacity);
            }
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
            if (_currentSortMode != SpriteSortMode.Immediate)
            {
                _currentDrawOperations.Add(new DrawOperation(spriteSheet.Texture2D.Texture,
                    spriteSheet.Rectangle,
                    new Rectangle(position.X, position.Y, spriteSheet.Rectangle.Width, spriteSheet.Rectangle.Height),
                    color, opacity));
            }
            else
            {
                Renderer.DrawTexture(spriteSheet.Texture2D.Texture, spriteSheet, position, color, opacity);
            }
        }

        /// <summary>
        /// Draws a Texture.
        /// </summary>
        /// <param name="spriteSheet">The SpriteSheet.</param>
        /// <param name="position">The Position.</param>
        /// <param name="opacity">The Opacity.</param>
        public void DrawTexture(SpriteSheet spriteSheet, Vector2 position, float opacity = 1f)
        {
            if (_currentSortMode != SpriteSortMode.Immediate)
            {
                _currentDrawOperations.Add(new DrawOperation(spriteSheet.Texture2D.Texture,
                    spriteSheet.Rectangle,
                    new Rectangle(position.X, position.Y, spriteSheet.Rectangle.Width, spriteSheet.Rectangle.Height),
                    Color.White, opacity));
            }
            else
            {
                DrawTexture(spriteSheet, position, Color.White, opacity);
            }
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
            if (_currentSortMode != SpriteSortMode.Immediate)
            {
                _currentDrawOperations.Add(new DrawOperation(spriteSheet.Texture2D.Texture,
                    spriteSheet.Rectangle, rectangle, color, opacity));
            }
            else
            {
                Renderer.DrawTexture(spriteSheet.Texture2D.Texture, spriteSheet, rectangle, color, opacity);
            }
        }

        /// <summary>
        /// Draws a Texture.
        /// </summary>
        /// <param name="spriteSheet">The SpriteSheet.</param>
        /// <param name="rectangle">The Rectangle.</param>
        /// <param name="opacity">The Opacity.</param>
        public void DrawTexture(SpriteSheet spriteSheet, Rectangle rectangle, float opacity = 1f)
        {
            if (_currentSortMode != SpriteSortMode.Immediate)
            {
                _currentDrawOperations.Add(new DrawOperation(spriteSheet.Texture2D.Texture,
                    spriteSheet.Rectangle, rectangle, Color.White, opacity));
            }
            else
            {
                DrawTexture(spriteSheet, rectangle, Color.White, opacity);
            }
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
            if (_currentSortMode != SpriteSortMode.Immediate)
            {
                _currentDrawOperations.Add(new DrawOperation(texture.Texture,
                    source, destination, color, opacity));
            }
            else
            {
                Renderer.DrawTexture(texture.Texture, source, destination, color, opacity);
            }
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
            if (_currentSortMode != SpriteSortMode.Immediate)
            {
                _currentDrawOperations.Add(new DrawOperation(texture.Texture,
                    source, destination, Color.White, opacity));
            }
            else
            {
                DrawTexture(texture, source, destination, Color.White, opacity);
            }
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