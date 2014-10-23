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
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Sharpex2D.Common.Extensions;
using Sharpex2D.Content.Pipeline;
using Sharpex2D.Content.Pipeline.Processor;
using Sharpex2D.Math;
using Sharpex2D.Rendering.OpenGL.Windows;
using Sharpex2D.Surface;
using Rectangle = Sharpex2D.Math.Rectangle;

namespace Sharpex2D.Rendering.OpenGL
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Tested)]
    public class OpenGLGraphics : IGraphics
    {
        private const int UnitCacheCount = 10;
        private readonly Graphics _graphics;
        private readonly GraphicsDevice _graphicsDevice;
        private readonly Dictionary<int, PreRenderedText> _preRenderedUnits;
        private readonly IRenderContext _renderContext;
        private readonly RenderTarget _renderTarget;
        private Vector2 _oldScale;

        /// <summary>
        /// Initializes a new OpenGLGraphics class.
        /// </summary>
        public OpenGLGraphics()
        {
            ResourceManager = new OpenGLResourceManager();
            SmoothingMode = SmoothingMode.AntiAlias;
            InterpolationMode = InterpolationMode.Linear;
            ContentProcessors = new IContentProcessor[] {new OpenGLTextureContentProcessor()};
            _preRenderedUnits = new Dictionary<int, PreRenderedText>();

            _graphicsDevice = SGL.QueryComponents<GraphicsDevice>();
            _renderTarget = SGL.QueryComponents<RenderTarget>();
            _graphics = Graphics.FromHwnd(_renderTarget.Handle);
            _oldScale = new Vector2(1, 1);

#if Windows
            //Windows render context
            _renderContext = new NativeRenderContext();
#else
    //TODO MacOSX and Linux need different render context creations.
            throw new NotImplementedException();
#endif
        }

        /// <summary>
        /// Gets the ResourceManager.
        /// </summary>
        public ResourceManager ResourceManager { get; private set; }

        /// <summary>
        /// Gets the ContentProcessors.
        /// </summary>
        public IContentProcessor[] ContentProcessors { get; private set; }

        /// <summary>
        /// Gets or sets the SmoothingMode.
        /// </summary>
        public SmoothingMode SmoothingMode { get; set; }

        /// <summary>
        /// Gets or sets the InterpolationMode.
        /// </summary>
        public InterpolationMode InterpolationMode { get; set; }

        /// <summary>
        /// Initializes the graphics.
        /// </summary>
        public void Initialize()
        {
            _renderContext.Create(_graphicsDevice.BackBuffer.Width, _graphicsDevice.BackBuffer.Height, 24,
                _renderTarget.Handle);

            OpenGL.glShadeModel(OpenGL.GL_FLAT);
            OpenGL.glMatrixMode(OpenGL.GL_PROJECTION);
            OpenGL.glLoadIdentity();
            OpenGL.glOrtho(0, _graphicsDevice.BackBuffer.Width, _graphicsDevice.BackBuffer.Height, 0,
                1, -1);
            OpenGL.glEnable(OpenGL.GL_BLEND);
            OpenGL.glBlendFunc(OpenGL.GL_SRC_ALPHA, OpenGL.GL_ONE_MINUS_SRC_ALPHA);
            OpenGL.glClearDepth(1f);
            OpenGLColor oglColor = OpenGLHelper.ConvertColor(_graphicsDevice.ClearColor);
            OpenGL.glClearColor(oglColor.R, oglColor.G, oglColor.B, oglColor.A);

            if (SmoothingMode == SmoothingMode.AntiAlias)
            {
                OpenGL.glHint(OpenGL.GL_LINE_SMOOTH_HINT, OpenGL.GL_NICEST);
                OpenGL.glEnable(OpenGL.GL_LINE_SMOOTH);
            }
        }

        /// <summary>
        /// Begins the draw operation.
        /// </summary>
        public void Begin()
        {
            _renderContext.MakeCurrent();
            if (_preRenderedUnits.Count > UnitCacheCount)
            {
                PreRenderedText unit = _preRenderedUnits.Values.First();
                unit.Dispose();
                _preRenderedUnits.Remove(unit.Identifer);
            }
            OpenGLColor oglColor = OpenGLHelper.ConvertColor(_graphicsDevice.ClearColor);
            OpenGL.glClearColor(oglColor.R, oglColor.G, oglColor.B, oglColor.A);
            OpenGL.glClear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
            OpenGL.glColor4f(1, 1, 1, 1);

            if (_graphicsDevice.BackBuffer.Scaling)
            {
                if (_graphicsDevice.Scale != _oldScale)
                {
                    OpenGL.glViewport(0, 0, (int) _renderTarget.Window.Size.X, (int) _renderTarget.Window.Size.Y);
                    OpenGL.glMatrixMode(OpenGL.GL_PROJECTION);
                    OpenGL.glLoadIdentity();

                    OpenGL.glOrtho(0, _graphicsDevice.BackBuffer.Width, _graphicsDevice.BackBuffer.Height, 0,
                        1, -1);

                    _oldScale = _graphicsDevice.Scale;
                }
            }
        }

        /// <summary>
        /// Ends the draw operation.
        /// </summary>
        public void End()
        {
            IntPtr hdc = _graphics.GetHdc();
            _renderContext.Blit(hdc);
            _graphics.ReleaseHdc(hdc);
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
            DrawString(text.WordWrap((int) rectangle.Width), font, new Vector2(rectangle.X, rectangle.Y), color);
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
            var oglFont = font.Instance as OpenGLFont;
            if (oglFont == null) throw new ArgumentException("OpenGLRenderDevice expects a OpenGLFont as resource.");

            var prt = new PreRenderedText(oglFont.GetFont(), color, text);
            if (!_preRenderedUnits.ContainsKey(prt.Identifer))
            {
                _preRenderedUnits.Add(prt.Identifer, prt);
                prt.RenderFont();
                DrawTexture(prt.OpenGLTexture, position, Color.White);
            }
            else
            {
                DrawTexture(_preRenderedUnits[prt.Identifer].OpenGLTexture, position, Color.White);
            }
        }

        /// <summary>
        /// Draws a Texture.
        /// </summary>
        /// <param name="texture">The Texture.</param>
        /// <param name="position">The Position.</param>
        /// <param name="opacity">The Opacity.</param>
        /// <param name="color">The Color.</param>
        public void DrawTexture(Texture2D texture, Vector2 position, Color color, float opacity = 1)
        {
            DrawTexture(texture, new Rectangle(position.X, position.Y, texture.Width, texture.Height), color, opacity);
        }

        /// <summary>
        /// Draws a Texture.
        /// </summary>
        /// <param name="texture">The Texture.</param>
        /// <param name="rectangle">The Rectangle.</param>
        /// <param name="opacity">The Opacity.</param>
        /// <param name="color">The Color.</param>
        public void DrawTexture(Texture2D texture, Rectangle rectangle, Color color, float opacity = 1)
        {
            var oglTexture = texture as OpenGLTexture;
            if (oglTexture == null)
                throw new ArgumentException("OpenGLRenderDevice expects a OpenGLTexture as resource.");

            if (!oglTexture.IsBinded)
            {
                oglTexture.BindIfUnbinded();
            }

            OpenGL.glEnable(OpenGL.GL_TEXTURE_2D);
            OpenGL.glBindTexture(OpenGL.GL_TEXTURE_2D, (uint) oglTexture.TextureId);

            if (color != Color.White)
            {
                OpenGL.glColor4f(color.R, color.G, color.B,
                    color.A/255f*opacity);
            }
            else
            {
                OpenGL.glColor4f(1f, 1f, 1f, opacity);
            }

            OpenGL.glBegin(OpenGL.GL_QUADS);
            OpenGL.glTexCoord2f(0, 0);
            OpenGL.glVertex2f(rectangle.X, rectangle.Y);
            OpenGL.glTexCoord2f(1, 0);
            OpenGL.glVertex2f(rectangle.X + rectangle.Width,
                rectangle.Y);
            OpenGL.glTexCoord2f(1, 1);
            OpenGL.glVertex2f(rectangle.X + rectangle.Width,
                rectangle.Y + rectangle.Height);
            OpenGL.glTexCoord2f(0, 1);
            OpenGL.glVertex2f(rectangle.X,
                rectangle.Y + rectangle.Height);
            OpenGL.glEnd();

            OpenGL.glDisable(OpenGL.GL_TEXTURE_2D);
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
            var oglTexture = spriteSheet.Texture2D as OpenGLTexture;
            if (oglTexture == null)
                throw new ArgumentException("OpenGLRenderDevice expects a OpenGLTexture as resource.");

            if (!oglTexture.IsBinded)
            {
                oglTexture.BindIfUnbinded();
            }

            float oglX = spriteSheet.Rectangle.X == 0
                ? 0
                : spriteSheet.Rectangle.X
                  /spriteSheet.Texture2D.Width;
            float oglY = spriteSheet.Rectangle.Y == 0
                ? 0
                : spriteSheet.Rectangle.Y
                  /spriteSheet.Texture2D.Height;
            float oglW = spriteSheet.Rectangle.Width == 0
                ? 0
                : spriteSheet.Rectangle.Width
                  /spriteSheet.Texture2D.Width;
            float oglH = spriteSheet.Rectangle.Height == 0
                ? 0
                : spriteSheet.Rectangle.Height
                  /spriteSheet.Texture2D.Height;

            OpenGL.glEnable(OpenGL.GL_TEXTURE_2D);
            OpenGL.glBindTexture(OpenGL.GL_TEXTURE_2D, (uint) oglTexture.TextureId);

            if (color != Color.White)
            {
                OpenGL.glColor4f(color.R, color.G, color.B,
                    color.A/255f*opacity);
            }
            else
            {
                OpenGL.glColor4f(1f, 1f, 1f, opacity);
            }

            OpenGL.glBegin(OpenGL.GL_QUADS);
            OpenGL.glTexCoord2f(oglX, oglY);
            OpenGL.glVertex2f(rectangle.X, rectangle.Y);
            OpenGL.glTexCoord2f(oglX + oglW, oglY);
            OpenGL.glVertex2f(rectangle.X + rectangle.Width, rectangle.Y);
            OpenGL.glTexCoord2f(oglX + oglW, oglY + oglH);
            OpenGL.glVertex2f(rectangle.X + rectangle.Width, rectangle.Y + rectangle.Height);
            OpenGL.glTexCoord2f(oglX, oglY + oglH);
            OpenGL.glVertex2f(rectangle.X, rectangle.Y + rectangle.Height);
            OpenGL.glEnd();
            OpenGL.glDisable(OpenGL.GL_TEXTURE_2D);
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
            float opacity = 1)
        {
            var oglTexture = texture as OpenGLTexture;
            if (oglTexture == null)
                throw new ArgumentException("OpenGLRenderDevice expects a OpenGLTexture as resource.");

            if (!oglTexture.IsBinded)
            {
                oglTexture.BindIfUnbinded();
            }

            float oglX = source.X == 0
                ? 0
                : source.X
                  /texture.Width;
            float oglY = source.Y == 0
                ? 0
                : source.Y
                  /texture.Height;
            float oglW = source.Width == 0
                ? 0
                : source.Width
                  /texture.Width;
            float oglH = source.Height == 0
                ? 0
                : source.Height
                  /texture.Height;

            OpenGL.glEnable(OpenGL.GL_TEXTURE_2D);
            OpenGL.glBindTexture(OpenGL.GL_TEXTURE_2D, (uint) oglTexture.TextureId);

            if (color != Color.White)
            {
                OpenGL.glColor4f(color.R, color.G, color.B,
                    color.A/255f*opacity);
            }
            else
            {
                OpenGL.glColor4f(1f, 1f, 1f, opacity);
            }

            OpenGL.glBegin(OpenGL.GL_QUADS);
            OpenGL.glTexCoord2f(oglX, oglY);
            OpenGL.glVertex2f(destination.X, destination.Y);
            OpenGL.glTexCoord2f(oglX + oglW, oglY);
            OpenGL.glVertex2f(destination.X + destination.Width, destination.Y);
            OpenGL.glTexCoord2f(oglX + oglW, oglY + oglH);
            OpenGL.glVertex2f(destination.X + destination.Width, destination.Y + destination.Height);
            OpenGL.glTexCoord2f(oglX, oglY + oglH);
            OpenGL.glVertex2f(destination.X, destination.Y + destination.Height);
            OpenGL.glEnd();
            OpenGL.glDisable(OpenGL.GL_TEXTURE_2D);
        }

        /// <summary>
        /// Measures the string.
        /// </summary>
        /// <param name="text">The String.</param>
        /// <param name="font">The Font.</param>
        /// <returns>Vector2.</returns>
        public Vector2 MeasureString(string text, Font font)
        {
            var oglFont = font.Instance as OpenGLFont;
            if (oglFont == null) throw new ArgumentException("OpenGLRenderDevice expects a OpenGLFont as resource.");

            SizeF result = _graphics.MeasureString(text, oglFont.GetFont());
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
            OpenGL.glMatrixMode(OpenGL.GL_MODELVIEW);
            OpenGL.glLoadMatrixf(matrixf);
        }

        /// <summary>
        /// Resets the Transform.
        /// </summary>
        public void ResetTransform()
        {
            OpenGL.glMatrixMode(OpenGL.GL_MODELVIEW);
            OpenGL.glLoadIdentity();
        }

        /// <summary>
        /// Draws a Rectangle.
        /// </summary>
        /// <param name="pen">The Pen.</param>
        /// <param name="rectangle">The Rectangle.</param>
        public void DrawRectangle(Pen pen, Rectangle rectangle)
        {
            var oglPen = pen.Instance as OpenGLPen;
            if (oglPen == null) throw new ArgumentException("OpenGLRenderDevice expects a OpenGLPen as resource.");

            OpenGL.glLineWidth(oglPen.Width);
            OpenGL.glColor4f(oglPen.Color.R, oglPen.Color.G, oglPen.Color.B,
                oglPen.PreCalculatedAlpha);
            OpenGL.glBegin(OpenGL.GL_LINE_LOOP);
            OpenGL.glVertex2f(rectangle.X, rectangle.Y);
            OpenGL.glVertex2f(rectangle.X + rectangle.Width,
                rectangle.Y);
            OpenGL.glVertex2f(rectangle.X + rectangle.Width,
                rectangle.Y + rectangle.Height);
            OpenGL.glVertex2f(rectangle.X,
                rectangle.Y + rectangle.Height);
            OpenGL.glEnd();
        }

        /// <summary>
        /// Draws a Line between two points.
        /// </summary>
        /// <param name="pen">The Pen.</param>
        /// <param name="start">The Startpoint.</param>
        /// <param name="target">The Targetpoint.</param>
        public void DrawLine(Pen pen, Vector2 start, Vector2 target)
        {
            var oglPen = pen.Instance as OpenGLPen;
            if (oglPen == null) throw new ArgumentException("OpenGLRenderDevice expects a OpenGLPen as resource.");

            OpenGL.glLineWidth(oglPen.Width);
            OpenGL.glColor4f(oglPen.Color.R, oglPen.Color.G, oglPen.Color.B,
                oglPen.PreCalculatedAlpha);
            OpenGL.glBegin(OpenGL.GL_LINES);
            OpenGL.glVertex2f(start.X, start.Y);
            OpenGL.glVertex2f(target.X, target.Y);
            OpenGL.glEnd();
        }

        /// <summary>
        /// Draws a Ellipse.
        /// </summary>
        /// <param name="pen">The Pen.</param>
        /// <param name="ellipse">The Ellipse.</param>
        public void DrawEllipse(Pen pen, Ellipse ellipse)
        {
            var oglPen = pen.Instance as OpenGLPen;
            if (oglPen == null) throw new ArgumentException("OpenGLRenderDevice expects a OpenGLPen as resource.");

            OpenGL.glLineWidth(oglPen.Width);
            OpenGL.glColor4f(oglPen.Color.R, oglPen.Color.G, oglPen.Color.B,
                oglPen.PreCalculatedAlpha);
            OpenGL.glBegin(OpenGL.GL_LINE_STRIP);

            Vector2[] ellipsePoints = ellipse.Points;

            foreach (Vector2 t in ellipsePoints)
            {
                OpenGL.glVertex2f(t.X, t.Y);
            }

            OpenGL.glEnd();
        }

        /// <summary>
        /// Draws an Arc.
        /// </summary>
        /// <param name="pen">The Pen.</param>
        /// <param name="rectangle">The Rectangle.</param>
        /// <param name="startAngle">The StartAngle.</param>
        /// <param name="sweepAngle">The SweepAngle.</param>
        public void DrawArc(Pen pen, Rectangle rectangle, float startAngle, float sweepAngle)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Draws a Polygon.
        /// </summary>
        /// <param name="pen">The Pen.</param>
        /// <param name="polygon">The Polygon.</param>
        public void DrawPolygon(Pen pen, Polygon polygon)
        {
            var oglPen = pen.Instance as OpenGLPen;
            if (oglPen == null) throw new ArgumentException("OpenGLRenderDevice expects a OpenGLPen as resource.");

            OpenGL.glLineWidth(oglPen.Width);
            OpenGL.glColor4f(oglPen.Color.R, oglPen.Color.G, oglPen.Color.B,
                oglPen.PreCalculatedAlpha);
            OpenGL.glBegin(OpenGL.GL_LINE_LOOP);

            Vector2[] polygonVectors = polygon.Points;

            foreach (Vector2 t in polygonVectors)
            {
                OpenGL.glVertex2f(t.X, t.Y);
            }

            OpenGL.glEnd();
        }

        /// <summary>
        /// Fills a Rectangle.
        /// </summary>
        /// <param name="color">The Color.</param>
        /// <param name="rectangle">The Rectangle.</param>
        public void FillRectangle(Color color, Rectangle rectangle)
        {
            OpenGL.glLineWidth(1f);
            OpenGL.glColor4f(color.R, color.G, color.B,
                color.A/255f);
            OpenGL.glBegin(OpenGL.GL_QUADS);
            OpenGL.glVertex2f(rectangle.X, rectangle.Y);
            OpenGL.glVertex2f(rectangle.X + rectangle.Width,
                rectangle.Y);
            OpenGL.glVertex2f(rectangle.X + rectangle.Width,
                rectangle.Y + rectangle.Height);
            OpenGL.glVertex2f(rectangle.X,
                rectangle.Y + rectangle.Height);
            OpenGL.glEnd();
        }

        /// <summary>
        /// Fills a Ellipse.
        /// </summary>
        /// <param name="color">The Color.</param>
        /// <param name="ellipse">The Ellipse.</param>
        public void FillEllipse(Color color, Ellipse ellipse)
        {
            OpenGL.glLineWidth(1f);
            OpenGL.glColor4f(color.R, color.G, color.B,
                color.A/255f);
            OpenGL.glBegin(OpenGL.GL_POLYGON);

            Vector2[] polygonVectors = ellipse.Points;

            foreach (Vector2 t in polygonVectors)
            {
                OpenGL.glVertex2f(t.X, t.Y);
            }

            OpenGL.glEnd();
        }

        /// <summary>
        /// Fills a Polygon.
        /// </summary>
        /// <param name="color">The Color.</param>
        /// <param name="polygon">The Polygon.</param>
        public void FillPolygon(Color color, Polygon polygon)
        {
            OpenGL.glLineWidth(1f);
            OpenGL.glColor4f(color.R, color.G, color.B,
                color.A/255f);
            OpenGL.glBegin(OpenGL.GL_POLYGON);

            Vector2[] polygonVectors = polygon.Points;

            foreach (Vector2 t in polygonVectors)
            {
                OpenGL.glVertex2f(t.X, t.Y);
            }

            OpenGL.glEnd();
        }
    }
}