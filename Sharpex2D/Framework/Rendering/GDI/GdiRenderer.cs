using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Sharpex2D.Framework.Common.Extensions;
using Sharpex2D.Framework.Content;
using Sharpex2D.Framework.Math;
using Sharpex2D.Framework.Rendering.Font;
using Rectangle = Sharpex2D.Framework.Math.Rectangle;

namespace Sharpex2D.Framework.Rendering.GDI
{
    public class GdiRenderer : IRenderer
    {
        #region IComponent Implementation

        /// <summary>
        /// Sets or gets the Guid of the Component.
        /// </summary>
        public Guid Guid
        {
            get { return new Guid("A18634EE-B48C-4705-866F-ADFA5F4630ED"); }
        }

        #endregion

        #region IRendererImplementation

        /// <summary>
        /// Sets or gets the GDIQuality.
        /// </summary>
        public GdiQuality Quality{
            get { return _quality; }
            set { ChangedQuality(value); }
        }
        /// <summary>
        /// Determines, if the buffer is open for draw operations.
        /// </summary>
        private bool IsBegin
        {
            get;
            set;
        }
        /// <summary>
        /// Current GraphicsDevice.
        /// </summary>
        public GraphicsDevice GraphicsDevice
        {
            get;
            set;
        }
        /// <summary>
        /// A value indicating whether VSync is enabled.
        /// </summary>
        public bool VSync { set; get; }
        /// <summary>
        /// A value indicating whether the renderer is disposed.
        /// </summary>
        public bool IsDisposed
        {
            get;
            private set;
        }

        /// <summary>
        /// Opens the buffer for draw operations.
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
        /// Flushes the buffer.
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
        /// Disposes the renderer.
        /// </summary>
        public void Dispose()
        {
            IsDisposed = true;
            GraphicsDevice = null;
        }
        /// <summary>
        /// Draws a Rectangle.
        /// </summary>
        /// <param name="pen">The Pen.</param>
        /// <param name="rectangle">The Rectangle.</param>
        public void DrawRectangle(IPen pen, Rectangle rectangle)
        {
            CheckDisposed();

            var gdiPen = pen as GdiPen;
            if (gdiPen == null) throw new ArgumentException("GdiRenderer expects a GdiPen as resource.");

            _buffergraphics.DrawRectangle(gdiPen.GetPen(),
                new System.Drawing.Rectangle((int)rectangle.X, (int)rectangle.Y, (int)rectangle.Width, (int)rectangle.Height));
        }
        /// <summary>
        /// Draws a Line between two points.
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
        /// Draws a Ellipse.
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
        /// Draws an Arc.
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
        /// Draws a Polygon.
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
        /// Draws a corner-rounded Rectangle.
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
            gfxPath.AddArc(rectangle.X + rectangle.Width - radius, rectangle.Y + rectangle.Height - radius, radius, radius, 0, 90);
            gfxPath.AddArc(rectangle.X, rectangle.Y + rectangle.Height - radius, radius, radius, 90, 90);
            gfxPath.CloseAllFigures();
            _buffergraphics.DrawPath(gdiPen.GetPen(), gfxPath);
        }
        /// <summary>
        /// Fills a Rectangle.
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
        /// Fills a Ellipse.
        /// </summary>
        /// <param name="color">The Color.</param>
        /// <param name="rectangle">The Rectangle.</param>
        public void FillEllipse(Color color, Rectangle rectangle)
        {
            CheckDisposed();

            _buffergraphics.FillEllipse(new SolidBrush(color.ToWin32Color()),
                new System.Drawing.Rectangle((int)rectangle.X, (int)rectangle.Y, (int)rectangle.Width,
                    (int)rectangle.Height));
        }
        /// <summary>
        /// Fills a Polygon.
        /// </summary>
        /// <param name="color">The Color.</param>
        /// <param name="points">The Points.</param>
        public void FillPolygon(Color color, Vector2[] points)
        {
            CheckDisposed();

            _buffergraphics.FillPolygon(new SolidBrush(color.ToWin32Color()), points.ToPoints());
        }
        /// <summary>
        /// Draws a corner-rounded Rectangle.
        /// </summary>
        /// <param name="color">The Color.</param>
        /// <param name="rectangle">The Rectangle.</param>
        /// <param name="radius">The Radius.</param>
        public void FillRoundedRectangle(Color color, Rectangle rectangle, int radius)
        {
            var gfxPath = new GraphicsPath();
            gfxPath.AddArc(rectangle.X, rectangle.Y, radius, radius, 180, 90);
            gfxPath.AddArc(rectangle.X + rectangle.Width - radius, rectangle.Y, radius, radius, 270, 90);
            gfxPath.AddArc(rectangle.X + rectangle.Width - radius, rectangle.Y + rectangle.Height - radius, radius, radius, 0, 90);
            gfxPath.AddArc(rectangle.X, rectangle.Y + rectangle.Height - radius, radius, radius, 90, 90);
            gfxPath.CloseAllFigures();
            _buffergraphics.FillPath(new SolidBrush(color.ToWin32Color()), gfxPath);
        }

