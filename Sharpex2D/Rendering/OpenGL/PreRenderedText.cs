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
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;
using Sharpex2D.Rendering.GDI;

namespace Sharpex2D.Rendering.OpenGL
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Untested)]
    internal class PreRenderedText : IDisposable
    {
        private readonly Color _color;
        private readonly System.Drawing.Font _font;
        private readonly string _text;

        /// <summary>
        /// Initializes a new PreRenderedText class.
        /// </summary>
        /// <param name="font">The Font.</param>
        /// <param name="color">The Color.</param>
        /// <param name="text">The Text.</param>
        public PreRenderedText(System.Drawing.Font font, Color color, string text)
        {
            Identifer = font.GetHashCode() + color.GetHashCode() + text.GetHashCode();
            _font = font;
            _color = color;
            _text = text;
        }

        /// <summary>
        /// Gets the identifer.
        /// </summary>
        public int Identifer { private set; get; }

        /// <summary>
        /// Gets the OpenGLTexture.
        /// </summary>
        public OpenGLTexture OpenGLTexture { private set; get; }

        /// <summary>
        /// Disposes the object.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Renders the font.
        /// </summary>
        public void RenderFont()
        {
            System.Drawing.Color fontColor = GDIHelper.ConvertColor(_color);
            Size result = TextRenderer.MeasureText(_text, _font);
            var bitmapFont = new Bitmap(result.Width, result.Height);
            Graphics graphics = Graphics.FromImage(bitmapFont);
            graphics.Clear(System.Drawing.Color.Transparent);
            graphics.CompositingMode = CompositingMode.SourceOver;
            graphics.CompositingQuality = CompositingQuality.HighQuality;
            graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
            graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
            graphics.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
            graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            graphics.DrawString(_text, _font, new SolidBrush(fontColor), new PointF(0, 0));
            graphics.Flush();
            graphics.Dispose();

            OpenGLTexture = new OpenGLTexture(bitmapFont);
            OpenGLTexture.BindIfUnbinded();
        }

        /// <summary>
        /// Disposes the object.
        /// </summary>
        /// <param name="disposing">The disposing state.</param>
        public virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                OpenGL.glDeleteTextures(1, new[] {(uint) OpenGLTexture.TextureId});
            }

            OpenGLTexture.RawBitmap.Dispose();
        }
    }
}