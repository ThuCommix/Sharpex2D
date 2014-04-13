using System;
using SharpDX;
using SharpDX.Direct2D1;
using SharpDX.Direct3D;
using SharpDX.Direct3D11;
using SharpDX.DirectWrite;
using SharpDX.DXGI;
using Sharpex2D.Framework.Content;
using Sharpex2D.Framework.Content.Serialization;
using Sharpex2D.Framework.Rendering.DirectX.Font;
using Sharpex2D.Framework.Rendering.Font;
using AlphaMode = SharpDX.Direct2D1.AlphaMode;
using Device = SharpDX.Direct3D11.Device;
using Factory = SharpDX.Direct2D1.Factory;
using FactoryType = SharpDX.Direct2D1.FactoryType;
using Rectangle = Sharpex2D.Framework.Math.Rectangle;
using Vector2 = Sharpex2D.Framework.Math.Vector2;

namespace Sharpex2D.Framework.Rendering.DirectX
{
    public class DirectXRenderer : IRenderer
    {
        #region IComponent Implementation

        /// <summary>
        /// Gets the Guid.
        /// </summary>
        public Guid Guid
        {
            get { return new Guid("4CDE20B5-7BD6-4B3B-A228-60ECE08CC65B"); }
        }

        #endregion

        #region IRenderer Implementation
        /// <summary>
        /// Draws a Rectangle.
        /// </summary>
        /// <param name="pen">The Pen.</param>
        /// <param name="rectangle">The Rectangle.</param>
        public void DrawRectangle(IPen pen, Rectangle rectangle)
        {
            CheckDisposed();

            var dxPen = pen as DirectXPen;
            if (dxPen == null) throw new ArgumentException("DirectXRenderer expects a DirectXPen as resource.");

            DirectXHelper.RenderTarget.DrawRectangle(
                DirectXHelper.ConvertRectangle(rectangle), dxPen.GetPen(), dxPen.Width);
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

            var dxPen = pen as DirectXPen;
            if (dxPen == null) throw new ArgumentException("DirectXRenderer expects a DirectXPen as resource.");
            DirectXHelper.RenderTarget.DrawLine(DirectXHelper.ConvertVector(start), DirectXHelper.ConvertVector(target),
                dxPen.GetPen(), dxPen.Width);
        }
        /// <summary>
        /// Draws a Ellipse.
        /// </summary>
        /// <param name="pen">The Pen.</param>
        /// <param name="rectangle">The Rectangle.</param>
        public void DrawEllipse(IPen pen, Rectangle rectangle)
        {
            CheckDisposed();

            var dxPen = pen as DirectXPen;
            if (dxPen == null) throw new ArgumentException("DirectXRenderer expects a DirectXPen as resource.");
            DirectXHelper.RenderTarget.DrawEllipse(DirectXHelper.ConvertEllipse(rectangle), dxPen.GetPen(), dxPen.Width);
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

            throw new NotSupportedException("DrawArc is not supported by DirectXRenderer");
        }
        /// <summary>
        /// Draws a Polygon.
        /// </summary>
        /// <param name="pen">The Pen.</param>
        /// <param name="points">The Points.</param>
        public void DrawPolygon(IPen pen, Vector2[] points)
        {
            CheckDisposed();

            var dxPen = pen as DirectXPen;
            if (dxPen == null) throw new ArgumentException("DirectXRenderer expects a DirectXPen as resource.");

            var geometry = new PathGeometry(DirectXHelper.D2DFactory);
            using (var sink = geometry.Open())
            {
                sink.BeginFigure(DirectXHelper.ConvertVector(points[0]), FigureBegin.Hollow);

                for (var i = 1; i < points.Length; i++)
                    sink.AddLine(DirectXHelper.ConvertVector(points[i]));

                sink.EndFigure(FigureEnd.Closed);
                sink.Close();

                DirectXHelper.RenderTarget.DrawGeometry(geometry, dxPen.GetPen(), dxPen.Width);
            }

            geometry.Dispose();
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

            throw new NotSupportedException("DrawRoundedRectangle is not supported by DirectXRenderer");
        }
        /// <summary>
        /// Fills a Rectangle.
        /// </summary>
        /// <param name="color">The Color.</param>
        /// <param name="rectangle">The Rectangle.</param>
        public void FillRectangle(Color color, Rectangle rectangle)
        {
            CheckDisposed();

            DirectXHelper.RenderTarget.FillRectangle(DirectXHelper.ConvertRectangle(rectangle),
                new SolidColorBrush(DirectXHelper.RenderTarget, DirectXHelper.ConvertColor(color)));
        }
        /// <summary>
        /// Fills a Ellipse.
        /// </summary>
        /// <param name="color">The Color.</param>
        /// <param name="rectangle">The Rectangle.</param>
        public void FillEllipse(Color color, Rectangle rectangle)
        {
            CheckDisposed();

            DirectXHelper.RenderTarget.FillEllipse(DirectXHelper.ConvertEllipse(rectangle),
                new SolidColorBrush(DirectXHelper.RenderTarget, DirectXHelper.ConvertColor(color)));
        }
        /// <summary>
        /// Fills a Polygon.
        /// </summary>
        /// <param name="color">The Color.</param>
        /// <param name="points">The Points.</param>
        public void FillPolygon(Color color, Vector2[] points)
        {
            CheckDisposed();

            var geometry = new PathGeometry(DirectXHelper.D2DFactory);
            using (var sink = geometry.Open())
            {
                sink.BeginFigure(DirectXHelper.ConvertVector(points[0]), FigureBegin.Filled);

                for (var i = 1; i < points.Length; i++)
                    sink.AddLine(DirectXHelper.ConvertVector(points[i]));

                sink.EndFigure(FigureEnd.Closed);
                sink.Close();

                DirectXHelper.RenderTarget.FillGeometry(geometry,
                    new SolidColorBrush(DirectXHelper.RenderTarget, DirectXHelper.ConvertColor(color)));
            }

            geometry.Dispose();
        }
        /// <summary>
        /// Draws a corner-rounded Rectangle.
        /// </summary>
        /// <param name="color">The Color.</param>
        /// <param name="rectangle">The Rectangle.</param>
        /// <param name="radius">The Radius.</param>
        public void FillRoundedRectangle(Color color, Rectangle rectangle, int radius)
        {
            CheckDisposed();

            throw new NotSupportedException("FillRoundedRectangle is not supported by DirectXRenderer");
        }
        /// <summary>
        /// Constructs the Component.
        /// </summary>
        public void Construct()
        {
            var d2DFactory = new Factory(FactoryType.MultiThreaded);
            DirectXHelper.D2DFactory = d2DFactory;
            var dpi = d2DFactory.DesktopDpi;

            GraphicsDevice.ClearColor = Color.CornflowerBlue;

            var swapChainDesc = new SwapChainDescription
            {
                BufferCount = 2,
                Usage = Usage.RenderTargetOutput,
                OutputHandle = SGL.Components.Get<Surface.RenderTarget>().Handle,
                IsWindowed = true,
                ModeDescription = new ModeDescription(GraphicsDevice.DisplayMode.Width, GraphicsDevice.DisplayMode.Height, new Rational((int)SGL.Components.Get<GraphicsDevice>().RefreshRate, 1), Format.R8G8B8A8_UNorm),
                SampleDescription = new SampleDescription(1, 0),
                Flags = SwapChainFlags.AllowModeSwitch,
                SwapEffect = SwapEffect.Discard
            };

            _swapChainDesc = swapChainDesc;

            if (VSync)
            {
                swapChainDesc.ModeDescription.RefreshRate = new Rational(60, 1);
            }

            swapChainDesc.ModeDescription.Scaling = GraphicsDevice.DisplayMode.Scaling ? DisplayModeScaling.Stretched : DisplayModeScaling.Centered;

            Device device;
            SwapChain swapChain;
            Device.CreateWithSwapChain(DriverType.Hardware, DeviceCreationFlags.BgraSupport, swapChainDesc, out device, out swapChain);
            _swapChain = swapChain;

            // Get back buffer in a Direct2D-compatible format (DXGI surface)
            var backBuffer = SharpDX.DXGI.Surface.FromSwapChain(swapChain, 0);

            var renderTarget = new RenderTarget(d2DFactory, backBuffer, new RenderTargetProperties
            {
                DpiX = dpi.Width,
                DpiY = dpi.Height,
                MinLevel = SharpDX.Direct2D1.FeatureLevel.Level_DEFAULT,
                PixelFormat = new PixelFormat(Format.R8G8B8A8_UNorm, AlphaMode.Ignore),
                Type = RenderTargetType.Hardware,
                Usage = RenderTargetUsage.None
            }) { TextAntialiasMode = TextAntialiasMode.Cleartype };
            DirectXHelper.RenderTarget = renderTarget;
        }
        /// <summary>
        /// Disposes the renderer.
        /// </summary>
        public void Dispose()
        {
            if (!IsDisposed)
            {
                IsDisposed = true;
                _swapChain.Dispose();
                DirectXHelper.RenderTarget.Dispose();
            }
        }
        /// <summary>
        /// Opens the buffer for draw operations.
        /// </summary>
        public void Begin()
        {
            CheckDisposed();

            _swapChainDesc.ModeDescription.Scaling = GraphicsDevice.DisplayMode.Scaling ? DisplayModeScaling.Stretched : DisplayModeScaling.Centered;
            DirectXHelper.RenderTarget.BeginDraw();
            DirectXHelper.RenderTarget.Transform = Matrix3x2.Identity;
            DirectXHelper.RenderTarget.Clear(DirectXHelper.ConvertColor(GraphicsDevice.ClearColor));
        }
        /// <summary>
        /// Flushes the buffer.
        /// </summary>
        public void Close()
        {
            CheckDisposed();

            DirectXHelper.RenderTarget.EndDraw();
            _swapChain.Present(0, PresentFlags.None);
        }
        /// <summary>
        /// Draws a string.
        /// </summary>
        /// <param name="text">The Text.</param>
        /// <param name="font">The Fonr.</param>
        /// <param name="rectangle">The Rectangle.</param>
        /// <param name="color">The Color.</param>
        public void DrawString(string text, IFont font, Rectangle rectangle, Color color)
        {
            CheckDisposed();

            var dxFont = font as DirectXFont;
            if (dxFont == null) throw new ArgumentException("DirectXRenderer expects a DirectXFont as resource.");

            DirectXHelper.RenderTarget.DrawText(text, dxFont.GetFont(),
                new RectangleF(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height), DirectXHelper.ConvertSolidColorBrush(color));
        }
        /// <summary>
        /// Draws a string.
        /// </summary>
        /// <param name="text">The Text.</param>
        /// <param name="font">The Fonr.</param>
        /// <param name="position">The Position.</param>
        /// <param name="color">The Color.</param>
        public void DrawString(string text, IFont font, Vector2 position, Color color)
        {
            CheckDisposed();

            var dxFont = font as DirectXFont;
            if (dxFont == null) throw new ArgumentException("DirectXRenderer expects a DirectXFont as resource.");

            DirectXHelper.RenderTarget.DrawText(text, dxFont.GetFont(),
                new RectangleF(position.X, position.Y, 9999, 9999), DirectXHelper.ConvertSolidColorBrush(color));
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

            var dxTexture = texture as DirectXTexture;
            if (dxTexture == null) throw new ArgumentException("DirectXRenderer expects a DirectXTexture as resource.");
            var dxBmp = dxTexture.GetBitmap();
            DirectXHelper.RenderTarget.DrawBitmap(dxBmp, new RectangleF(position.X, position.Y, texture.Width, texture.Height), 1,
                BitmapInterpolationMode.Linear);
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

            var dxTexture = texture as DirectXTexture;
            if (dxTexture == null) throw new ArgumentException("DirectXRenderer expects a DirectXTexture as resource.");
            var dxBmp = dxTexture.GetBitmap();
            DirectXHelper.RenderTarget.DrawBitmap(dxBmp, DirectXHelper.ConvertRectangle(rectangle), 1,
                BitmapInterpolationMode.Linear);
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

            var dxFont = font as DirectXFont;
            if (dxFont == null) throw new ArgumentException("DirectXRenderer expects a DirectXFont as resource.");

            var fontData = dxFont.GetFont();

            fontData.ParagraphAlignment = ParagraphAlignment.Near;
            fontData.TextAlignment = TextAlignment.Leading;
            fontData.WordWrapping = WordWrapping.NoWrap;

            using (var layout = new TextLayout(DirectXHelper.DirectWriteFactory, text, fontData, float.MaxValue, float.MaxValue))
            {
                return new Vector2(layout.Metrics.Width, layout.Metrics.Height);
            }
        }
        /// <summary>
        /// Sets the Transform.
        /// </summary>
        /// <param name="matrix">The Matrix.</param>
        public void SetTransform(Math.Matrix2x3 matrix)
        {
            CheckDisposed();

            var dxMatrix = new Matrix3x2
            {
                TranslationVector = DirectXHelper.ConvertVector(new Vector2(matrix.OffsetX, matrix.OffsetY)),
                ScaleVector = DirectXHelper.ConvertVector(new Vector2(matrix[0, 0], matrix[1, 1])),
                M12 = matrix[1, 0],
                M21 = matrix[0, 1]
            };

            DirectXHelper.RenderTarget.Transform = dxMatrix;
        }
        /// <summary>
        /// Resets the Transform.
        /// </summary>
        public void ResetTransform()
        {
            CheckDisposed();

            DirectXHelper.RenderTarget.Transform = Matrix3x2.Identity;
        }

