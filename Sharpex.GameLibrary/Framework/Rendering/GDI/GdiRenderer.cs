using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using SharpexGL.Framework.Common.Extensions;
using SharpexGL.Framework.Math;
using SharpexGL.Framework.Rendering.Font;
using Rectangle = SharpexGL.Framework.Math.Rectangle;

namespace SharpexGL.Framework.Rendering.GDI
{
    public class GdiRenderer : IRenderer
    {

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
        /// Current Framerate.
        /// </summary>
        public int FramesPerSecond
        {
            get
            {
                return _drawInfo.FramesPerSecond;
            }
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
        /// Determines if the renderer is disposed.
        /// </summary>
        public bool IsDisposed
        {
            get;
            set;
        }

        /// <summary>
        /// Opens the buffer for draw operations.
        /// </summary>
        public void Begin()
        {
            if (!IsBegin)
            {
                IsBegin = true;
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
                _drawInfo.AddDraw();
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
        /// <param name="color">The Color.</param>
        /// <param name="rectangle">The Rectangle.</param>
        public void DrawRectangle(Color color, Rectangle rectangle)
        {
            CheckDisposed();

            _buffergraphics.DrawRectangle(new Pen(new SolidBrush(color.ToWin32Color())),
                new System.Drawing.Rectangle((int)rectangle.X, (int)rectangle.Y, (int)rectangle.Width, (int)rectangle.Height));
        }
        /// <summary>
        /// Draws a Line between two points.
        /// </summary>
        /// <param name="color">The Color.</param>
        /// <param name="start">The Startpoint.</param>
        /// <param name="target">The Targetpoint.</param>
        public void DrawLine(Color color, Vector2 start, Vector2 target)
        {
            CheckDisposed();

            _buffergraphics.DrawLine(new Pen(new SolidBrush(color.ToWin32Color())), start.X, start.Y, target.X, target.Y);
        }
        /// <summary>
        /// Draws a Ellipse.
        /// </summary>
        /// <param name="color">The Color.</param>
        /// <param name="rectangle">The Rectangle.</param>
        public void DrawEllipse(Color color, Rectangle rectangle)
        {
            CheckDisposed();

            _buffergraphics.DrawEllipse(new Pen(new SolidBrush(color.ToWin32Color())),
                new System.Drawing.Rectangle((int) rectangle.X, (int) rectangle.Y, (int) rectangle.Width,
                    (int) rectangle.Height));
        }
        /// <summary>
        /// Draws an Arc.
        /// </summary>
        /// <param name="color">The Color.</param>
        /// <param name="rectangle">The Rectangle.</param>
        /// <param name="startAngle">The StartAngle.</param>
        /// <param name="sweepAngle">The SweepAngle.</param>
        public void DrawArc(Color color, Rectangle rectangle, float startAngle, float sweepAngle)
        {
            CheckDisposed();

            _buffergraphics.DrawArc(new Pen(new SolidBrush(color.ToWin32Color())),
                new System.Drawing.Rectangle((int) rectangle.X, (int) rectangle.Y, (int) rectangle.Width,
                    (int) rectangle.Height), startAngle, sweepAngle);
        }
        /// <summary>
        /// Draws a Polygon.
        /// </summary>
        /// <param name="color">The Color.</param>
        /// <param name="points">The Points.</param>
        public void DrawPolygon(Color color, Vector2[] points)
        {
            CheckDisposed();

            _buffergraphics.DrawPolygon(new Pen(new SolidBrush(color.ToWin32Color())), points.ToPoints());
        }
        /// <summary>
        /// Draws a corner-rounded Rectangle.
        /// </summary>
        /// <param name="color">The Color.</param>
        /// <param name="rectangle">The Rectangle.</param>
        /// <param name="radius">The Radius.</param>
        public void DrawRoundedRectangle(Color color, Rectangle rectangle, int radius)
        {
            var gfxPath = new GraphicsPath();
            gfxPath.AddArc(rectangle.X, rectangle.Y, radius, radius, 180, 90);
            gfxPath.AddArc(rectangle.X + rectangle.Width - radius, rectangle.Y, radius, radius, 270, 90);
            gfxPath.AddArc(rectangle.X + rectangle.Width - radius, rectangle.Y + rectangle.Height - radius, radius, radius, 0, 90);
            gfxPath.AddArc(rectangle.X, rectangle.Y + rectangle.Height - radius, radius, radius, 90, 90);
            gfxPath.CloseAllFigures();
            _buffergraphics.DrawPath(new Pen(new SolidBrush(color.ToWin32Color())), gfxPath);
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
        public void DrawTexture(Texture texture, Rectangle rectangle, Color color)
        {
            CheckDisposed();

            if (color != Color.White)
            {
                var tempBmp = new Bitmap(texture.Texture2D.Width, texture.Texture2D.Height);
                var g = Graphics.FromImage(tempBmp);
                g.DrawImage(texture.Texture2D, 0, 0);
                g.FillRectangle(new SolidBrush(color.ToWin32Color()),
                                new System.Drawing.Rectangle(0, 0, texture.Texture2D.Width, texture.Texture2D.Height));
                g.Dispose();
                _buffergraphics.DrawImage(tempBmp, new System.Drawing.Rectangle((int)rectangle.X, (int)rectangle.Y, (int)rectangle.Width, (int)rectangle.Height));
                tempBmp.Dispose();
            }
            else
            {
                _buffergraphics.DrawImage(texture.Texture2D, new System.Drawing.Rectangle((int)rectangle.X, (int)rectangle.Y, (int)rectangle.Width, (int)rectangle.Height));
            }
        }

        /// <summary>
        /// Draws a Texture.
        /// </summary>
        /// <param name="texture">The Texture.</param>
        /// <param name="position">The Position.</param>
        /// <param name="color">The Color.</param>
        public void DrawTexture(Texture texture, Vector2 position, Color color)
        {
            CheckDisposed();

            if (color != Color.White)
            {
                var tempBmp = new Bitmap(texture.Texture2D.Width, texture.Texture2D.Height);
                var g = Graphics.FromImage(tempBmp);
                g.DrawImage(texture.Texture2D, 0, 0);
                g.FillRectangle(new SolidBrush(color.ToWin32Color()),
                                new System.Drawing.Rectangle(0, 0, texture.Texture2D.Width, texture.Texture2D.Height));
                g.Dispose();
                _buffergraphics.DrawImage(tempBmp, position.ToPoint());
                tempBmp.Dispose();
            }
            else
            {
                _buffergraphics.DrawImage(texture.Texture2D, position.ToPoint());
            }
        }

        /// <summary>
        /// Draws a string.
        /// </summary>
        /// <param name="text">The Text.</param>
        /// <param name="spriteFont">The SpriteFont.</param>
        /// <param name="position">The Position.</param>
        /// <param name="color">The Color.</param>
        public void DrawString(string text, SpriteFont spriteFont, Vector2 position, Color color)
        {
            CheckDisposed();

            spriteFont.Value = text;
            spriteFont.FontColor = color.ToWin32Color();
            _buffergraphics.DrawImage(spriteFont.Render().Texture2D, position.ToPoint());
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
        }

        #endregion

        #region GDIRenderer
        /// <summary>
        /// Initializes a new GdiRenderer.
        /// </summary>
        public GdiRenderer()
        {
            _drawInfo.Start();
        }

        private Bitmap _buffer;
        private readonly AccurateFpsCounter _drawInfo = new AccurateFpsCounter();
        private Graphics _buffergraphics;
        private GdiQuality _quality;

        /// <summary>
        /// Starts the rendering pipe.
        /// </summary>
        private void Renderer()
        {
            _buffergraphics.ResetTransform();
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
                GdiNative.DeleteObject(intPtr);
                graphics.ReleaseHdc(hdc);
            }
            _buffergraphics.Clear(GraphicsDevice.ClearColor.ToWin32Color());
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
                _buffergraphics.SmoothingMode = _quality.AntiAlias ? SmoothingMode.AntiAlias : SmoothingMode.HighSpeed;
                _buffergraphics.InterpolationMode = _quality.Interpolation
                    ? InterpolationMode.High
                    : InterpolationMode.NearestNeighbor;
                _buffergraphics.CompositingQuality = _quality.Compositing
                    ? CompositingQuality.HighQuality
                    : CompositingQuality.AssumeLinear;
                _buffergraphics.PixelOffsetMode = _quality.HighQualityPixelOffset
                    ? PixelOffsetMode.HighQuality
                    : PixelOffsetMode.HighSpeed;
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
