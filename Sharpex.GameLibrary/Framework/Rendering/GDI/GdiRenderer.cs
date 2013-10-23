using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using SharpexGL.Framework.Math;
using SharpexGL.Framework.Rendering.Font;

namespace SharpexGL.Framework.Rendering.GDI
{
    public class GdiRenderer : IRenderer
    {
        private Bitmap _buffer;
        private AccurateFpsCounter DrawInfo = new AccurateFpsCounter();
        private Graphics _buffergraphics;
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
                return DrawInfo.FramesPerSecond;
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
        /// Determines if the renderer is disposed.
        /// </summary>
        public bool IsDisposed
        {
            get;
            set;
        }
        /// <summary>
        /// Initializes a new GdiRenderer.
        /// </summary>
        public GdiRenderer()
        {
            DrawInfo.Start();
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
                DrawInfo.AddDraw();
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
        /// Draws a Texture.
        /// </summary>
        /// <param name="texture">The Texture.</param>
        /// <param name="rectangle">The Rectangle.</param>
        /// <param name="color">The Color.</param>
        public void DrawTexture(Texture texture, Math.Rectangle rectangle, Color color)
        {
            if (IsDisposed)
            {
                throw new ObjectDisposedException("GdiRenderer");
            }
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
            if (IsDisposed)
            {
                throw new ObjectDisposedException("GdiRenderer");
            }
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
            if (IsDisposed)
            {
                throw new ObjectDisposedException("GdiRenderer");
            }
            spriteFont.Value = text;
            spriteFont.FontColor = color.ToWin32Color();
            _buffergraphics.DrawImage(spriteFont.Render().Texture2D, position.ToPoint());
        }
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
            Control control = Control.FromHandle(GraphicsDevice.DeviceHandle);
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
        /// Constructs the Component.
        /// </summary>
        public void Construct()
        {
            _buffer = new Bitmap(GraphicsDevice.DisplayMode.Width, GraphicsDevice.DisplayMode.Height);
            _buffergraphics = Graphics.FromImage(_buffer);
            _buffergraphics.InterpolationMode = InterpolationMode.NearestNeighbor;
            _buffergraphics.PixelOffsetMode = PixelOffsetMode.HighSpeed;
            _buffergraphics.SmoothingMode = SmoothingMode.HighSpeed;
            _buffergraphics.CompositingQuality = CompositingQuality.AssumeLinear;
            GraphicsDevice.ClearColor = Color.CornflowerBlue;
            _buffergraphics.Clear(GraphicsDevice.ClearColor.ToWin32Color());
        }
    }
}