        /// <summary>
        /// Draws a Texture.
        /// </summary>
        /// <param name="texture">The Texture.</param>
        /// <param name="rectangle">The Rectangle.</param>
        /// <param name="color">The Color.</param>
        public void DrawTexture(ITexture texture, Rectangle rectangle, Color color)
        {
            CheckDisposed();

            var gdiTexture = texture as GdiTexture;
            if (gdiTexture == null) throw new ArgumentException("GdiRenderer expects a GdiTexture resource.");

            if (color != Color.White)
            {
                var tempBmp = new Bitmap(gdiTexture.Width, gdiTexture.Height);
                var g = Graphics.FromImage(tempBmp);
                g.DrawImage(gdiTexture.Bmp, 0, 0);
                g.FillRectangle(new SolidBrush(color.ToWin32Color()),
                                new System.Drawing.Rectangle(0, 0, gdiTexture.Width, gdiTexture.Height));
                g.Dispose();
                _buffergraphics.DrawImage(tempBmp, new System.Drawing.Rectangle((int)rectangle.X, (int)rectangle.Y, (int)rectangle.Width, (int)rectangle.Height));
                tempBmp.Dispose();
            }
            else
            {
                _buffergraphics.DrawImage(gdiTexture.Bmp, new System.Drawing.Rectangle((int)rectangle.X, (int)rectangle.Y, (int)rectangle.Width, (int)rectangle.Height));
            }
        }

        /// <summary>
        /// Draws a Texture.
        /// </summary>
        /// <param name="texture">The Texture.</param>
        /// <param name="position">The Position.</param>
        /// <param name="color">The Color.</param>
        public void DrawTexture(ITexture texture, Vector2 position, Color color)
        {
            CheckDisposed();

            var gdiTexture = texture as GdiTexture;
            if (gdiTexture == null) throw new ArgumentException("GdiRenderer expects a GdiTexture resource.");

            if (color != Color.White)
            {
                var tempBmp = new Bitmap(gdiTexture.Width, gdiTexture.Height);
                var g = Graphics.FromImage(tempBmp);
                g.DrawImage(gdiTexture.Bmp, 0, 0);
                g.FillRectangle(new SolidBrush(color.ToWin32Color()),
                                new System.Drawing.Rectangle(0, 0, gdiTexture.Width, gdiTexture.Height));
                g.Dispose();
                _buffergraphics.DrawImage(tempBmp, position.ToPoint());
                tempBmp.Dispose();
            }
            else
            {
                _buffergraphics.DrawImage(gdiTexture.Bmp, position.ToPoint());
            }
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
        /// Draws a string.
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
        /// Measures the string.
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

            var result = _buffergraphics.MeasureString(text, gdifont.GetFont());

            return new Vector2(result.Width, result.Height);
        }
        /// <summary>
        /// Sets the Transform.
        /// </summary>
        /// <param name="matrix">The Matrix.</param>
        public void SetTransform(Matrix2x3 matrix)
        {
            CheckDisposed();

            _buffergraphics.Transform = new System.Drawing.Drawing2D.Matrix(matrix[0, 0], matrix[1, 0], matrix[0, 1],
                matrix[1, 1], matrix.OffsetX, matrix.OffsetY);
        }
        /// <summary>
        /// Resets the Transform.
        /// </summary>
        public void ResetTransform()
        {
            CheckDisposed();

            _buffergraphics.ResetTransform();
        }

        /// <summary>
        /// Constructs the Component.
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
            _buffergraphics.CompositingQuality = _quality.Compositing ? CompositingQuality.HighQuality : CompositingQuality.AssumeLinear;
            _buffergraphics.PixelOffsetMode = _quality.HighQualityPixelOffset
                ? PixelOffsetMode.HighQuality
                : PixelOffsetMode.HighSpeed;
            GraphicsDevice.ClearColor = Color.CornflowerBlue;
            _buffergraphics.Clear(GraphicsDevice.ClearColor.ToWin32Color());
            _buffergraphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;

            _buffergraphics.PageUnit = GraphicsUnit.Pixel;
            //add extensions:
            SGL.Components.Get<ContentManager>().Extend(new GdiSpriteLoader());
        }

        #endregion

        #region GDIRenderer
        /// <summary>
        /// Initializes a new GdiRenderer.
        /// </summary>
        public GdiRenderer()
        {
            _quality = new GdiQuality(GdiQualityMode.High);
        }

        private Bitmap _buffer;
        private Graphics _buffergraphics;
        private GdiQuality _quality;

        /// <summary>
        /// Starts the rendering pipe.
        /// </summary>
        private void Renderer()
        {
            Present();
        }
        /// <summary>
        /// Releases the frame.
        /// </summary>
        private void Present()
        {
            var control = Control.FromHandle(GraphicsDevice.RenderTarget.Handle);
            if (control != null)
            {
                var width = control.Width;
                var height = control.Height;
                if (!GraphicsDevice.DisplayMode.Scaling)
                {
                    width = GraphicsDevice.DisplayMode.Width;
                    height = GraphicsDevice.DisplayMode.Height;
                }
                var graphics = control.CreateGraphics();
                var hdc = graphics.GetHdc();
                var intPtr = GdiNative.CreateCompatibleDC(hdc);
                var hbitmap = _buffer.GetHbitmap();
                GdiNative.SelectObject(intPtr, hbitmap);
                GdiNative.StretchBlt(hdc, 0, 0, width, height, intPtr, 0, 0, GraphicsDevice.DisplayMode.Width, GraphicsDevice.DisplayMode.Height, GdiNative.GdiRasterOperations.SRCCOPY);
                GdiNative.DeleteObject(hbitmap);
                GdiNative.DeleteDC(intPtr);
                graphics.ReleaseHdc(hdc);
                graphics.Dispose();
            }
        }

        /// <summary>
        /// Changes the current Quality.
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
                _buffergraphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
                _buffergraphics.CompositingMode = CompositingMode.SourceOver;
            }
        }

        /// <summary>
        /// Checks if the GDIRenderer is disposed.
        /// </summary>
        private void CheckDisposed()
        {
            if (IsDisposed)
            {
                throw new ObjectDisposedException("GdiRenderer");
            }
        }
        #endregion
    }
}
