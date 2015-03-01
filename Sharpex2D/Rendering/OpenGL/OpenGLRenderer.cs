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
using Sharpex2D.Rendering.OpenGL.Shaders;
using Sharpex2D.Surface;

namespace Sharpex2D.Rendering.OpenGL
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Untested)]
    public class OpenGLRenderer : IRenderer
    {
        private readonly RenderContext _renderContext;
        private readonly GraphicsDevice _graphicsDevice;
        private ShaderProgram _colorShader;
        private readonly TextEntityManager _textEntityManager;
        private float[] _matrix4;
        private readonly GameWindow _window;
        private Vector2 _windowSize;

        /// <summary>
        /// Gets or sets the SmoothingMode.
        /// </summary>
        public SmoothingMode SmoothingMode { get; set; }

        /// <summary>
        /// Gets or sets the InterpolationMode.
        /// </summary>
        public InterpolationMode InterpolationMode { get; set; }

        /// <summary>
        /// Initializes a new OpenGLRenderer class.
        /// </summary>
        public OpenGLRenderer()
        {
            _renderContext = new RenderContext();
            _graphicsDevice = SGL.QueryComponents<GraphicsDevice>();
            _textEntityManager = new TextEntityManager();
            _window = SGL.QueryComponents<RenderTarget>().Window;
        }

        /// <summary>
        /// Initializes the graphics.
        /// </summary>
        public void Initialize()
        {
            _renderContext.Initialize();
            _colorShader = new ShaderProgram();
            var vshader = new VertexShader();
            vshader.Compile(SimpleVertexShader.SourceCode);
            var fshader = new FragmentShader();
            fshader.Compile(SimpleFragmentShader.SourceCode);
            _colorShader.Link(vshader, fshader);
            OpenGLInterops.glEnable(OpenGLInterops.GL_BLEND);
            OpenGLInterops.glBlendFunc(OpenGLInterops.GL_SRC_ALPHA, OpenGLInterops.GL_ONE_MINUS_SRC_ALPHA);
            SetTransform(Matrix2x3.Identity);
        }

        /// <summary>
        /// Begins the draw operation.
        /// </summary>
        public void Begin()
        {
            _renderContext.MakeCurrent();
            OpenGLInterops.glClear(OpenGLInterops.GL_COLOR_BUFFER_BIT);
            var clearColor = OpenGLHelper.ConvertColor(_graphicsDevice.ClearColor);
            OpenGLInterops.glClearColor(clearColor.R, clearColor.G, clearColor.B, clearColor.A);
            _windowSize = _window.Size;
            OpenGLInterops.glViewport(0, 0, (int)_windowSize.X, (int)_windowSize.Y);
        }

        /// <summary>
        /// Ends the draw operation.
        /// </summary>
        public void End()
        {
            _renderContext.SwapBuffers();
        }

        /// <summary>
        /// Draws a string.
        /// </summary>
        /// <param name="text">The Text.</param>
        /// <param name="font">The Font.</param>
        /// <param name="rectangle">The Rectangle.</param>
        /// <param name="color">The Color.</param>
        public void DrawString(string text, IFont font, Rectangle rectangle, Color color)
        {
            var oglFont = font as OpenGLFont;
            if (oglFont == null) throw new InvalidOperationException("Expected a OpenGLFont as resource.");

            var texture = _textEntityManager.GetFontTexture(text, oglFont, color, (int)rectangle.Width);
            DrawTexture(texture, new Vector2(rectangle.X, rectangle.Y), Color.White);
        }

        /// <summary>
        /// Draws a string.
        /// </summary>
        /// <param name="text">The Text.</param>
        /// <param name="font">The Font.</param>
        /// <param name="position">The Position.</param>
        /// <param name="color">The Color.</param>
        public void DrawString(string text, IFont font, Vector2 position, Color color)
        {
            var oglFont = font as OpenGLFont;
            if (oglFont == null) throw new InvalidOperationException("Expected a OpenGLFont as resource.");

            var texture = _textEntityManager.GetFontTexture(text, oglFont, color);
            DrawTexture(texture, position, Color.White);
        }

        /// <summary>
        /// Draws a Texture.
        /// </summary>
        /// <param name="texture">The Texture.</param>
        /// <param name="position">The Position.</param>
        /// <param name="opacity">The Opacity.</param>
        /// <param name="color">The Color.</param>
        public void DrawTexture(ITexture texture, Vector2 position, Color color, float opacity = 1)
        {
            var tex = texture as OpenGLTexture;
            if (tex == null) throw new InvalidOperationException("Expected OpenGLTexture as resource.");
            var col = OpenGLHelper.ConvertColor(color);

            var vertices = new []
            {
                //  Position                                         Color             Texcoords
                position.X, position.Y,                              col.R, col.G, col.B, 0.0f, 0.0f, // Top-left
                position.X + tex.Width, position.Y,                  col.R, col.G, col.B, 1.0f, 0.0f, // Top-right
                position.X + tex.Width, position.Y + tex.Height,     col.R, col.G, col.B, 1.0f, 1.0f, // Bottom-right
                position.X, position.Y + tex.Height,                 col.R, col.G, col.B, 0.0f, 1.0f // Bottom-left
            };

            var elements = new ushort[]
            {
                0, 1, 2,
                2, 3, 0
            };

            var vao = new VertexArray();
            vao.Bind();

            var vbo = new VertexBuffer();
            vbo.Bind();
            vbo.SetData(vertices);

            var ebo = new IndexBuffer();
            ebo.Bind();

            _colorShader.Bind();
            tex.Bind();
            _colorShader.SetUniform("alpha", opacity);
            _colorShader.SetUniform("dim", _graphicsDevice.BackBuffer.Width, _graphicsDevice.BackBuffer.Height);
            _colorShader.SetUniformMatrix("transform", _matrix4);

            ebo.SetData(elements);

            var posAttrib = _colorShader.GetAttribLocation("position");
            VertexBuffer.EnableVertexAttribArray(posAttrib);
            VertexBuffer.VertexAttribPointer(posAttrib, 2, false, 7*sizeof (float), 0);

            var colAttrib = _colorShader.GetAttribLocation("color");
            VertexBuffer.EnableVertexAttribArray(colAttrib);
            VertexBuffer.VertexAttribPointer(colAttrib, 3, false, 7*sizeof (float), 2*sizeof (float));

            var texAttrib = _colorShader.GetAttribLocation("texcoord");
            VertexBuffer.EnableVertexAttribArray(texAttrib);
            VertexBuffer.VertexAttribPointer(texAttrib, 2, false, 7*sizeof (float), 5*sizeof (float));

            OpenGLInterops.glDrawElements(OpenGLInterops.GL_TRIANGLES, 6, OpenGLInterops.GL_UNSIGNED_SHORT, IntPtr.Zero);

            tex.Unbind();
            _colorShader.Unbind();
            ebo.Dispose();
            vbo.Dispose();
            vao.Dispose();
        }

        /// <summary>
        /// Draws a Texture.
        /// </summary>
        /// <param name="texture">The Texture.</param>
        /// <param name="rectangle">The Rectangle.</param>
        /// <param name="opacity">The Opacity.</param>
        /// <param name="color">The Color.</param>
        public void DrawTexture(ITexture texture, Rectangle rectangle, Color color, float opacity = 1)
        {
            var tex = texture as OpenGLTexture;
            if (tex == null) throw new InvalidOperationException("Expected OpenGLTexture as resource.");
            var col = OpenGLHelper.ConvertColor(color);

            var vertices = new[]
            {
                //  Position                                         Color             Texcoords
                rectangle.X, rectangle.Y,                              col.R, col.G, col.B, 0.0f, 0.0f, // Top-left
                rectangle.X + rectangle.Width, rectangle.Y,                  col.R, col.G, col.B, 1.0f, 0.0f, // Top-right
                rectangle.X + rectangle.Width, rectangle.Y + tex.Height,     col.R, col.G, col.B, 1.0f, 1.0f, // Bottom-right
                rectangle.X, rectangle.Y + rectangle.Height,                 col.R, col.G, col.B, 0.0f, 1.0f // Bottom-left
            };

            var elements = new ushort[]
            {
                0, 1, 2,
                2, 3, 0
            };

            var vao = new VertexArray();
            vao.Bind();

            var vbo = new VertexBuffer();
            vbo.Bind();
            vbo.SetData(vertices);

            var ebo = new IndexBuffer();
            ebo.Bind();

            _colorShader.Bind();
            tex.Bind();
            _colorShader.SetUniform("alpha", opacity);
            _colorShader.SetUniform("dim", _graphicsDevice.BackBuffer.Width, _graphicsDevice.BackBuffer.Height);
            _colorShader.SetUniformMatrix("transform", _matrix4);

            ebo.SetData(elements);

            var posAttrib = _colorShader.GetAttribLocation("position");
            VertexBuffer.EnableVertexAttribArray(posAttrib);
            VertexBuffer.VertexAttribPointer(posAttrib, 2, false, 7 * sizeof(float), 0);

            var colAttrib = _colorShader.GetAttribLocation("color");
            VertexBuffer.EnableVertexAttribArray(colAttrib);
            VertexBuffer.VertexAttribPointer(colAttrib, 3, false, 7 * sizeof(float), 2 * sizeof(float));

            var texAttrib = _colorShader.GetAttribLocation("texcoord");
            VertexBuffer.EnableVertexAttribArray(texAttrib);
            VertexBuffer.VertexAttribPointer(texAttrib, 2, false, 7 * sizeof(float), 5 * sizeof(float));

            OpenGLInterops.glDrawElements(OpenGLInterops.GL_TRIANGLES, 6, OpenGLInterops.GL_UNSIGNED_SHORT, IntPtr.Zero);

            tex.Unbind();
            _colorShader.Unbind();
            ebo.Dispose();
            vbo.Dispose();
            vao.Dispose();
        }

        /// <summary>
        /// Draws a Texture.
        /// </summary>
        /// <param name="spriteSheet">The SpriteSheet.</param>
        /// <param name="position">The Position.</param>
        /// <param name="color">The Color.</param>
        /// <param name="opacity">The Opacity.</param>
        public void DrawTexture(SpriteSheet spriteSheet, Vector2 position, Color color, float opacity = 1)
        {
            DrawTexture(spriteSheet,
                new Rectangle(position.X, position.Y, spriteSheet.Rectangle.Width, spriteSheet.Rectangle.Height), color,
                opacity);
        }

        /// <summary>
        /// Draws a Texture.
        /// </summary>
        /// <param name="spriteSheet">The SpriteSheet.</param>
        /// <param name="rectangle">The Rectangle.</param>
        /// <param name="color">The Color.</param>
        /// <param name="opacity">The Opacity.</param>
        public void DrawTexture(SpriteSheet spriteSheet, Rectangle rectangle, Color color, float opacity = 1)
        {
            DrawTexture(spriteSheet.Texture2D.Texture, spriteSheet.Rectangle, rectangle, color, opacity);
        }

        /// <summary>
        /// Draws a Texture.
        /// </summary>
        /// <param name="texture">The Texture.</param>
        /// <param name="source">The SourceRectangle.</param>
        /// <param name="destination">The DestinationRectangle.</param>
        /// <param name="color">The Color.</param>
        /// <param name="opacity">The Opacity.</param>
        public void DrawTexture(ITexture texture, Rectangle source, Rectangle destination, Color color,
            float opacity = 1)
        {
            var tex = texture as OpenGLTexture;
            if (tex == null) throw new InvalidOperationException("Expected OpenGLTexture as resource.");
            var col = OpenGLHelper.ConvertColor(color);

            var oglX = source.X <= 0
                ? 0
                : source.X
                  /texture.Width;
            var oglY = source.Y <= 0
                ? 0
                : source.Y
                  /texture.Height;
            var oglW = source.Width <= 0
                ? 0
                : source.Width
                  / texture.Width;
            var oglH = source.Height <= 0
                ? 0
                : source.Height
                  /texture.Height;

            var vertices = new[]
            {
                //  Position                                                       Color             Texcoords
                destination.X, destination.Y,                                      col.R, col.G, col.B, oglX, oglY, // Top-left
                destination.X + destination.Width, destination.Y,                  col.R, col.G, col.B, oglX + oglW, oglY, // Top-right
                destination.X + destination.Width, destination.Y + destination.Height,     col.R, col.G, col.B, oglX + oglW, oglY + oglH, // Bottom-right
                destination.X, destination.Y + destination.Height,                 col.R, col.G, col.B, oglX, oglY + oglH // Bottom-left
            };

            var elements = new ushort[]
            {
                0, 1, 2,
                2, 3, 0
            };

            var vao = new VertexArray();
            vao.Bind();

            var vbo = new VertexBuffer();
            vbo.Bind();
            vbo.SetData(vertices);

            var ebo = new IndexBuffer();
            ebo.Bind();

            _colorShader.Bind();
            tex.Bind();
            _colorShader.SetUniform("alpha", opacity);
            _colorShader.SetUniform("dim", _graphicsDevice.BackBuffer.Width, _graphicsDevice.BackBuffer.Height);
            _colorShader.SetUniformMatrix("transform", _matrix4);

            ebo.SetData(elements);

            var posAttrib = _colorShader.GetAttribLocation("position");
            VertexBuffer.EnableVertexAttribArray(posAttrib);
            VertexBuffer.VertexAttribPointer(posAttrib, 2, false, 7 * sizeof(float), 0);

            var colAttrib = _colorShader.GetAttribLocation("color");
            VertexBuffer.EnableVertexAttribArray(colAttrib);
            VertexBuffer.VertexAttribPointer(colAttrib, 3, false, 7 * sizeof(float), 2 * sizeof(float));

            var texAttrib = _colorShader.GetAttribLocation("texcoord");
            VertexBuffer.EnableVertexAttribArray(texAttrib);
            VertexBuffer.VertexAttribPointer(texAttrib, 2, false, 7 * sizeof(float), 5 * sizeof(float));

            OpenGLInterops.glDrawElements(OpenGLInterops.GL_TRIANGLES, 6, OpenGLInterops.GL_UNSIGNED_SHORT, IntPtr.Zero);

            tex.Unbind();
            _colorShader.Unbind();
            ebo.Dispose();
            vbo.Dispose();
            vao.Dispose();
        }

        /// <summary>
        /// Measures the string.
        /// </summary>
        /// <param name="text">The String.</param>
        /// <param name="font">The Font.</param>
        /// <returns>Vector2.</returns>
        public Vector2 MeasureString(string text, IFont font)
        {
            var oglFont = font as OpenGLFont;
            if (oglFont == null) throw new InvalidOperationException("Expected a OpenGLFont as resource.");

            var gdiFont = OpenGLHelper.ConvertFont(oglFont);
            var result = System.Windows.Forms.TextRenderer.MeasureText(text, gdiFont);
            return new Vector2(result.Width, result.Height);
        }

        /// <summary>
        /// Sets the Transform.
        /// </summary>
        /// <param name="matrix">The Matrix.</param>
        public void SetTransform(Matrix2x3 matrix)
        {
            var matrixf = new float[16];
            matrixf[0] = matrix[0, 0];
            matrixf[1] = matrix[1, 0];
            matrixf[2] = 0;
            matrixf[3] = 0;

            matrixf[4] = matrix[0, 1];
            matrixf[5] = matrix[1, 1];
            matrixf[6] = 0;
            matrixf[7] = 0;

            matrixf[8] = 0;
            matrixf[9] = 0;
            matrixf[10] = 1;
            matrixf[11] = 0;

            matrixf[12] = matrix.OffsetX;
            matrixf[13] = matrix.OffsetY;
            matrixf[14] = 0;
            matrixf[15] = 1;

            _matrix4 = matrixf;
        }

        /// <summary>
        /// Resets the Transform.
        /// </summary>
        public void ResetTransform()
        {
            SetTransform(Matrix2x3.Identity);
        }

        /// <summary>
        /// Creates a new Resource.
        /// </summary>
        /// <param name="fontFamily">The FontFamily.</param>
        /// <param name="size">The Size.</param>
        /// <param name="accessoire">The TextAccessoire.</param>
        /// <returns>IFont.</returns>
        public IFont CreateResource(string fontFamily, float size, TextAccessoire accessoire)
        {
            return new OpenGLFont(fontFamily, size, accessoire);
        }

        /// <summary>
        /// Creates a new Resource.
        /// </summary>
        /// <param name="path">The Path.</param>
        /// <returns>ITexture.</returns>
        public ITexture CreateResource(string path)
        {
            return new OpenGLTexture((System.Drawing.Bitmap) System.Drawing.Image.FromFile(path));
        }
    } 
}
