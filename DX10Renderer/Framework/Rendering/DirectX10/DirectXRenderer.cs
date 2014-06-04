using System;
using System.Drawing;
using Sharpex2D.Framework.Content;
using Sharpex2D.Framework.Content.Pipeline.Processors;
using Sharpex2D.Framework.Rendering.DirectX10.Font;
using SlimDX;
using SlimDX.Direct2D;
using SlimDX.Direct3D10;
using SlimDX.DirectWrite;
using SlimDX.DXGI;
using Sharpex2D.Framework.Math;
using Sharpex2D.Framework.Rendering.Font;
using Vector2 = Sharpex2D.Framework.Math.Vector2;
using FactoryType = SlimDX.Direct2D.FactoryType;
using Rectangle = Sharpex2D.Framework.Math.Rectangle;

namespace Sharpex2D.Framework.Rendering.DirectX10
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Tested)]
    public class DirectXRenderer : IRenderer
    {

        private SlimDX.Direct3D10_1.Device1 _direct3D10Device;
        private SwapChain _swapChain;
        private RenderTarget _renderTarget;

        #region IConstructable Implementation
        /// <summary>
        /// Gets the Guid.
        /// </summary>
        public Guid Guid { get { return new Guid("0E6ED83A-26F5-416C-9B7A-B0B3DC5B1DBC"); } }
        /// <summary>
        /// Constructs the component.
        /// </summary>
        public void Construct()
        {
            GraphicsDevice.ClearColor = Color.CornflowerBlue;
            var descMode = new ModeDescription
            {
                Width = GraphicsDevice.DisplayMode.Width,
                Height = GraphicsDevice.DisplayMode.Height,
                Format = Format.R8G8B8A8_UNorm,
                RefreshRate = new Rational(60, 1),
                Scaling= DisplayModeScaling.Stretched
            };

            var descSwap = new SwapChainDescription
            {
                BufferCount = 1,
                ModeDescription = descMode,
                Usage = Usage.RenderTargetOutput,
                OutputHandle = GraphicsDevice.RenderTarget.Handle,
                SampleDescription = new SampleDescription(1, 0),
                IsWindowed = true,
                SwapEffect = SwapEffect.Discard,
                Flags = SwapChainFlags.None, 
            };


            var creationResult = SlimDX.Direct3D10_1.Device1.CreateWithSwapChain(null, DriverType.Hardware,
                DeviceCreationFlags.BgraSupport, SlimDX.Direct3D10_1.FeatureLevel.Level_10_0, descSwap,
                out _direct3D10Device, out _swapChain);
         
            if (creationResult.IsFailure)
            {
                throw new RenderInitializeException("DirectX10 is not supported on the current platform.");
            }

            var backBuffer = SlimDX.DXGI.Surface.FromSwapChain(_swapChain, 0);
            var d2DFactory = new SlimDX.Direct2D.Factory(FactoryType.Multithreaded);
            var dpi = d2DFactory.DesktopDpi;

            var renderTarget = RenderTarget.FromDXGI(d2DFactory, backBuffer, new RenderTargetProperties
            {
                HorizontalDpi = dpi.Width,
                VerticalDpi = dpi.Height,
                MinimumFeatureLevel = FeatureLevel.Default,
                PixelFormat = new PixelFormat(Format.R8G8B8A8_UNorm, AlphaMode.Ignore),
                Type = RenderTargetType.Hardware,
                Usage = RenderTargetUsage.None
            });
            renderTarget.AntialiasMode = AntialiasMode.Aliased;

            _renderTarget = renderTarget;
            DirectXHelper.RenderTarget = _renderTarget;
            DirectXHelper.Direct2DFactory = d2DFactory;
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

            _renderTarget.DrawRectangle(dxPen.GetPen(), DirectXHelper.ConvertRectangle(rectangle));
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

            _renderTarget.DrawLine(dxPen.GetPen(), DirectXHelper.ConvertPointF(start),
                DirectXHelper.ConvertPointF(target));
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

            _renderTarget.DrawEllipse(dxPen.GetPen(), DirectXHelper.ConvertEllipse(rectangle));
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

            var geometry = new PathGeometry(DirectXHelper.Direct2DFactory);
            using (var sink = geometry.Open())
            {
                sink.BeginFigure(DirectXHelper.ConvertVector(points[0]), FigureBegin.Hollow);

                for (var i = 1; i < points.Length; i++)
                    sink.AddLine(DirectXHelper.ConvertVector(points[i]));

                sink.EndFigure(FigureEnd.Closed);
                sink.Close();

                _renderTarget.DrawGeometry(geometry, dxPen.GetPen(), dxPen.Width);
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

            _renderTarget.FillRectangle(new SolidColorBrush(_renderTarget, DirectXHelper.ConvertColor(color)),
                DirectXHelper.ConvertRectangle(rectangle));

        }
        /// <summary>
        /// Fills a Ellipse.
        /// </summary>
        /// <param name="color">The Color.</param>
        /// <param name="rectangle">The Rectangle.</param>
        public void FillEllipse(Color color, Rectangle rectangle)
        {
            CheckDisposed();

            _renderTarget.FillEllipse(new SolidColorBrush(_renderTarget, DirectXHelper.ConvertColor(color)),
                DirectXHelper.ConvertEllipse(rectangle));
        }
        /// <summary>
        /// Fills a Polygon.
        /// </summary>
        /// <param name="color">The Color.</param>
        /// <param name="points">The Points.</param>
        public void FillPolygon(Color color, Vector2[] points)
        {
            CheckDisposed();

            var geometry = new PathGeometry(DirectXHelper.Direct2DFactory);
            using (var sink = geometry.Open())
            {
                sink.BeginFigure(DirectXHelper.ConvertVector(points[0]), FigureBegin.Filled);

                for (var i = 1; i < points.Length; i++)
                    sink.AddLine(DirectXHelper.ConvertVector(points[i]));

                sink.EndFigure(FigureEnd.Closed);
                sink.Close();

                _renderTarget.FillGeometry(geometry,
                    new SolidColorBrush(DirectXHelper.RenderTarget, DirectXHelper.ConvertColor(color)));
            }

            geometry.Dispose();
        }
        /// <summary>
        /// Fills a corner-rounded Rectangle.
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
        /// Disposes the object.
        /// </summary>
        public void Dispose()
        {
            if (IsDisposed) return;

            IsDisposed = true;
            _swapChain.Dispose();
            _renderTarget.Dispose();
            _direct3D10Device.Dispose();
        }
        /// <summary>
        /// Sets or gets the GraphicsDevice.
        /// </summary>
        public GraphicsDevice GraphicsDevice { get; set; }
        /// <summary>
        /// Sets or gets whether the renderer is disposed.
        /// </summary>
        public bool IsDisposed { get; private set; }
        /// <summary>
        /// A value indicating whether VSync is enabled.
        /// </summary>
        public bool VSync { get; set; }
        /// <summary>
        /// Begins the draw operation.
        /// </summary>
        public void Begin()
        {
            CheckDisposed();

            _renderTarget.BeginDraw();
            _renderTarget.Transform = Matrix3x2.Identity;
            _renderTarget.Clear(DirectXHelper.ConvertColor(GraphicsDevice.ClearColor));
        }
        /// <summary>
        /// Flushes the buffer.
        /// </summary>
        public void Close()
        {
            CheckDisposed();

            _renderTarget.EndDraw();
            _swapChain.Present(VSync ? 60 : 0, PresentFlags.None);
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

            var dxFont = font as DirectXFont;
            if (dxFont == null) throw new ArgumentException("DirectXRenderer expects a DirectXFont as resource.");

            _renderTarget.DrawText(text, dxFont.GetFont(),
                new RectangleF(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height), DirectXHelper.ConvertSolidColorBrush(color));
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

            var dxFont = font as DirectXFont;
            if (dxFont == null) throw new ArgumentException("DirectXRenderer expects a DirectXFont as resource.");

            _renderTarget.DrawText(text, dxFont.GetFont(),
                new RectangleF(position.X, position.Y, 9999, 9999), DirectXHelper.ConvertSolidColorBrush(color));
        }
        /// <summary>
        /// Draws a Texture.
        /// </summary>
        /// <param name="texture">The Texture.</param>
        /// <param name="position">The Position.</param>
        /// <param name="color">The Color.</param>
        /// <param name="opacity">The Opacity.</param>
        public void DrawTexture(ITexture texture, Vector2 position, Color color, float opacity = 1f)
        {
            CheckDisposed();

            var dxTexture = texture as DirectXTexture;
            if (dxTexture == null) throw new ArgumentException("DirectXRenderer expects a DirectXTexture as resource.");
            var dxBmp = dxTexture.GetBitmap();
            _renderTarget.DrawBitmap(dxBmp,
                new RectangleF(position.X, position.Y, texture.Width, texture.Height), opacity, InterpolationMode.Linear);
        }
        /// <summary>
        /// Draws a Texture.
        /// </summary>
        /// <param name="texture">The Texture.</param>
        /// <param name="rectangle">The Rectangle.</param>
        /// <param name="color">The Color.</param>
        /// <param name="opacity">The Opacity.</param>
        public void DrawTexture(ITexture texture, Rectangle rectangle, Color color, float opacity = 1f)
        {
            CheckDisposed();

            var dxTexture = texture as DirectXTexture;
            if (dxTexture == null) throw new ArgumentException("DirectXRenderer expects a DirectXTexture as resource.");
            var dxBmp = dxTexture.GetBitmap();
            _renderTarget.DrawBitmap(dxBmp, DirectXHelper.ConvertRectangle(rectangle), opacity,
                InterpolationMode.Linear);
        }
        /// <summary>
        /// Measures the string.
        /// </summary>
        /// <param name="text">The String.</param>
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
        public void SetTransform(Matrix2x3 matrix)
        {
            CheckDisposed();

            var dxMatrix = new Matrix3x2
            {
                
                M31 = matrix.OffsetX,
                M32 = matrix.OffsetY,
                M11 = matrix[0, 0],
                M22 = matrix[1, 1],
                M12 = matrix[1, 0],
                M21 = matrix[0, 1]
            };

            _renderTarget.Transform = dxMatrix;
        }
        /// <summary>
        /// Resets the Transform.
        /// </summary>
        public void ResetTransform()
        {
            CheckDisposed();

            DirectXHelper.RenderTarget.Transform = Matrix3x2.Identity;
        }

        #endregion

        /// <summary>
        /// Initializes a new DirectXRenderer class.
        /// </summary>
        public DirectXRenderer()
        {
            DirectXHelper.Direct2DFactory = new SlimDX.Direct2D.Factory(FactoryType.Multithreaded);
            DirectXHelper.DirectWriteFactory = new SlimDX.DirectWrite.Factory(SlimDX.DirectWrite.FactoryType.Shared);
            
            var contentManager = SGL.Components.Get<ContentManager>();

            contentManager.ContentProcessor.Add(new DirectXFontContentProcessor());
            contentManager.ContentProcessor.Add(new DirectXPenContentProcessor());
            contentManager.ContentProcessor.Add(new DirectXTextureContentProcessor());
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
        /// Deconstructs the object.
        /// </summary>
        ~DirectXRenderer()
        {
            Dispose();
        }
    }
}