        /// <summary>
        /// Sets or gets the GraphicsDevice.
        /// </summary>
        public GraphicsDevice GraphicsDevice { get; set; }
        /// <summary>
        /// A value indicating whether the renderer is disposed.
        /// </summary>
        public bool IsDisposed { get; private set; }
        /// <summary>
        /// A value indicating whether VSync is enabled.
        /// </summary>
        public bool VSync { get; set; }

        #endregion

        private SwapChain _swapChain;

        /// <summary>
        /// Initializes a new DirectXRenderer class.
        /// </summary>
        public DirectXRenderer()
        {
            DirectXHelper.DirectWriteFactory = new SharpDX.DirectWrite.Factory();
            SGL.Components.Get<Implementation.ImplementationManager>().AddImplementation(new DirectXTextureSerializer());
            SGL.Components.Get<ContentManager>().Extend(new DirectXTextureLoader());
            SGL.Components.Get<ContentManager>().Extend(new DirectXSpriteLoader());
        }

        /// <summary>
        /// Checks if the DirectXRenderer is disposed.
        /// </summary>
        private void CheckDisposed()
        {
            if (IsDisposed)
            {
                throw new ObjectDisposedException("DirectXRenderer");
            }
        }

        /// <summary>
        /// Deconstructor for DirectXRenderer class.
        /// </summary>
        ~DirectXRenderer()
        {
            Dispose();
        }

        private SwapChainDescription _swapChainDesc;
    }
}
