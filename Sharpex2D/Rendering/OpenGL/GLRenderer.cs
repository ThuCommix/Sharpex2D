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
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace Sharpex2D.Framework.Rendering.OpenGL
{
    internal class GLRenderer : IRenderer
    {
        private readonly RenderContext _renderContext;
        private readonly float[] _staticVertices;
        private GraphicsDevice _graphicsDevice;
        private IndexBuffer _sourceEbo;
        private VertexArray _sourceVao;
        private VertexBuffer _sourceVbo;
        private GameWindow _window;
        private Vector2 _windowSize;
        private BasicGLEffect _basicEffect;
        private Matrix _matrix3;
        private ushort[] _elements;
        private int _targetWidth;
        private int _targetHeight;
        private bool _renderfbo;

        /// <summary>
        /// Initializes a new GLRenderer class.
        /// </summary>
        public GLRenderer()
        {
            _renderContext = new RenderContext();
            _staticVertices = new float[28];
        }

        /// <summary>
        /// Disposes the object.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Initializes the renderer.
        /// </summary>
        /// <param name="game">The Game.</param>
        public void Initialize(Game game)
        {
            _graphicsDevice = game.Get<GraphicsDevice>();
            _window = game.Get<GameWindow>();

            _renderContext.Initialize();
            _basicEffect = new BasicGLEffect();
            _basicEffect.Compile();
            SetTransform(Matrix.Identity);
            _windowSize = _window.ClientSize;
            GLInterops.Viewport(0, 0, (int) _windowSize.X, (int) _windowSize.Y);
            _sourceVao = new VertexArray();
            _sourceVao.Bind();
            _basicEffect.Bind();
            _sourceEbo = new IndexBuffer();

            _targetWidth = _graphicsDevice.GraphicsManager.PreferredBackBufferWidth;
            _targetHeight = _graphicsDevice.GraphicsManager.PreferredBackBufferHeight;

            _elements = new ushort[]
            {
                0, 1, 2,
                2, 3, 0
            };

            _sourceEbo.Bind();
            _sourceEbo.SetData(_elements);

            _sourceVbo = new VertexBuffer();
            _sourceVbo.Bind();

            _window.ClientSizeChanged += WindowScreenSizeChanged;
        }

        /// <summary>
        /// Clears the buffer
        /// </summary>
        /// <param name="color">The color</param>
        public void Clear(Color color)
        {
            _renderContext.MakeCurrent();
            GLInterops.ClearColor(GLHelper.ConvertColor(color));
            GLInterops.Clear();
            if (!_renderfbo)
            {
                GLInterops.Viewport(0, 0, (int)_windowSize.X, (int)_windowSize.Y);
            }
        }

        /// <summary>
        /// Creates  a new render target
        /// </summary>
        /// <param name="width">The width</param>
        /// <param name="height">The height</param>
        /// <returns>Returns a new render target</returns>
        public IRenderTarget2D CreateRenderTarget(int width, int height)
        {
            return new GLFrameBuffer(width, height);
        }

        /// <summary>
        /// Sets the render target
        /// </summary>
        /// <param name="renderTarget">The render target</param>
        public void SetRenderTarget(IRenderTarget2D renderTarget)
        {
            var framebuffer = renderTarget as GLFrameBuffer;
            if (framebuffer == null) throw new ArgumentException("Expected GLFrameBuffer as resource.");

            GLInterops.BindFramebuffer(framebuffer.FramebufferId);
            _renderfbo = true;
            _targetWidth = framebuffer.Width;
            _targetHeight = framebuffer.Height;

            GLInterops.Viewport(0, 0, framebuffer.Width, framebuffer.Height);
            SetTransform(Matrix.Identity);
        }

        /// <summary>
        /// Sets the default render target
        /// </summary>
        public void SetDefaultRenderTarget()
        {
            GLInterops.BindFramebuffer(0);

            _renderfbo = false;
            _targetWidth = _graphicsDevice.GraphicsManager.PreferredBackBufferWidth;
            _targetHeight = _graphicsDevice.GraphicsManager.PreferredBackBufferHeight;

            GLInterops.Viewport(0, 0, (int)_windowSize.X, (int)_windowSize.Y);
            SetTransform(Matrix.Identity);
        }

        /// <summary>
        /// Sets the blend state
        /// </summary>
        /// <param name="blendState">The blend state</param>
        public void SetBlendState(BlendState blendState)
        {
            GLInterops.SetBlendState(blendState);
        }

        /// <summary>
        /// Draws a range of textures.
        /// </summary>
        /// <param name="drawOperations">The DrawOperations.</param>
        public void DrawTextures(IEnumerable<DrawOperation> drawOperations)
        {
            var oldOpacity = 1f;
            
            _basicEffect.SetData("dim", _graphicsDevice.GraphicsManager.PreferredBackBufferWidth,
                _graphicsDevice.GraphicsManager.PreferredBackBufferHeight, oldOpacity);
            _basicEffect.SetData("transform", _matrix3);

            uint posAttrib = _basicEffect.GetAttribLocation("position");
            VertexBuffer.EnableVertexAttribArray(posAttrib);
            VertexBuffer.VertexAttribPointer(posAttrib, 2, false, 7*sizeof (float), 0);

            uint colAttrib = _basicEffect.GetAttribLocation("color");
            VertexBuffer.EnableVertexAttribArray(colAttrib);
            VertexBuffer.VertexAttribPointer(colAttrib, 3, false, 7*sizeof (float), 2*sizeof (float));

            uint texAttrib = _basicEffect.GetAttribLocation("texcoord");
            VertexBuffer.EnableVertexAttribArray(texAttrib);
            VertexBuffer.VertexAttribPointer(texAttrib, 2, false, 7*sizeof (float), 5*sizeof (float));

            foreach (var operation in drawOperations)
            {
                var tex = operation.Texture as GLTexture;
                if (tex == null) throw new ArgumentException("Expected OpenGLTexture as resource.");
                GLColor col = GLHelper.ConvertColor(operation.Color);

                float oglX = operation.Source.X <= 0
                    ? 0
                    : operation.Source.X
                      /operation.Texture.Width;
                float oglY = operation.Source.Y <= 0
                    ? 0
                    : operation.Source.Y
                      /operation.Texture.Height;
                float oglW = operation.Source.Width <= 0
                    ? 0
                    : operation.Source.Width
                      /operation.Texture.Width;
                float oglH = operation.Source.Height <= 0
                    ? 0
                    : operation.Source.Height
                      /operation.Texture.Height;

                _staticVertices[0] = operation.Destination.X;
                _staticVertices[1] = operation.Destination.Y;
                _staticVertices[2] = col.R;
                _staticVertices[3] = col.G;
                _staticVertices[4] = col.B;
                _staticVertices[5] = oglX;
                _staticVertices[6] = oglY;

                _staticVertices[7] = operation.Destination.X + operation.Destination.Width;
                _staticVertices[8] = operation.Destination.Y;
                _staticVertices[9] = col.R;
                _staticVertices[10] = col.G;
                _staticVertices[11] = col.B;
                _staticVertices[12] = oglX + oglW;
                _staticVertices[13] = oglY;

                _staticVertices[14] = operation.Destination.X + operation.Destination.Width;
                _staticVertices[15] = operation.Destination.Y + operation.Destination.Height;
                _staticVertices[16] = col.R;
                _staticVertices[17] = col.G;
                _staticVertices[18] = col.B;
                _staticVertices[19] = oglX + oglW;
                _staticVertices[20] = oglY + oglH;

                _staticVertices[21] = operation.Destination.X;
                _staticVertices[22] = operation.Destination.Y + operation.Destination.Height;
                _staticVertices[23] = col.R;
                _staticVertices[24] = col.G;
                _staticVertices[25] = col.B;
                _staticVertices[26] = oglX;
                _staticVertices[27] = oglY + oglH;


                _sourceVbo.SetData(_staticVertices);

                //if the opacity differs, send the updated information to the color shader
                if (oldOpacity != operation.Color.A/255f)
                {
                    oldOpacity = operation.Color.A/255f;
                    _basicEffect.SetData("dim", _targetWidth, _targetHeight, oldOpacity);
                }

                tex.Bind();
                GLInterops.DrawElements(DrawMode.Triangles, 6, DataTypes.UShort, IntPtr.Zero);
                tex.Unbind();
            }
        }

        /// <summary>
        /// Presents the buffer.
        /// </summary>
        public void Present()
        {
            _renderContext.SwapBuffers();
        }

        /// <summary>
        /// Draws a Texture.
        /// </summary>
        /// <param name="texture">The Texture.</param>
        /// <param name="position">The Position.</param>
        /// <param name="color">The Color.</param>
        public void DrawTexture(ITexture texture, Vector2 position, Color color)
        {
            var tex = texture as GLTexture;
            if (tex == null) throw new ArgumentException("Expected OpenGLTexture as resource.");
            GLColor col = GLHelper.ConvertColor(color);

            var vertices = new[]
            {
                //  Position                                         Color             Texcoords
                position.X, position.Y, col.R, col.G, col.B, 0.0f, 0.0f, // Top-left
                position.X + tex.Width, position.Y, col.R, col.G, col.B, 1.0f, 0.0f, // Top-right
                position.X + tex.Width, position.Y + tex.Height, col.R, col.G, col.B, 1.0f, 1.0f, // Bottom-right
                position.X, position.Y + tex.Height, col.R, col.G, col.B, 0.0f, 1.0f // Bottom-left
            };

            _sourceVbo.SetData(vertices);

            tex.Bind();

            _basicEffect.SetData("dim", _targetWidth, _targetHeight, col.A);
            _basicEffect.SetData("transform", _matrix3);

            uint posAttrib = _basicEffect.GetAttribLocation("position");
            VertexBuffer.EnableVertexAttribArray(posAttrib);
            VertexBuffer.VertexAttribPointer(posAttrib, 2, false, 7*sizeof (float), 0);

            uint colAttrib = _basicEffect.GetAttribLocation("color");
            VertexBuffer.EnableVertexAttribArray(colAttrib);
            VertexBuffer.VertexAttribPointer(colAttrib, 3, false, 7*sizeof (float), 2*sizeof (float));

            uint texAttrib = _basicEffect.GetAttribLocation("texcoord");
            VertexBuffer.EnableVertexAttribArray(texAttrib);
            VertexBuffer.VertexAttribPointer(texAttrib, 2, false, 7*sizeof (float), 5*sizeof (float));

            GLInterops.DrawElements(DrawMode.Triangles, 6, DataTypes.UShort, IntPtr.Zero);

            tex.Unbind();
        }

        /// <summary>
        /// Draws a Texture.
        /// </summary>
        /// <param name="texture">The Texture.</param>
        /// <param name="rectangle">The Rectangle.</param>
        /// <param name="color">The Color.</param>
        public void DrawTexture(ITexture texture, Rectangle rectangle, Color color)
        {
            var tex = texture as GLTexture;
            if (tex == null) throw new ArgumentException("Expected OpenGLTexture as resource.");
            GLColor col = GLHelper.ConvertColor(color);

            var vertices = new[]
            {
                //  Position                                         Color             Texcoords
                rectangle.X, rectangle.Y, col.R, col.G, col.B, 0.0f, 0.0f, // Top-left
                rectangle.X + rectangle.Width, rectangle.Y, col.R, col.G, col.B, 1.0f, 0.0f, // Top-right
                rectangle.X + rectangle.Width, rectangle.Y + rectangle.Height, col.R, col.G, col.B, 1.0f, 1.0f,
                // Bottom-right
                rectangle.X, rectangle.Y + rectangle.Height, col.R, col.G, col.B, 0.0f, 1.0f // Bottom-left
            };

            _sourceVbo.SetData(vertices);

            tex.Bind();

            _basicEffect.SetData("dim", _targetWidth, _targetHeight, col.A);
            _basicEffect.SetData("transform", _matrix3);

            uint posAttrib = _basicEffect.GetAttribLocation("position");
            VertexBuffer.EnableVertexAttribArray(posAttrib);
            VertexBuffer.VertexAttribPointer(posAttrib, 2, false, 7*sizeof (float), 0);

            uint colAttrib = _basicEffect.GetAttribLocation("color");
            VertexBuffer.EnableVertexAttribArray(colAttrib);
            VertexBuffer.VertexAttribPointer(colAttrib, 3, false, 7*sizeof (float), 2*sizeof (float));

            uint texAttrib = _basicEffect.GetAttribLocation("texcoord");
            VertexBuffer.EnableVertexAttribArray(texAttrib);
            VertexBuffer.VertexAttribPointer(texAttrib, 2, false, 7*sizeof (float), 5*sizeof (float));

            GLInterops.DrawElements(DrawMode.Triangles, 6, DataTypes.UShort, IntPtr.Zero);

            tex.Unbind();
        }

        /// <summary>
        /// Draws a Texture.
        /// </summary>
        /// <param name="texture">The Texture.</param>
        /// <param name="spriteSheet">The SpriteSheet.</param>
        /// <param name="position">The Position.</param>
        /// <param name="color">The Color.</param>
        public void DrawTexture(ITexture texture, SpriteSheet spriteSheet, Vector2 position, Color color)
        {
            DrawTexture(texture, spriteSheet,
                new Rectangle(position.X, position.Y, spriteSheet.Rectangle.Width, spriteSheet.Rectangle.Height), color);
        }

        /// <summary>
        /// Draws a Texture.
        /// </summary>
        /// <param name="texture">The Texture.</param>
        /// <param name="spriteSheet">The SpriteSheet.</param>
        /// <param name="rectangle">The Rectangle.</param>
        /// <param name="color">The Color.</param>
        public void DrawTexture(ITexture texture, SpriteSheet spriteSheet, Rectangle rectangle, Color color)
        {
            DrawTexture(texture, spriteSheet.Rectangle, rectangle, color);
        }

        /// <summary>
        /// Draws a Texture.
        /// </summary>
        /// <param name="texture">The Texture.</param>
        /// <param name="source">The SourceRectangle.</param>
        /// <param name="destination">The DestinationRectangle.</param>
        /// <param name="color">The Color.</param>
        public void DrawTexture(ITexture texture, Rectangle source, Rectangle destination, Color color)
        {
            var tex = texture as GLTexture;
            if (tex == null) throw new ArgumentException("Expected OpenGLTexture as resource.");
            GLColor col = GLHelper.ConvertColor(color);

            float oglX = source.X <= 0
                ? 0
                : source.X
                  /texture.Width;
            float oglY = source.Y <= 0
                ? 0
                : source.Y
                  /texture.Height;
            float oglW = source.Width <= 0
                ? 0
                : source.Width
                  /texture.Width;
            float oglH = source.Height <= 0
                ? 0
                : source.Height
                  /texture.Height;

            var vertices = new[]
            {
                //  Position                                                       Color             Texcoords
                destination.X, destination.Y, col.R, col.G, col.B, oglX, oglY, // Top-left
                destination.X + destination.Width, destination.Y, col.R, col.G, col.B, oglX + oglW, oglY, // Top-right
                destination.X + destination.Width, destination.Y + destination.Height, col.R, col.G, col.B, oglX + oglW,
                oglY + oglH, // Bottom-right
                destination.X, destination.Y + destination.Height, col.R, col.G, col.B, oglX, oglY + oglH // Bottom-left
            };

            _sourceVbo.SetData(vertices);

            tex.Bind();

            _basicEffect.SetData("dim", _targetWidth, _targetHeight, col.A);
            _basicEffect.SetData("transform", _matrix3);

            uint posAttrib = _basicEffect.GetAttribLocation("position");
            VertexBuffer.EnableVertexAttribArray(posAttrib);
            VertexBuffer.VertexAttribPointer(posAttrib, 2, false, 7*sizeof (float), 0);

            uint colAttrib = _basicEffect.GetAttribLocation("color");
            VertexBuffer.EnableVertexAttribArray(colAttrib);
            VertexBuffer.VertexAttribPointer(colAttrib, 3, false, 7*sizeof (float), 2*sizeof (float));

            uint texAttrib = _basicEffect.GetAttribLocation("texcoord");
            VertexBuffer.EnableVertexAttribArray(texAttrib);
            VertexBuffer.VertexAttribPointer(texAttrib, 2, false, 7*sizeof (float), 5*sizeof (float));

            GLInterops.DrawElements(DrawMode.Triangles, 6, DataTypes.UShort, IntPtr.Zero);

            tex.Unbind();
        }

        /// <summary>
        /// Sets the Transform.
        /// </summary>
        /// <param name="matrix">The Matrix.</param>
        public void SetTransform(Matrix matrix)
        {
            _matrix3 = matrix;
        }

        /// <summary>
        /// Resets the Transform.
        /// </summary>
        public void ResetTransform()
        {
            SetTransform(Matrix.Identity);
        }

        /// <summary>
        /// Creates a new Resource.
        /// </summary>
        /// <param name="path">The Path.</param>
        /// <returns>ITexture.</returns>
        public ITexture CreateTexture(string path)
        {
            return new GLTexture((Bitmap) Image.FromFile(path));
        }

        /// <summary>
        /// Creates a new Resource.
        /// </summary>
        /// <param name="stream">The Stream.</param>
        /// <returns>ITexture.</returns>
        public ITexture CreateTexture(Stream stream)
        {
            return new GLTexture(stream);
        }

        /// <summary>
        /// Creates a new Resource.
        /// </summary>
        /// <param name="width">The Width.</param>
        /// <param name="height">The Height.</param>
        /// <returns>ITexture.</returns>
        public ITexture CreateTexture(int width, int height)
        {
            var emptyBmp = new Bitmap(width, height, PixelFormat.Format32bppArgb);
            Graphics graphics = Graphics.FromImage(emptyBmp);
            graphics.Clear(System.Drawing.Color.Transparent);
            graphics.Dispose();
            return new GLTexture(emptyBmp);
        }

        /// <summary>
        /// Deconstructs the OpenGLRenderer class.
        /// </summary>
        ~GLRenderer()
        {
            Dispose(false);
        }

        /// <summary>
        /// Disposes the object.
        /// </summary>
        /// <param name="disposing">The disposing state.</param>
        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                _renderContext.Dispose();
            }
        }

        /// <summary>
        /// Triggered if the screen size changed.
        /// </summary>
        /// <param name="sender">The Sender.</param>
        /// <param name="e">The EventArgs.</param>
        private void WindowScreenSizeChanged(object sender, EventArgs e)
        {
            _windowSize = _window.ClientSize;
        }
    }
}
