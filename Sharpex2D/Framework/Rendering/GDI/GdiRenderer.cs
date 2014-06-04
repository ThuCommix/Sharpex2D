using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Sharpex2D.Framework.Common.Extensions;
using Sharpex2D.Framework.Content;
using Sharpex2D.Framework.Content.Pipeline.Processor;
using Sharpex2D.Framework.Math;
using Sharpex2D.Framework.Rendering.Font;
using Matrix = System.Drawing.Drawing2D.Matrix;
using Rectangle = Sharpex2D.Framework.Math.Rectangle;

namespace Sharpex2D.Framework.Rendering.GDI
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Tested)]
    public class GdiRenderer : IRenderer
    {
        #region IComponent Implementation

        /// <summary>
        ///     Sets or gets the Guid of the Component.
        /// </summary>
        public Guid Guid
        {
            get { return new Guid("A18634EE-B48C-4705-866F-ADFA5F4630ED"); }
        }

        #endregion

        #region IRendererImplementation

        /// <summary>
        ///     Sets or gets the GDIQuality.
        /// </summary>
        public GdiQuality Quality
        {
            get { return _quality; }
            set { ChangedQuality(value); }
        }

        /// <summary>
        ///     Determines, if the buffer is open for draw operations.
        /// </summary>
        private bool IsBegin { get; set; }

        /// <summary>
        ///     Current GraphicsDevice.
        /// </summary>
        public GraphicsDevice GraphicsDevice { get; set; }

        /// <summary>
        ///     A value indicating whether VSync is enabled.
        /// </summary>
        public bool VSync { set; get; }

        /// <summary>
        ///     A value indicating whether the renderer is disposed.
        /// </summary>
        public bool IsDisposed { get; private set; }

        /// <summary>
        ///     Opens the buffer for draw operations.
        /// </summary>
        public void Begin()
        {
            if (!IsBegin)
            {
                IsBegin = true;
                _buffergraphics.Clear(GraphicsDevice.ClearColor.ToWin32Color());
            }
        }

        /// <summary>
        ///     Flushes the buffer.
        /// </summary>
        public void Close()
        {
            if (IsBegin)
            {
                IsBegin = false;
                Renderer();
            }
        }

        /// <summary>
        ///     Disposes the renderer.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        ///     Draws a Rectangle.
        /// </summary>
        /// <param name="pen">The Pen.</param>
        /// <param name="rectangle">The Rectangle.</param>
        public void DrawRectangle(IPen pen, Rectangle rectangle)
        {
            CheckDisposed();

            var gdiPen = pen as GdiPen;
            if (gdiPen == null) throw new ArgumentException("GdiRenderer expects a GdiPen as resource.");

            _buffergraphics.DrawRectangle(gdiPen.GetPen(),
                new System.Drawing.Rectangle((int) rectangle.X, (int) rectangle.Y, (int) rectangle.Width,
                    (int) rectangle.Height));
        }

        /// <summary>
        ///     Draws a Line between two points.
        /// </summary>
        /// <param name="pen">The Pen.</param>
        /// <param name="start">The Startpoint.</param>
        /// <param name="target">The Targetpoint.</param>
        public void DrawLine(IPen pen, Vector2 start, Vector2 target)
        {
            CheckDisposed();

            var gdiPen = pen as GdiPen;
            if (gdiPen == null) throw new ArgumentException("GdiRenderer expects a GdiPen as resource.");

            _buffergraphics.DrawLine(gdiPen.GetPen(), start.X, start.Y, target.X, target.Y);
        }

        /// <summary>
        ///     Draws a Ellipse.
        /// </summary>
        /// <param name="pen">The Pen.</param>
        /// <param name="rectangle">The Rectangle.</param>
        public void DrawEllipse(IPen pen, Rectangle rectangle)
        {
            CheckDisposed();

            var gdiPen = pen as GdiPen;
            if (gdiPen == null) throw new ArgumentException("GdiRenderer expects a GdiPen as resource.");

            _buffergraphics.DrawEllipse(gdiPen.GetPen(),
                new System.Drawing.Rectangle((int) rectangle.X, (int) rectangle.Y, (int) rectangle.Width,
                    (int) rectangle.Height));
        }

        /// <summary>
        ///     Draws an Arc.
        /// </summary>
        /// <param name="pen">The Pen.</param>
        /// <param name="rectangle">The Rectangle.</param>
        /// <param name="startAngle">The StartAngle.</param>
        /// <param name="sweepAngle">The SweepAngle.</param>
        public void DrawArc(IPen pen, Rectangle rectangle, float startAngle, float sweepAngle)
        {
            CheckDisposed();

            var gdiPen = pen as GdiPen;
            if (gdiPen == null) throw new ArgumentException("GdiRenderer expects a GdiPen as resource.");

            _buffergraphics.DrawArc(gdiPen.GetPen(),
                new System.Drawing.Rectangle((int) rectangle.X, (int) rectangle.Y, (int) rectangle.Width,
                    (int) rectangle.Height), startAngle, sweepAngle);
        }

        /// <summary>
        ///     Draws a Polygon.
        /// </summary>
        /// <param name="pen">The Pen.</param>
        /// <param name="points">The Points.</param>
        public void DrawPolygon(IPen pen, Vector2[] points)
        {
            CheckDisposed();

            var gdiPen = pen as GdiPen;
            if (gdiPen == null) throw new ArgumentException("GdiRenderer expects a GdiPen as resource.");

            _buffergraphics.DrawPolygon(gdiPen.GetPen(), points.ToPoints());
        }

        /// <summary>
        ///     Draws a corner-rounded Rectangle.
        /// </summary>
        /// <param name="pen">The Pen.</param>
        /// <param name="rectangle">The Rectangle.</param>
        /// <param name="radius">The Radius.</param>
        public void DrawRoundedRectangle(IPen pen, Rectangle rectangle, int radius)
        {
            CheckDisposed();

            var gdiPen = pen as GdiPen;
            if (gdiPen == null) throw new ArgumentException("GdiRenderer expects a GdiPen as resource.");

            var gfxPath = new GraphicsPath();
            gfxPath.AddArc(rectangle.X, rectangle.Y, radius, radius, 180, 90);
            gfxPath.AddArc(rectangle.X + rectangle.Width - radius, rectangle.Y, radius, radius, 270, 90);
            gfxPath.AddArc(rectangle.X + rectangle.Width - radius, rectangle.Y + rectangle.Height - radius, radius,
                radius, 0, 90);
            gfxPath.AddArc(rectangle.X, rectangle.Y + rectangle.Height - radius, radius, radius, 90, 90);
            gfxPath.CloseAllFigures();
            _buffergraphics.DrawPath(gdiPen.GetPen(), gfxPath);
        }

        /// <summary>
        ///     Fills a Rectangle.
        /// </summary>
        /// <param name="color">The Color.</param>
        /// <param name="rectangle">The Rectangle.</param>
        public void FillRectangle(Color color, Rectangle rectangle)
        {
            CheckDisposed();

            _buffergraphics.FillRectangle(new SolidBrush(color.ToWin32Color()),
                new System.Drawing.Rectangle((int) rectangle.X, (int) rectangle.Y, (int) rectangle.Width,
                    (int) rectangle.Height));
        }

        /// <summary>
        ///     Fills a Ellipse.
        /// </summary>
        /// <param name="color">The Color.</param>
        /// <param name="rectangle">The Rectangle.</param>
        public void FillEllipse(Color color, Rectangle rectangle)
        {
            CheckDisposed();

            _buffergraphics.FillEllipse(new SolidBrush(color.ToWin32Color()),
                new System.Drawing.Rectangle((int) rectangle.X, (int) rectangle.Y, (int) rectangle.Width,
                    (int) rectangle.Height));
        }

        /// <summary>
        ///     Fills a Polygon.
        /// </summary>
        /// <param name="color">The Color.</param>
        /// <param name="points">The Points.</param>
        public void FillPolygon(Color color, Vector2[] points)
        {
            CheckDisposed();

            _buffergraphics.FillPolygon(new SolidBrush(color.ToWin32Color()), points.ToPoints());
        }

        /// <summary>
        ///     Draws a corner-rounded Rectangle.
        /// </summary>
        /// <param name="color">The Color.</param>
        /// <param name="rectangle">The Rectangle.</param>
        /// <param name="radius">The Radius.</param>
        public void FillRoundedRectangle(Color color, Rectangle rectangle, int radius)
        {
            var gfxPath = new GraphicsPath();
            gfxPath.AddArc(rectangle.X, rectangle.Y, radius, radius, 180, 90);
            gfxPath.AddArc(rectangle.X + rectangle.Width - radius, rectangle.Y, radius, radius, 270, 90);
            gfxPath.AddArc(rectangle.X + rectangle.Width - radius, rectangle.Y + rectangle.Height - radius, radius,
                radius, 0, 90);
            gfxPath.AddArc(rectangle.X, rectangle.Y + rectangle.Height - radius, radius, radius, 90, 90);
            gfxPath.CloseAllFigures();
            _buffergraphics.FillPath(new SolidBrush(color.ToWin32Color()), gfxPath);
        }

        /// <summary>
        ///     Draws a Texture.
        /// </summary>
        /// <param name="texture">The Texture.</param>
        /// <param name="rectangle">The Rectangle.</param>
        /// <param name="opacity">The Opacity.</param>
        /// <param name="color">The Color.</param>
        public void DrawTexture(ITexture texture, Rectangle rectangle, Color color, float opacity = 1f)
        {
            CheckDisposed();

            var gdiTexture = texture as GdiTexture;
            if (gdiTexture == null) throw new ArgumentException("GdiRenderer expects a GdiTexture resource.");

            var matrix = new ColorMatrix {Matrix33 = opacity};
            var attributes = new ImageAttributes();

            attributes.SetColorMatrix(matrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

            if (color != Color.White)
            {
                var tempBmp = new Bitmap(gdiTexture.Width, gdiTexture.Height);
                var g = Graphics.FromImage(tempBmp);
                g.DrawImage(ColorTint(gdiTexture.Bmp, color.B, color.G, color.R), 0, 0);
                g.Dispose();
                _buffergraphics.DrawImage(tempBmp,
                    new System.Drawing.Rectangle((int)rectangle.X, (int)rectangle.Y, (int)rectangle.Width, (int)rectangle.Height), 0, 0,
                    tempBmp.Width, tempBmp.Height, GraphicsUnit.Pixel, attributes);
                tempBmp.Dispose();
            }
            else
            {
                _buffergraphics.DrawImage(gdiTexture.Bmp,
                    new System.Drawing.Rectangle((int)rectangle.X, (int)rectangle.Y, (int)rectangle.Width, (int)rectangle.Height), 0, 0,
                    gdiTexture.Bmp.Width, gdiTexture.Bmp.Height, GraphicsUnit.Pixel, attributes);
            }
        }

        /// <summary>
        ///     Draws a Texture.
        /// </summary>
        /// <param name="texture">The Texture.</param>
        /// <param name="position">The Position.</param>
        /// <param name="opacity">The Opacity.</param>
        /// <param name="color">The Color.</param>
        public void DrawTexture(ITexture texture, Vector2 position, Color color, float opacity = 1f)
        {
            CheckDisposed();

            var gdiTexture = texture as GdiTexture;
            if (gdiTexture == null) throw new ArgumentException("GdiRenderer expects a GdiTexture resource.");

            var matrix = new ColorMatrix { Matrix33 = opacity };
            var attributes = new ImageAttributes();

            attributes.SetColorMatrix(matrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

            if (color != Color.White)
            {
                var tempBmp = new Bitmap(gdiTexture.Width, gdiTexture.Height);
                var g = Graphics.FromImage(tempBmp);
                g.DrawImage(ColorTint(gdiTexture.Bmp, color.B, color.G, color.R), 0, 0);

                _buffergraphics.DrawImage(tempBmp,
                    new System.Drawing.Rectangle((int)position.X, (int)position.Y, tempBmp.Width, tempBmp.Height), 0, 0,
                    tempBmp.Width, tempBmp.Height, GraphicsUnit.Pixel, attributes);
                tempBmp.Dispose();
            }
            else
            {
                _buffergraphics.DrawImage(gdiTexture.Bmp,
                    new System.Drawing.Rectangle((int)position.X, (int)position.Y, gdiTexture.Bmp.Width, gdiTexture.Bmp.Height), 0, 0,
                    gdiTexture.Bmp.Width, gdiTexture.Bmp.Height, GraphicsUnit.Pixel, attributes);
            }
        }

        /// <summary>
        ///     Draws a string.
        /// </summary>
        /// <param name="text">The Text.</param>
        /// <param name="font">The Font.</param>
        /// <param name="rectangle">The Rectangle.</param>
        /// <param name="color">The Color.</param>
        public void DrawString(string text, IFont font, Rectangle rectangle, Color color)
        {
            CheckDisposed();

            var gdifont = font as GdiFont;
            if (gdifont == null)
            {
                throw new InvalidOperationException("GdiRenderer needs a GdiFont resource.");
            }
            _buffergraphics.DrawString(text, gdifont.GetFont(), new SolidBrush(color.ToWin32Color()),
                new RectangleF(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height));
        }

        /// <summary>
        ///     Draws a string.
        /// </summary>
        /// <param name="text">The Text.</param>
        /// <param name="font">The Font.</param>
        /// <param name="position">The Position.</param>
        /// <param name="color">The Color.</param>
        public void DrawString(string text, IFont font, Vector2 position, Color color)
        {
            CheckDisposed();

            var gdifont = font as GdiFont;
            if (gdifont == null)
            {
                throw new InvalidOperationException("GdiRenderer needs a GdiFont resource.");
            }

            _buffergraphics.DrawString(text, gdifont.GetFont(), new SolidBrush(color.ToWin32Color()),
                position.ToPointF());
        }

        /// <summary>
        ///     Measures the string.
        /// </summary>
        /// <param name="text">The Text.</param>
        /// <param name="font">The Font.</param>
        /// <returns>Vector2.</returns>
        public Vector2 MeasureString(string text, IFont font)
        {
            CheckDisposed();

            var gdifont = font as GdiFont;
            if (gdifont == null)
            {
                throw new InvalidOperationException("GdiRenderer needs a GdiFont resource.");
            }

            SizeF result = _buffergraphics.MeasureString(text, gdifont.GetFont());

            return new Vector2(result.Width, result.Height);
        }

        /// <summary>
        ///     Sets the Transform.
        /// </summary>
        /// <param name="matrix">The Matrix.</param>
        public void SetTransform(Matrix2x3 matrix)
        {
            CheckDisposed();

            _buffergraphics.Transform = new Matrix(matrix[0, 0], matrix[1, 0], matrix[0, 1],
                matrix[1, 1], matrix.OffsetX, matrix.OffsetY);
        }

        /// <summary>
        ///     Resets the Transform.
        /// </summary>
        public void ResetTransform()
        {
            CheckDisposed();

            _buffergraphics.ResetTransform();
        }

        /// <summary>
        ///     Constructs the Component.
        /// </summary>
        public void Construct()
        {
            _buffer = new Bitmap(GraphicsDevice.DisplayMode.Width, GraphicsDevice.DisplayMode.Height);
            _buffergraphics = Graphics.FromImage(_buffer);
            if (_quality == null) _quality = new GdiQuality(GdiQualityMode.High);
            _buffergraphics.SmoothingMode = _quality.AntiAlias ? SmoothingMode.HighQuality : SmoothingMode.HighSpeed;
            _buffergraphics.InterpolationMode = _quality.Interpolation
                ? InterpolationMode.High
                : InterpolationMode.NearestNeighbor;
            _buffergraphics.CompositingQuality = _quality.Compositing
                ? CompositingQuality.HighQuality
                : CompositingQuality.AssumeLinear;
            _buffergraphics.PixelOffsetMode = _quality.HighQualityPixelOffset
                ? PixelOffsetMode.HighQuality
                : PixelOffsetMode.HighSpeed;
            GraphicsDevice.ClearColor = Color.CornflowerBlue;
            _buffergraphics.Clear(GraphicsDevice.ClearColor.ToWin32Color());
            _buffergraphics.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;

            _buffergraphics.PageUnit = GraphicsUnit.Pixel;
        }

        /// <summary>
        ///     Disposes the object.
        /// </summary>
        /// <param name="disposing">Indicates whether managed resources should be disposed.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!IsDisposed)
            {
                IsDisposed = true;
                if (disposing)
                {
                    _buffer.Dispose();
                }
            }
        }

        #endregion

        #region GDIRenderer

        private Bitmap _buffer;
        private Graphics _buffergraphics;
        private GdiQuality _quality;

        /// <summary>
        ///     Initializes a new GdiRenderer.
        /// </summary>
        public GdiRenderer()
        {
            _quality = new GdiQuality(GdiQualityMode.High);

            var contentManager = SGL.Components.Get<ContentManager>();

            contentManager.ContentProcessor.Add(new GdiTextureContentProcessor());
            contentManager.ContentProcessor.Add(new GdiFontContentProcessor());
            contentManager.ContentProcessor.Add(new GdiPenContentProcessor());
        }

        /// <summary>
        ///     Starts the rendering pipe.
        /// </summary>
        private void Renderer()
        {
            Present();
        }

        /// <summary>
        ///     Releases the frame.
        /// </summary>
        private void Present()
        {
            Control control = Control.FromHandle(GraphicsDevice.RenderTarget.Handle);
            if (control != null)
            {
                int width = control.Width;
                int height = control.Height;
                if (!GraphicsDevice.DisplayMode.Scaling)
                {
                    width = GraphicsDevice.DisplayMode.Width;
                    height = GraphicsDevice.DisplayMode.Height;
                }
                var graphics = control.CreateGraphics();
                var hdc = graphics.GetHdc();
                var intPtr = NativeMethods.CreateCompatibleDC(hdc);
                var hbitmap = _buffer.GetHbitmap();
                NativeMethods.SelectObject(intPtr, hbitmap);
                NativeMethods.StretchBlt(hdc, 0, 0, width, height, intPtr, 0, 0, GraphicsDevice.DisplayMode.Width,
                    GraphicsDevice.DisplayMode.Height, NativeMethods.GdiRasterOperations.SRCCOPY);
                NativeMethods.DeleteObject(hbitmap);
                NativeMethods.DeleteDC(intPtr);
                graphics.ReleaseHdc(hdc);
                graphics.Dispose();
            }
        }

        /// <summary>
        ///     Changes the current Quality.
        /// </summary>
        /// <param name="quality">The GDIQuality.</param>
        private void ChangedQuality(GdiQuality quality)
        {
            _quality = quality;
            if (_buffergraphics != null)
            {
                _buffergraphics.SmoothingMode = _quality.AntiAlias ? SmoothingMode.HighQuality : SmoothingMode.HighSpeed;
                _buffergraphics.InterpolationMode = _quality.Interpolation
                    ? InterpolationMode.HighQualityBicubic
                    : InterpolationMode.Low;
                _buffergraphics.CompositingQuality = _quality.Compositing
                    ? CompositingQuality.HighQuality
                    : CompositingQuality.HighSpeed;
                _buffergraphics.PixelOffsetMode = _quality.HighQualityPixelOffset
                    ? PixelOffsetMode.HighQuality
                    : PixelOffsetMode.HighSpeed;
                _buffergraphics.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
                _buffergraphics.CompositingMode = CompositingMode.SourceOver;
            }
        }

        /// <summary>
        ///     Checks if the GDIRenderer is disposed.
        /// </summary>
        private void CheckDisposed()
        {
            if (IsDisposed)
            {
                throw new ObjectDisposedException("GdiRenderer");
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
            var sourceData = sourceBitmap.LockBits(new System.Drawing.Rectangle(0, 0,
                                    sourceBitmap.Width, sourceBitmap.Height),
                                    ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);


            var pixelBuffer = new byte[sourceData.Stride * sourceData.Height];


            Marshal.Copy(sourceData.Scan0, pixelBuffer, 0, pixelBuffer.Length);

            sourceBitmap.UnlockBits(sourceData);

            for (var k = 0; k + 4 < pixelBuffer.Length; k += 4)
            {
                var blue = pixelBuffer[k] + (255 - pixelBuffer[k]) * blueTint;
                var green = pixelBuffer[k + 1] + (255 - pixelBuffer[k + 1]) * greenTint;
                var red = pixelBuffer[k + 2] + (255 - pixelBuffer[k + 2]) * redTint;


                if (blue > 255)
                { blue = 255; }


                if (green > 255)
                { green = 255; }


                if (red > 255)
                { red = 255; }


                pixelBuffer[k] = (byte)blue;
                pixelBuffer[k + 1] = (byte)green;
                pixelBuffer[k + 2] = (byte)red;


            }


            var resultBitmap = new Bitmap(sourceBitmap.Width, sourceBitmap.Height);


            var resultData = resultBitmap.LockBits(new System.Drawing.Rectangle(0, 0,
                                    resultBitmap.Width, resultBitmap.Height),
                                    ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);


            Marshal.Copy(pixelBuffer, 0, resultData.Scan0, pixelBuffer.Length);
            resultBitmap.UnlockBits(resultData);


            return resultBitmap;
        } 

        #endregion
    }
}