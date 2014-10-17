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
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Sharpex2D.Common.Extensions;
using Sharpex2D.Content.Pipeline;
using Sharpex2D.Content.Pipeline.Processor;
using Sharpex2D.Math;
using Matrix = System.Drawing.Drawing2D.Matrix;
using Rectangle = Sharpex2D.Math.Rectangle;

namespace Sharpex2D.Rendering.GDI
{
#if Windows
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Tested)]
    public class GDIGraphics : IGraphics
    {
        private readonly GraphicsDevice _graphicsDevice;
        private Bitmap _buffer;
        private Graphics _buffergraphics;

        /// <summary>
        /// Initializes a new GDIGraphics class.
        /// </summary>
        public GDIGraphics()
        {
            ResourceManager = new GDIResourceManager();
            ContentProcessors = new IContentProcessor[]
            {new GDIFontContentProcessor(), new GDIPenContentProcessor(), new GDITextureContentProcessor()};
            SmoothingMode = SmoothingMode.AntiAlias;
            InterpolationMode = InterpolationMode.Linear;

            _graphicsDevice = SGL.QueryComponents<GraphicsDevice>();
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
            _buffer = new Bitmap(_graphicsDevice.BackBuffer.Width, _graphicsDevice.BackBuffer.Height);
            _buffergraphics = Graphics.FromImage(_buffer);

            _buffergraphics.InterpolationMode = InterpolationMode == InterpolationMode.Linear
                ? System.Drawing.Drawing2D.InterpolationMode.Bilinear
                : System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;

            _buffergraphics.SmoothingMode = SmoothingMode == SmoothingMode.AntiAlias
                ? System.Drawing.Drawing2D.SmoothingMode.AntiAlias
                : System.Drawing.Drawing2D.SmoothingMode.None;

            _buffergraphics.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
            _buffergraphics.CompositingMode = CompositingMode.SourceOver;
            _buffergraphics.CompositingQuality = CompositingQuality.HighQuality;
            _buffergraphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
            _buffergraphics.PageUnit = GraphicsUnit.Pixel;

            _buffergraphics.Clear(GDIHelper.ConvertColor(_graphicsDevice.ClearColor));
        }

        /// <summary>
        /// Begins the draw operation.
        /// </summary>
        public void Begin()
        {
            _buffergraphics.Clear(GDIHelper.ConvertColor(_graphicsDevice.ClearColor));
        }

        /// <summary>
        /// Ends the draw operation.
        /// </summary>
        public void End()
        {
            Present();
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
            var gdifont = font.Instance as GDIFont;
            if (gdifont == null)
            {
                throw new GraphicsException("GdiRenderer needs a GdiFont resource.");
            }
            _buffergraphics.DrawString(text, gdifont.GetFont(), new SolidBrush(GDIHelper.ConvertColor(color)),
                new RectangleF(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height));
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
            var gdifont = font.Instance as GDIFont;
            if (gdifont == null)
            {
                throw new GraphicsException("GdiRenderer needs a GdiFont resource.");
            }

            _buffergraphics.DrawString(text, gdifont.GetFont(), new SolidBrush(GDIHelper.ConvertColor(color)),
                GDIHelper.ConvertPointF(position));
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
            var gdiTexture = texture as GDITexture;
            if (gdiTexture == null) throw new ArgumentException("GdiRenderer expects a GdiTexture resource.");

            var matrix = new ColorMatrix {Matrix33 = opacity};
            var attributes = new ImageAttributes();

            attributes.SetColorMatrix(matrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

            if (color != Color.White)
            {
                var tempBmp = new Bitmap(gdiTexture.Width, gdiTexture.Height);
                Graphics g = Graphics.FromImage(tempBmp);
                g.DrawImage(ColorTint(gdiTexture.Bmp, color.B, color.G, color.R), 0, 0);

                _buffergraphics.DrawImage(tempBmp,
                    new System.Drawing.Rectangle((int) position.X, (int) position.Y, tempBmp.Width, tempBmp.Height), 0,
                    0,
                    tempBmp.Width, tempBmp.Height, GraphicsUnit.Pixel, attributes);
                tempBmp.Dispose();
            }
            else
            {
                _buffergraphics.DrawImage(gdiTexture.Bmp,
                    new System.Drawing.Rectangle((int) position.X, (int) position.Y, gdiTexture.Bmp.Width,
                        gdiTexture.Bmp.Height), 0, 0,
                    gdiTexture.Bmp.Width, gdiTexture.Bmp.Height, GraphicsUnit.Pixel, attributes);
            }
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
            var gdiTexture = texture as GDITexture;
            if (gdiTexture == null) throw new ArgumentException("GdiRenderer expects a GdiTexture resource.");

            var matrix = new ColorMatrix {Matrix33 = opacity};
            var attributes = new ImageAttributes();

            attributes.SetColorMatrix(matrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

            if (color != Color.White)
            {
                var tempBmp = new Bitmap(gdiTexture.Width, gdiTexture.Height);
                Graphics g = Graphics.FromImage(tempBmp);
                g.DrawImage(ColorTint(gdiTexture.Bmp, color.B, color.G, color.R), 0, 0);
                g.Dispose();
                _buffergraphics.DrawImage(tempBmp, GDIHelper.ConvertRectangle(rectangle), 0, 0,
                    tempBmp.Width, tempBmp.Height, GraphicsUnit.Pixel, attributes);
                tempBmp.Dispose();
            }
            else
            {
                _buffergraphics.DrawImage(gdiTexture.Bmp, GDIHelper.ConvertRectangle(rectangle), 0, 0,
                    gdiTexture.Bmp.Width, gdiTexture.Bmp.Height, GraphicsUnit.Pixel, attributes);
            }
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
            var gdiTexture = spriteSheet.Texture2D as GDITexture;
            if (gdiTexture == null) throw new ArgumentException("GdiRenderer expects a GdiTexture resource.");

            var matrix = new ColorMatrix {Matrix33 = opacity};
            var attributes = new ImageAttributes();

            attributes.SetColorMatrix(matrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

            if (color != Color.White)
            {
                var tempBmp = new Bitmap(gdiTexture.Width, gdiTexture.Height);
                Graphics g = Graphics.FromImage(tempBmp);
                g.DrawImage(ColorTint(gdiTexture.Bmp, color.B, color.G, color.R), 0, 0);
                g.Dispose();
                _buffergraphics.DrawImage(tempBmp,
                    new System.Drawing.Rectangle((int) position.X, (int) position.Y, (int) spriteSheet.Rectangle.Width,
                        (int) spriteSheet.Rectangle.Height),
                    spriteSheet.Rectangle.X, spriteSheet.Rectangle.Y, spriteSheet.Rectangle.Width,
                    spriteSheet.Rectangle.Height, GraphicsUnit.Pixel, attributes);
            }
            else
            {
                _buffergraphics.DrawImage(gdiTexture.Bmp,
                    new System.Drawing.Rectangle((int) position.X, (int) position.Y, (int) spriteSheet.Rectangle.Width,
                        (int) spriteSheet.Rectangle.Height),
                    spriteSheet.Rectangle.X, spriteSheet.Rectangle.Y, spriteSheet.Rectangle.Width,
                    spriteSheet.Rectangle.Height, GraphicsUnit.Pixel, attributes);
            }
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
            var gdiTexture = spriteSheet.Texture2D as GDITexture;
            if (gdiTexture == null) throw new ArgumentException("GdiRenderer expects a GdiTexture resource.");

            var matrix = new ColorMatrix {Matrix33 = opacity};
            var attributes = new ImageAttributes();

            attributes.SetColorMatrix(matrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

            if (color != Color.White)
            {
                var tempBmp = new Bitmap(gdiTexture.Width, gdiTexture.Height);
                Graphics g = Graphics.FromImage(tempBmp);
                g.DrawImage(ColorTint(gdiTexture.Bmp, color.B, color.G, color.R), 0, 0);
                g.Dispose();
                _buffergraphics.DrawImage(tempBmp,
                    new System.Drawing.Rectangle((int) rectangle.X, (int) rectangle.Y, (int) rectangle.Width,
                        (int) rectangle.Height),
                    spriteSheet.Rectangle.X, spriteSheet.Rectangle.Y, spriteSheet.Rectangle.Width,
                    spriteSheet.Rectangle.Height, GraphicsUnit.Pixel, attributes);
            }
            else
            {
                _buffergraphics.DrawImage(gdiTexture.Bmp,
                    new System.Drawing.Rectangle((int) rectangle.X, (int) rectangle.Y, (int) rectangle.Width,
                        (int) rectangle.Height),
                    spriteSheet.Rectangle.X, spriteSheet.Rectangle.Y, spriteSheet.Rectangle.Width,
                    spriteSheet.Rectangle.Height, GraphicsUnit.Pixel, attributes);
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
            float opacity = 1)
        {
            var gdiTexture = texture as GDITexture;
            if (gdiTexture == null) throw new ArgumentException("GdiRenderer expects a GdiTexture resource.");
            var matrix = new ColorMatrix {Matrix33 = opacity};
            var attributes = new ImageAttributes();
            attributes.SetColorMatrix(matrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
            if (color != Color.White)
            {
                var tempBmp = new Bitmap(gdiTexture.Width, gdiTexture.Height);
                Graphics g = Graphics.FromImage(tempBmp);
                g.DrawImage(ColorTint(gdiTexture.Bmp, color.B, color.G, color.R), 0, 0);
                g.Dispose();
                _buffergraphics.DrawImage(tempBmp,
                    new System.Drawing.Rectangle((int) destination.X, (int) destination.Y, (int) destination.Width,
                        (int) destination.Height), (int) source.X, (int) source.Y,
                    (int) source.Width, (int) source.Height, GraphicsUnit.Pixel, attributes);
            }
            else
            {
                _buffergraphics.DrawImage(gdiTexture.Bmp,
                    new System.Drawing.Rectangle((int) destination.X, (int) destination.Y, (int) destination.Width,
                        (int) destination.Height), (int) source.X, (int) source.Y,
                    (int) source.Width, (int) source.Height, GraphicsUnit.Pixel, attributes);
            }
        }

        /// <summary>
        /// Measures the string.
        /// </summary>
        /// <param name="text">The String.</param>
        /// <param name="font">The Font.</param>
        /// <returns>Vector2.</returns>
        public Vector2 MeasureString(string text, Font font)
        {
            var gdifont = font.Instance as GDIFont;
            if (gdifont == null)
            {
                throw new InvalidOperationException("GdiRenderer needs a GdiFont resource.");
            }

            SizeF result = _buffergraphics.MeasureString(text, gdifont.GetFont());

            return new Vector2(result.Width, result.Height);
        }

        /// <summary>
        /// Sets the Transform.
        /// </summary>
        /// <param name="matrix">The Matrix.</param>
        public void SetTransform(Matrix2x3 matrix)
        {
            _buffergraphics.Transform = new Matrix(matrix[0, 0], matrix[1, 0], matrix[0, 1],
                matrix[1, 1], matrix.OffsetX, matrix.OffsetY);
        }

        /// <summary>
        /// Resets the Transform.
        /// </summary>
        public void ResetTransform()
        {
            _buffergraphics.ResetTransform();
        }

        /// <summary>
        /// Draws a Rectangle.
        /// </summary>
        /// <param name="pen">The Pen.</param>
        /// <param name="rectangle">The Rectangle.</param>
        public void DrawRectangle(Pen pen, Rectangle rectangle)
        {
            var gdiPen = pen.Instance as GDIPen;
            if (gdiPen == null) throw new ArgumentException("GdiRenderer expects a GdiPen as resource.");

            _buffergraphics.DrawRectangle(gdiPen.GetPen(),
                new System.Drawing.Rectangle((int) rectangle.X, (int) rectangle.Y, (int) rectangle.Width,
                    (int) rectangle.Height));
        }

        /// <summary>
        /// Draws a Line between two points.
        /// </summary>
        /// <param name="pen">The Pen.</param>
        /// <param name="start">The Startpoint.</param>
        /// <param name="target">The Targetpoint.</param>
        public void DrawLine(Pen pen, Vector2 start, Vector2 target)
        {
            var gdiPen = pen.Instance as GDIPen;
            if (gdiPen == null) throw new ArgumentException("GdiRenderer expects a GdiPen as resource.");

            _buffergraphics.DrawLine(gdiPen.GetPen(), start.X, start.Y, target.X, target.Y);
        }

        /// <summary>
        /// Draws a Ellipse.
        /// </summary>
        /// <param name="pen">The Pen.</param>
        /// <param name="ellipse">The Ellipse.</param>
        public void DrawEllipse(Pen pen, Ellipse ellipse)
        {
            var gdiPen = pen.Instance as GDIPen;
            if (gdiPen == null) throw new ArgumentException("GdiRenderer expects a GdiPen as resource.");

            _buffergraphics.DrawEllipse(gdiPen.GetPen(),
                new System.Drawing.Rectangle((int) ellipse.Position.X, (int) ellipse.Position.Y, (int) ellipse.RadiusX*2,
                    (int) ellipse.RadiusY*2));
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
            var gdiPen = pen.Instance as GDIPen;
            if (gdiPen == null) throw new ArgumentException("GdiRenderer expects a GdiPen as resource.");

            _buffergraphics.DrawArc(gdiPen.GetPen(),
                new System.Drawing.Rectangle((int) rectangle.X, (int) rectangle.Y, (int) rectangle.Width,
                    (int) rectangle.Height), startAngle, sweepAngle);
        }

        /// <summary>
        /// Draws a Polygon.
        /// </summary>
        /// <param name="pen">The Pen.</param>
        /// <param name="polygon">The Polygon.</param>
        public void DrawPolygon(Pen pen, Polygon polygon)
        {
            var gdiPen = pen.Instance as GDIPen;
            if (gdiPen == null) throw new ArgumentException("GdiRenderer expects a GdiPen as resource.");

            _buffergraphics.DrawPolygon(gdiPen.GetPen(), polygon.Points.ToPoints());
        }

        /// <summary>
        /// Fills a Rectangle.
        /// </summary>
        /// <param name="color">The Color.</param>
        /// <param name="rectangle">The Rectangle.</param>
        public void FillRectangle(Color color, Rectangle rectangle)
        {
            _buffergraphics.FillRectangle(new SolidBrush(GDIHelper.ConvertColor(color)),
                new System.Drawing.Rectangle((int) rectangle.X, (int) rectangle.Y, (int) rectangle.Width,
                    (int) rectangle.Height));
        }

        /// <summary>
        /// Fills a Ellipse.
        /// </summary>
        /// <param name="color">The Color.</param>
        /// <param name="ellipse">The Ellipse.</param>
        public void FillEllipse(Color color, Ellipse ellipse)
        {
            _buffergraphics.FillEllipse(new SolidBrush(GDIHelper.ConvertColor(color)),
                new System.Drawing.Rectangle((int) ellipse.Position.X, (int) ellipse.Position.Y, (int) ellipse.RadiusX*2,
                    (int) ellipse.RadiusY*2));
        }

        /// <summary>
        /// Fills a Polygon.
        /// </summary>
        /// <param name="color">The Color.</param>
        /// <param name="polygon">The Polygon.</param>
        public void FillPolygon(Color color, Polygon polygon)
        {
            _buffergraphics.FillPolygon(new SolidBrush(GDIHelper.ConvertColor(color)), polygon.Points.ToPoints());
        }

        /// <summary>
        /// Presents the Frame.
        /// </summary>
        private void Present()
        {
            Control control = Control.FromHandle(_graphicsDevice.RenderTarget.Handle);
            if (control != null)
            {
                int width = control.Width;
                int height = control.Height;
                if (!_graphicsDevice.BackBuffer.Scaling)
                {
                    width = _graphicsDevice.BackBuffer.Width;
                    height = _graphicsDevice.BackBuffer.Height;
                }
                Graphics graphics = control.CreateGraphics();
                IntPtr hdc = graphics.GetHdc();
                IntPtr intPtr = NativeMethods.CreateCompatibleDC(hdc);
                IntPtr hbitmap = _buffer.GetHbitmap();
                NativeMethods.SelectObject(intPtr, hbitmap);
                NativeMethods.StretchBlt(hdc, 0, 0, width, height, intPtr, 0, 0, _graphicsDevice.BackBuffer.Width,
                    _graphicsDevice.BackBuffer.Height, NativeMethods.GdiRasterOperations.SRCCOPY);
                NativeMethods.DeleteObject(hbitmap);
                NativeMethods.DeleteDC(intPtr);
                graphics.ReleaseHdc(hdc);
                graphics.Dispose();
            }
        }

        /// <summary>
        /// Tints the Bitmap.
        /// </summary>
        /// <param name="sourceBitmap">The SourceBitmap.</param>
        /// <param name="blueTint">The BlueTint.</param>
        /// <param name="greenTint">The GreenTint.</param>
        /// <param name="redTint">The RedTint.</param>
        /// <returns>The Bitmap.</returns>
        private Bitmap ColorTint(Bitmap sourceBitmap, float blueTint, float greenTint, float redTint)
        {
            BitmapData sourceData = sourceBitmap.LockBits(new System.Drawing.Rectangle(0, 0,
                sourceBitmap.Width, sourceBitmap.Height),
                ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);


            var pixelBuffer = new byte[sourceData.Stride*sourceData.Height];


            Marshal.Copy(sourceData.Scan0, pixelBuffer, 0, pixelBuffer.Length);

            sourceBitmap.UnlockBits(sourceData);

            for (int k = 0; k + 4 < pixelBuffer.Length; k += 4)
            {
                float blue = pixelBuffer[k] + (255 - pixelBuffer[k])*blueTint;
                float green = pixelBuffer[k + 1] + (255 - pixelBuffer[k + 1])*greenTint;
                float red = pixelBuffer[k + 2] + (255 - pixelBuffer[k + 2])*redTint;


                if (blue > 255)
                {
                    blue = 255;
                }


                if (green > 255)
                {
                    green = 255;
                }


                if (red > 255)
                {
                    red = 255;
                }


                pixelBuffer[k] = (byte) blue;
                pixelBuffer[k + 1] = (byte) green;
                pixelBuffer[k + 2] = (byte) red;
            }


            var resultBitmap = new Bitmap(sourceBitmap.Width, sourceBitmap.Height);


            BitmapData resultData = resultBitmap.LockBits(new System.Drawing.Rectangle(0, 0,
                resultBitmap.Width, resultBitmap.Height),
                ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);


            Marshal.Copy(pixelBuffer, 0, resultData.Scan0, pixelBuffer.Length);
            resultBitmap.UnlockBits(resultData);


            return resultBitmap;
        }
    }
#endif
}