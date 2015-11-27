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
    public class SpriteBatch : IComponent
    {
        private readonly List<DrawOperation> _currentDrawOperations;
        internal readonly IRenderer Renderer;
        private bool _beginCalled;
        private SpriteSortMode _currentSortMode;
        private bool _endCalled = true;
        private RenderTarget2D _renderTarget;

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
        /// Gets the blend state
        /// </summary>
        public BlendState BlendState { private set; get; }

        /// <summary>
        /// Begins the draw operation
        /// </summary>
        /// <param name="state">The blend state</param>
        /// <param name="mode">The sort mode</param>
        public void Begin(BlendState state, SpriteSortMode mode = SpriteSortMode.Immediate)
        {
            if (!_endCalled)
                throw new GraphicsException("End must be called before Begin can be called.");

            if (BlendState != state)
            {
                BlendState = state;
                Renderer.SetBlendState(state);
            }

            if (_renderTarget != null)
            {
                _renderTarget.ReadOnly = true;
            }

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
                    ? (IEnumerable<DrawOperation>) _currentDrawOperations.OrderBy(x => x.Color.A)
                    : _currentDrawOperations);
                _currentDrawOperations.Clear();
            }

            if (_renderTarget != null)
            {
                _renderTarget.ReadOnly = false;
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
        /// Clears the buffer
        /// </summary>
        /// <param name="color">The color</param>
        public void Clear(Color color)
        {
            Renderer.Clear(color);
        }

        /// <summary>
        /// Creates  a new render target
        /// </summary>
        /// <param name="width">The width</param>
        /// <param name="height">The height</param>
        /// <returns>Returns a new render target</returns>
        public RenderTarget2D CreateRenderTarget(int width, int height)
        {
            return new RenderTarget2D(Renderer.CreateRenderTarget(width, height));
        }

        /// <summary>
        /// Resets the render target
        /// </summary>
        public void ResetRenderTarget()
        {
            if (!_endCalled)
            {
                throw new GraphicsException("Unable to switch render target between draw calls.");
            }

            Renderer.SetDefaultRenderTarget();

            if (_renderTarget != null)
            {
                _renderTarget.ReadOnly = false;
                _renderTarget = null;
            }
        }

        /// <summary>
        /// Sets the render target
        /// </summary>
        /// <param name="renderTarget">The render target</param>
        public void SetRenderTarget(RenderTarget2D renderTarget)
        {
            if (!_endCalled)
            {
                throw new GraphicsException("Unable to switch render target between draw calls.");
            }

            if (_renderTarget != null)
            {
                ResetRenderTarget();
            }

            Renderer.SetRenderTarget(renderTarget.Instance);
            _renderTarget = renderTarget;
        }

        /// <summary>
        /// Draws a string.
        /// </summary>
        /// <param name="text">The Text.</param>
        /// <param name="font">The SpriteFont.</param>
        /// <param name="position">The Position.</param>
        /// <param name="color">The Color.</param>
        public void DrawString(string text, SpriteFont font, Vector2 position, Color color)
        {
            //already buffered since it uses the drawtexture calls internal
            font.DrawText(this, text, position, color);
        }

        /// <summary>
        /// Draws a string.
        /// </summary>
        /// <param name="text">The Text.</param>
        /// <param name="font">The SpriteFont.</param>
        /// <param name="layoutRectangle">The LayoutRectangle.</param>
        /// <param name="format">The TextFormat.</param>
        /// <param name="color">The Color.</param>
        public void DrawString(string text, SpriteFont font, Rectangle layoutRectangle, TextFormat format, Color color)
        {
            //already buffered since it uses the drawtexture calls internal
            font.DrawText(this, text, layoutRectangle, format, color);
        }

        /// <summary>
        /// Draws a Texture.
        /// </summary>
        /// <param name="texture">The Texture.</param>
        /// <param name="position">The Position.</param>
        /// <param name="color">The Color.</param>
        public void DrawTexture(Texture2D texture, Vector2 position, Color color)
        {
            if (_currentSortMode != SpriteSortMode.Immediate)
            {
                _currentDrawOperations.Add(new DrawOperation(texture.Texture,
                    new Rectangle(0, 0, texture.Width, texture.Height),
                    new Rectangle(position.X, position.Y, texture.Width, texture.Height), color));
            }
            else
            {
                Renderer.DrawTexture(texture.Texture, position, color);
            }
        }

        /// <summary>
        /// Draws a Texture.
        /// </summary>
        /// <param name="texture">The Texture.</param>
        /// <param name="position">The Position.</param>
        public void DrawTexture(Texture2D texture, Vector2 position)
        {
            if (_currentSortMode != SpriteSortMode.Immediate)
            {
                _currentDrawOperations.Add(new DrawOperation(texture.Texture,
                    new Rectangle(0, 0, texture.Width, texture.Height),
                    new Rectangle(position.X, position.Y, texture.Width, texture.Height), Color.White));
            }
            else
            {
                DrawTexture(texture, position, Color.White);
            }
        }

        /// <summary>
        /// Draws a Texture.
        /// </summary>
        /// <param name="texture">The Texture.</param>
        /// <param name="rectangle">The Rectangle.</param>
        /// <param name="color">The Color.</param>
        public void DrawTexture(Texture2D texture, Rectangle rectangle, Color color)
        {
            if (_currentSortMode != SpriteSortMode.Immediate)
            {
                _currentDrawOperations.Add(new DrawOperation(texture.Texture,
                    new Rectangle(0, 0, texture.Width, texture.Height),
                    rectangle, color));
            }
            else
            {
                Renderer.DrawTexture(texture.Texture, rectangle, color);
            }
        }

        /// <summary>
        /// Draws a Texture.
        /// </summary>
        /// <param name="texture">The Texture.</param>
        /// <param name="rectangle">The Rectangle.</param>
        public void DrawTexture(Texture2D texture, Rectangle rectangle)
        {
            if (_currentSortMode != SpriteSortMode.Immediate)
            {
                _currentDrawOperations.Add(new DrawOperation(texture.Texture,
                    new Rectangle(0, 0, texture.Width, texture.Height),
                    rectangle, Color.White));
            }
            else
            {
                DrawTexture(texture, rectangle, Color.White);
            }
        }

        /// <summary>
        /// Draws a Texture.
        /// </summary>
        /// <param name="spriteSheet">The SpriteSheet.</param>
        /// <param name="position">The Position.</param>
        /// <param name="color">The Color.</param>
        public void DrawTexture(SpriteSheet spriteSheet, Vector2 position, Color color)
        {
            if (_currentSortMode != SpriteSortMode.Immediate)
            {
                _currentDrawOperations.Add(new DrawOperation(spriteSheet.Texture2D.Texture,
                    spriteSheet.Rectangle,
                    new Rectangle(position.X, position.Y, spriteSheet.Rectangle.Width, spriteSheet.Rectangle.Height),
                    color));
            }
            else
            {
                Renderer.DrawTexture(spriteSheet.Texture2D.Texture, spriteSheet, position, color);
            }
        }

        /// <summary>
        /// Draws a Texture.
        /// </summary>
        /// <param name="spriteSheet">The SpriteSheet.</param>
        /// <param name="position">The Position.</param>
        public void DrawTexture(SpriteSheet spriteSheet, Vector2 position)
        {
            if (_currentSortMode != SpriteSortMode.Immediate)
            {
                _currentDrawOperations.Add(new DrawOperation(spriteSheet.Texture2D.Texture,
                    spriteSheet.Rectangle,
                    new Rectangle(position.X, position.Y, spriteSheet.Rectangle.Width, spriteSheet.Rectangle.Height),
                    Color.White));
            }
            else
            {
                DrawTexture(spriteSheet, position, Color.White);
            }
        }

        /// <summary>
        /// Draws a Texture.
        /// </summary>
        /// <param name="spriteSheet">The SpriteSheet.</param>
        /// <param name="rectangle">The Rectangle.</param>
        /// <param name="color">The Color.</param>
        public void DrawTexture(SpriteSheet spriteSheet, Rectangle rectangle, Color color)
        {
            if (_currentSortMode != SpriteSortMode.Immediate)
            {
                _currentDrawOperations.Add(new DrawOperation(spriteSheet.Texture2D.Texture,
                    spriteSheet.Rectangle, rectangle, color));
            }
            else
            {
                Renderer.DrawTexture(spriteSheet.Texture2D.Texture, spriteSheet, rectangle, color);
            }
        }

        /// <summary>
        /// Draws a Texture.
        /// </summary>
        /// <param name="spriteSheet">The SpriteSheet.</param>
        /// <param name="rectangle">The Rectangle.</param>
        public void DrawTexture(SpriteSheet spriteSheet, Rectangle rectangle)
        {
            if (_currentSortMode != SpriteSortMode.Immediate)
            {
                _currentDrawOperations.Add(new DrawOperation(spriteSheet.Texture2D.Texture,
                    spriteSheet.Rectangle, rectangle, Color.White));
            }
            else
            {
                DrawTexture(spriteSheet, rectangle, Color.White);
            }
        }

        /// <summary>
        /// Draws a Texture.
        /// </summary>
        /// <param name="texture">The Texture.</param>
        /// <param name="source">The SourceRectangle.</param>
        /// <param name="destination">The DestinationRectangle.</param>
        /// <param name="color">The Color.</param>
        public void DrawTexture(Texture2D texture, Rectangle source, Rectangle destination, Color color)
        {
            if (_currentSortMode != SpriteSortMode.Immediate)
            {
                _currentDrawOperations.Add(new DrawOperation(texture.Texture,
                    source, destination, color));
            }
            else
            {
                Renderer.DrawTexture(texture.Texture, source, destination, color);
            }
        }

        /// <summary>
        /// Draws a Texture.
        /// </summary>
        /// <param name="texture">The Texture.</param>
        /// <param name="source">The SourceRectangle.</param>
        /// <param name="destination">The DestinationRectangle.</param>
        public void DrawTexture(Texture2D texture, Rectangle source, Rectangle destination)
        {
            if (_currentSortMode != SpriteSortMode.Immediate)
            {
                _currentDrawOperations.Add(new DrawOperation(texture.Texture,
                    source, destination, Color.White));
            }
            else
            {
                DrawTexture(texture, source, destination, Color.White);
            }
        }

        /// <summary>
        /// Sets the Transform.
        /// </summary>
        /// <param name="matrix">The Matrix.</param>
        public void SetTransform(Matrix matrix)
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
