using System;
using SharpDX;
using SharpDX.Direct2D1;
using SharpDX.Direct3D;
using SharpDX.Direct3D11;
using SharpDX.DirectWrite;
using SharpDX.DXGI;
using Sharpex2D.Framework.Content.Pipeline;
using Sharpex2D.Framework.Content.Pipeline.Processors;
using Sharpex2D.Framework.Math;
using Sharpex2D.Framework.Rendering.Devices;
using Sharpex2D.Framework.Rendering.DirectX11.Fonts;
using Device = SharpDX.Direct3D11.Device;
using Ellipse = Sharpex2D.Framework.Math.Ellipse;
using Factory = SharpDX.Direct2D1.Factory;
using FactoryType = SharpDX.Direct2D1.FactoryType;
using FeatureLevel = SharpDX.Direct2D1.FeatureLevel;
using Font = Sharpex2D.Framework.Rendering.Fonts.Font;
using Rectangle = Sharpex2D.Framework.Math.Rectangle;
using RenderTarget = Sharpex2D.Framework.Surface.RenderTarget;
using Vector2 = Sharpex2D.Framework.Math.Vector2;

namespace Sharpex2D.Framework.Rendering.DirectX11
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Tested)]
    [Device("DirectX 11 Device")]
    public class DirectXRenderDevice : RenderDevice
    {
        private readonly IContentProcessor[] _contentProcessors;
        private readonly InterpolationMode _interpolationMode;
        private readonly SmoothingMode _smoothingMode;
        private SwapChain _swapChain;
        private SwapChainDescription _swapChainDesc;

        /// <summary>
        ///     Initializes a new DirectXRenderDevice class.
        /// </summary>
        /// <param name="interpolationMode">The InterpolationMode.</param>
        /// <param name="smoothingMode">The SmoothingMode.</param>
        public DirectXRenderDevice(InterpolationMode interpolationMode, SmoothingMode smoothingMode)
            : base(new DirectXResourceManager(), new Guid("4CDE20B5-7BD6-4B3B-A228-60ECE08CC65B"))
        {
            _contentProcessors = new IContentProcessor[]
            {new DirectXFontContentProcessor(), new DirectXPenContentProcessor(), new DirectXTextureContentProcessor()};

            _interpolationMode = interpolationMode;
            _smoothingMode = smoothingMode;

            var d2DFactory = new Factory(FactoryType.MultiThreaded);
            DirectXHelper.D2DFactory = d2DFactory;
            DirectXHelper.DirectWriteFactory = new SharpDX.DirectWrite.Factory(SharpDX.DirectWrite.FactoryType.Shared);
        }

        /// <summary>
        ///     Initializes a new DirectXRenderDevice class.
        /// </summary>
        public DirectXRenderDevice()
            : base(new DirectXResourceManager(), new Guid("4CDE20B5-7BD6-4B3B-A228-60ECE08CC65B"))
        {
            _contentProcessors = new IContentProcessor[]
            {new DirectXFontContentProcessor(), new DirectXPenContentProcessor(), new DirectXTextureContentProcessor()};

            _interpolationMode = InterpolationMode.Linear;
            _smoothingMode = SmoothingMode.AntiAlias;

            var d2DFactory = new Factory(FactoryType.MultiThreaded);
            DirectXHelper.D2DFactory = d2DFactory;
            DirectXHelper.DirectWriteFactory = new SharpDX.DirectWrite.Factory(SharpDX.DirectWrite.FactoryType.Shared);
        }

        /// <summary>
        ///     Gets the required PlatformVersion.
        /// </summary>
        public override Version PlatformVersion
        {
            get { return new Version(6, 1, 7601); }
        }

        /// <summary>
        ///     Gets the ContentProcessors.
        /// </summary>
        public override IContentProcessor[] ContentProcessors
        {
            get { return _contentProcessors; }
        }

        /// <summary>
        ///     Initializes the Device.
        /// </summary>
        public override void InitializeDevice()
        {
            Size2F dpi = DirectXHelper.D2DFactory.DesktopDpi;

            GraphicsDevice.ClearColor = Color.CornflowerBlue;

            var swapChainDesc = new SwapChainDescription
            {
                BufferCount = 2,
                Usage = Usage.RenderTargetOutput,
                OutputHandle = SGL.Components.Get<RenderTarget>().Handle,
                IsWindowed = true,
                ModeDescription =
                    new ModeDescription(GraphicsDevice.BackBuffer.Width, GraphicsDevice.BackBuffer.Height,
                        new Rational(60, 1), Format.R8G8B8A8_UNorm),
                SampleDescription = new SampleDescription(1, 0),
                Flags = SwapChainFlags.AllowModeSwitch,
                SwapEffect = SwapEffect.Discard
            };

            _swapChainDesc = swapChainDesc;

            swapChainDesc.ModeDescription.Scaling = GraphicsDevice.BackBuffer.Scaling
                ? DisplayModeScaling.Stretched
                : DisplayModeScaling.Centered;

            Device device;
            SwapChain swapChain;
            Device.CreateWithSwapChain(DriverType.Hardware, DeviceCreationFlags.BgraSupport, swapChainDesc, out device,
                out swapChain);
            _swapChain = swapChain;


            // Get back buffer in a Direct2D-compatible format (DXGI surface)
            SharpDX.DXGI.Surface backBuffer = SharpDX.DXGI.Surface.FromSwapChain(swapChain, 0);

            var renderTarget = new SharpDX.Direct2D1.RenderTarget(DirectXHelper.D2DFactory, backBuffer,
                new RenderTargetProperties
                {
                    DpiX = dpi.Width,
                    DpiY = dpi.Height,
                    MinLevel = FeatureLevel.Level_DEFAULT,
                    PixelFormat = new PixelFormat(Format.R8G8B8A8_UNorm, AlphaMode.Ignore),
                    Type = RenderTargetType.Hardware,
                    Usage = RenderTargetUsage.None
                }) {TextAntialiasMode = TextAntialiasMode.Cleartype};
            DirectXHelper.RenderTarget = renderTarget;
        }

        /// <summary>
        ///     Begins the draw operation.
        /// </summary>
        public override void Begin()
        {
            _swapChainDesc.ModeDescription.Scaling = GraphicsDevice.BackBuffer.Scaling
                ? DisplayModeScaling.Stretched
                : DisplayModeScaling.Centered;
            DirectXHelper.RenderTarget.BeginDraw();
            DirectXHelper.RenderTarget.Transform = Matrix3x2.Identity;
            DirectXHelper.RenderTarget.Clear(DirectXHelper.ConvertColor(GraphicsDevice.ClearColor));
        }

        /// <summary>
        ///     Ends the draw operation.
        /// </summary>
        public override void End()
        {
            DirectXHelper.RenderTarget.EndDraw();
            _swapChain.Present(0, PresentFlags.None);
        }

        /// <summary>
        ///     Draws a string.
        /// </summary>
        /// <param name="text">The Text.</param>
        /// <param name="font">The Font.</param>
        /// <param name="rectangle">The Rectangle.</param>
        /// <param name="color">The Color.</param>
        public override void DrawString(string text, Font font, Rectangle rectangle, Color color)
        {
            var dxFont = font.Instance as DirectXFont;
            if (dxFont == null) throw new ArgumentException("DirectXRenderer expects a DirectXFont as resource.");

            DirectXHelper.RenderTarget.DrawText(text, dxFont.GetFont(),
                new RectangleF(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height),
                DirectXHelper.ConvertSolidColorBrush(color));
        }

        /// <summary>
        ///     Draws a string.
        /// </summary>
        /// <param name="text">The Text.</param>
        /// <param name="font">The Font.</param>
        /// <param name="position">The Position.</param>
        /// <param name="color">The Color.</param>
        public override void DrawString(string text, Font font, Vector2 position, Color color)
        {
            var dxFont = font.Instance as DirectXFont;
            if (dxFont == null) throw new ArgumentException("DirectXRenderer expects a DirectXFont as resource.");

            DirectXHelper.RenderTarget.DrawText(text, dxFont.GetFont(),
                new RectangleF(position.X, position.Y, 9999, 9999), DirectXHelper.ConvertSolidColorBrush(color));
        }

        /// <summary>
        ///     Draws a Texture.
        /// </summary>
        /// <param name="texture">The Texture.</param>
        /// <param name="position">The Position.</param>
        /// <param name="opacity">The Opacity.</param>
        /// <param name="color">The Color.</param>
        public override void DrawTexture(Texture2D texture, Vector2 position, Color color, float opacity = 1)
        {
            var dxTexture = texture as DirectXTexture;
            if (dxTexture == null) throw new ArgumentException("DirectXRenderer expects a DirectXTexture as resource.");
            Bitmap dxBmp = dxTexture.GetBitmap();
            DirectXHelper.RenderTarget.DrawBitmap(dxBmp,
                new RectangleF(position.X, position.Y, texture.Width, texture.Height), opacity,
                _interpolationMode == InterpolationMode.Linear
                    ? BitmapInterpolationMode.Linear
                    : BitmapInterpolationMode.NearestNeighbor);
        }

        /// <summary>
        ///     Draws a Texture.
        /// </summary>
        /// <param name="texture">The Texture.</param>
        /// <param name="rectangle">The Rectangle.</param>
        /// <param name="opacity">The Opacity.</param>
        /// <param name="color">The Color.</param>
        public override void DrawTexture(Texture2D texture, Rectangle rectangle, Color color, float opacity = 1)
        {
            var dxTexture = texture as DirectXTexture;
            if (dxTexture == null) throw new ArgumentException("DirectXRenderer expects a DirectXTexture as resource.");
            Bitmap dxBmp = dxTexture.GetBitmap();
            DirectXHelper.RenderTarget.DrawBitmap(dxBmp,
                new RectangleF(rectangle.X, rectangle.Y, texture.Width, texture.Height), opacity,
                _interpolationMode == InterpolationMode.Linear
                    ? BitmapInterpolationMode.Linear
                    : BitmapInterpolationMode.NearestNeighbor);
        }

        /// <summary>
        ///     Measures the string.
        /// </summary>
        /// <param name="text">The String.</param>
        /// <param name="font">The Font.</param>
        /// <returns>Vector2.</returns>
        public override Vector2 MeasureString(string text, Font font)
        {
            var dxFont = font.Instance as DirectXFont;
            if (dxFont == null) throw new ArgumentException("DirectXRenderer expects a DirectXFont as resource.");

            TextFormat fontData = dxFont.GetFont();

            fontData.ParagraphAlignment = ParagraphAlignment.Near;
            fontData.TextAlignment = TextAlignment.Leading;
            fontData.WordWrapping = WordWrapping.NoWrap;

            using (
                var layout = new TextLayout(DirectXHelper.DirectWriteFactory, text, fontData, float.MaxValue,
                    float.MaxValue))
            {
                return new Vector2(layout.Metrics.Width, layout.Metrics.Height);
            }
        }

        /// <summary>
        ///     Sets the Transform.
        /// </summary>
        /// <param name="matrix">The Matrix.</param>
        public override void SetTransform(Matrix2x3 matrix)
        {
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
        ///     Resets the Transform.
        /// </summary>
        public override void ResetTransform()
        {
            DirectXHelper.RenderTarget.Transform = Matrix3x2.Identity;
        }

        /// <summary>
        ///     Draws a Rectangle.
        /// </summary>
        /// <param name="pen">The Pen.</param>
        /// <param name="rectangle">The Rectangle.</param>
        public override void DrawRectangle(Pen pen, Rectangle rectangle)
        {
            var dxPen = pen.Instance as DirectXPen;
            if (dxPen == null) throw new ArgumentException("DirectXRenderer expects a DirectXPen as resource.");

            DirectXHelper.RenderTarget.DrawRectangle(
                DirectXHelper.ConvertRectangle(rectangle), dxPen.GetPen(), dxPen.Width);
        }

        /// <summary>
        ///     Draws a Line between two points.
        /// </summary>
        /// <param name="pen">The Pen.</param>
        /// <param name="start">The Startpoint.</param>
        /// <param name="target">The Targetpoint.</param>
        public override void DrawLine(Pen pen, Vector2 start, Vector2 target)
        {
            var dxPen = pen.Instance as DirectXPen;
            if (dxPen == null) throw new ArgumentException("DirectXRenderer expects a DirectXPen as resource.");
            DirectXHelper.RenderTarget.DrawLine(DirectXHelper.ConvertVector(start), DirectXHelper.ConvertVector(target),
                dxPen.GetPen(), dxPen.Width);
        }

        /// <summary>
        ///     Draws a Ellipse.
        /// </summary>
        /// <param name="pen">The Pen.</param>
        /// <param name="ellipse">The Ellipse.</param>
        public override void DrawEllipse(Pen pen, Ellipse ellipse)
        {
            var dxPen = pen.Instance as DirectXPen;
            if (dxPen == null) throw new ArgumentException("DirectXRenderer expects a DirectXPen as resource.");
            DirectXHelper.RenderTarget.DrawEllipse(DirectXHelper.ConvertEllipse(ellipse), dxPen.GetPen(), dxPen.Width);
        }

        /// <summary>
        ///     Draws an Arc.
        /// </summary>
        /// <param name="pen">The Pen.</param>
        /// <param name="rectangle">The Rectangle.</param>
        /// <param name="startAngle">The StartAngle.</param>
        /// <param name="sweepAngle">The SweepAngle.</param>
        public override void DrawArc(Pen pen, Rectangle rectangle, float startAngle, float sweepAngle)
        {
            throw new NotSupportedException("DrawArc is not supported by DirectXRenderer");
        }

        /// <summary>
        ///     Draws a Polygon.
        /// </summary>
        /// <param name="pen">The Pen.</param>
        /// <param name="polygon">The Polygon.</param>
        public override void DrawPolygon(Pen pen, Polygon polygon)
        {
            var dxPen = pen.Instance as DirectXPen;
            if (dxPen == null) throw new ArgumentException("DirectXRenderer expects a DirectXPen as resource.");

            var geometry = new PathGeometry(DirectXHelper.D2DFactory);
            using (GeometrySink sink = geometry.Open())
            {
                sink.BeginFigure(DirectXHelper.ConvertVector(polygon.Points[0]), FigureBegin.Hollow);

                for (int i = 1; i < polygon.Points.Length; i++)
                    sink.AddLine(DirectXHelper.ConvertVector(polygon.Points[i]));

                sink.EndFigure(FigureEnd.Closed);
                sink.Close();

                DirectXHelper.RenderTarget.DrawGeometry(geometry, dxPen.GetPen(), dxPen.Width);
            }

            geometry.Dispose();
        }

        /// <summary>
        ///     Fills a Rectangle.
        /// </summary>
        /// <param name="color">The Color.</param>
        /// <param name="rectangle">The Rectangle.</param>
        public override void FillRectangle(Color color, Rectangle rectangle)
        {
            DirectXHelper.RenderTarget.FillRectangle(DirectXHelper.ConvertRectangle(rectangle),
                new SolidColorBrush(DirectXHelper.RenderTarget, DirectXHelper.ConvertColor(color)));
        }

        /// <summary>
        ///     Fills a Ellipse.
        /// </summary>
        /// <param name="color">The Color.</param>
        /// <param name="ellipse">The Ellipse.</param>
        public override void FillEllipse(Color color, Ellipse ellipse)
        {
            DirectXHelper.RenderTarget.FillEllipse(DirectXHelper.ConvertEllipse(ellipse),
                new SolidColorBrush(DirectXHelper.RenderTarget, DirectXHelper.ConvertColor(color)));
        }

        /// <summary>
        ///     Fills a Polygon.
        /// </summary>
        /// <param name="color">The Color.</param>
        /// <param name="polygon">The Polygon.</param>
        public override void FillPolygon(Color color, Polygon polygon)
        {
            var geometry = new PathGeometry(DirectXHelper.D2DFactory);
            using (GeometrySink sink = geometry.Open())
            {
                sink.BeginFigure(DirectXHelper.ConvertVector(polygon.Points[0]), FigureBegin.Filled);

                for (int i = 1; i < polygon.Points.Length; i++)
                    sink.AddLine(DirectXHelper.ConvertVector(polygon.Points[i]));

                sink.EndFigure(FigureEnd.Closed);
                sink.Close();

                DirectXHelper.RenderTarget.FillGeometry(geometry,
                    new SolidColorBrush(DirectXHelper.RenderTarget, DirectXHelper.ConvertColor(color)));
            }

            geometry.Dispose();
        }
    }
}