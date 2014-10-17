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
using Sharpex2D.Content.Pipeline;
using Sharpex2D.Content.Pipeline.Processors;
using Sharpex2D.Math;
using SlimDX;
using SlimDX.Direct2D;
using SlimDX.Direct3D10;
using SlimDX.DirectWrite;
using SlimDX.DXGI;
using Bitmap = SlimDX.Direct2D.Bitmap;
using Device1 = SlimDX.Direct3D10_1.Device1;
using Ellipse = Sharpex2D.Math.Ellipse;
using Factory = SlimDX.DirectWrite.Factory;
using FactoryType = SlimDX.DirectWrite.FactoryType;
using FeatureLevel = SlimDX.Direct3D10_1.FeatureLevel;
using Rectangle = Sharpex2D.Math.Rectangle;
using Vector2 = Sharpex2D.Math.Vector2;

namespace Sharpex2D.Rendering.DirectX10
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Tested)]
    public class DirectXGraphics : IGraphics
    {
        private readonly GraphicsDevice _graphicsDevice;
        private Device1 _direct3D10Device;
        private RenderTarget _renderTarget;
        private SwapChain _swapChain;

        /// <summary>
        /// Initializes a new DirectXGraphics class.
        /// </summary>
        public DirectXGraphics()
        {
            ResourceManager = new DirectXResourceManager();
            ContentProcessors = new IContentProcessor[]
            {new DirectXFontContentProcessor(), new DirectXPenContentProcessor(), new DirectXTextureContentProcessor()};
            SmoothingMode = SmoothingMode.AntiAlias;
            InterpolationMode = InterpolationMode.Linear;

            _graphicsDevice = SGL.QueryComponents<GraphicsDevice>();

            DirectXHelper.DirectWriteFactory = new Factory(FactoryType.Shared);
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
            var descMode = new ModeDescription
            {
                Width = _graphicsDevice.BackBuffer.Width,
                Height = _graphicsDevice.BackBuffer.Height,
                Format = Format.R8G8B8A8_UNorm,
                RefreshRate = new Rational(60, 1),
                Scaling = DisplayModeScaling.Stretched
            };

            var descSwap = new SwapChainDescription
            {
                BufferCount = 1,
                ModeDescription = descMode,
                Usage = Usage.RenderTargetOutput,
                OutputHandle = _graphicsDevice.RenderTarget.Handle,
                SampleDescription = new SampleDescription(1, 0),
                IsWindowed = true,
                SwapEffect = SwapEffect.Discard,
                Flags = SwapChainFlags.None,
            };


            Result creationResult = Device1.CreateWithSwapChain(null, DriverType.Hardware,
                DeviceCreationFlags.BgraSupport, FeatureLevel.Level_10_0, descSwap,
                out _direct3D10Device, out _swapChain);

            if (creationResult.IsFailure)
            {
                throw new GraphicsException("DirectX10 is not supported on the current platform.");
            }

            SlimDX.DXGI.Surface backBuffer = SlimDX.DXGI.Surface.FromSwapChain(_swapChain, 0);
            var d2DFactory = new SlimDX.Direct2D.Factory(SlimDX.Direct2D.FactoryType.Multithreaded);

            RenderTarget renderTarget = RenderTarget.FromDXGI(d2DFactory, backBuffer, new RenderTargetProperties
            {
                HorizontalDpi = 96,
                VerticalDpi = 96,
                MinimumFeatureLevel = SlimDX.Direct2D.FeatureLevel.Default,
                PixelFormat = new PixelFormat(Format.R8G8B8A8_UNorm, AlphaMode.Premultiplied),
                Type = RenderTargetType.Hardware,
                Usage = RenderTargetUsage.None
            });
            renderTarget.AntialiasMode = SmoothingMode == SmoothingMode.AntiAlias
                ? AntialiasMode.Aliased
                : AntialiasMode.PerPrimitive;
            renderTarget.TextAntialiasMode = TextAntialiasMode.ClearType;

            _renderTarget = renderTarget;
            DirectXHelper.RenderTarget = _renderTarget;
            DirectXHelper.Direct2DFactory = d2DFactory;
        }

        /// <summary>
        /// Begins the draw operation.
        /// </summary>
        public void Begin()
        {
            _renderTarget.BeginDraw();
            _renderTarget.Transform = Matrix3x2.Identity;
            _renderTarget.Clear(DirectXHelper.ConvertColor(_graphicsDevice.ClearColor));
        }

        /// <summary>
        /// Ends the draw operation.
        /// </summary>
        public void End()
        {
            _renderTarget.EndDraw();
            _swapChain.Present(0, PresentFlags.None);
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
            var dxFont = font.Instance as DirectXFont;
            if (dxFont == null) throw new ArgumentException("DirectX10 expects a DirectXFont as resource.");

            _renderTarget.DrawText(text, dxFont.GetFont(),
                new RectangleF(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height),
                DirectXHelper.ConvertSolidColorBrush(color));
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
            var dxFont = font.Instance as DirectXFont;
            if (dxFont == null) throw new ArgumentException("DirectX10 expects a DirectXFont as resource.");

            _renderTarget.DrawText(text, dxFont.GetFont(),
                new RectangleF(position.X, position.Y, 9999, 9999), DirectXHelper.ConvertSolidColorBrush(color));
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
            var dxTexture = texture as DirectXTexture;
            if (dxTexture == null) throw new ArgumentException("DirectX10 expects a DirectXTexture as resource.");
            Bitmap dxBmp = dxTexture.GetBitmap();
            _renderTarget.DrawBitmap(dxBmp,
                new RectangleF(position.X, position.Y, texture.Width, texture.Height), opacity,
                InterpolationMode == InterpolationMode.Linear
                    ? SlimDX.Direct2D.InterpolationMode.Linear
                    : SlimDX.Direct2D.InterpolationMode.NearestNeighbor);
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
            var dxTexture = texture as DirectXTexture;
            if (dxTexture == null) throw new ArgumentException("DirectX10 expects a DirectXTexture as resource.");
            Bitmap dxBmp = dxTexture.GetBitmap();
            _renderTarget.DrawBitmap(dxBmp, DirectXHelper.ConvertRectangle(rectangle), opacity,
                InterpolationMode == InterpolationMode.Linear
                    ? SlimDX.Direct2D.InterpolationMode.Linear
                    : SlimDX.Direct2D.InterpolationMode.NearestNeighbor);
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
            var dxTexture = spriteSheet.Texture2D as DirectXTexture;
            if (dxTexture == null) throw new ArgumentException("DirectX10 expects a DirectXTexture as resource.");
            Bitmap dxBmp = dxTexture.GetBitmap();

            DirectXHelper.RenderTarget.DrawBitmap(dxBmp,
                new RectangleF(position.X, position.Y, spriteSheet.Rectangle.Width, spriteSheet.Rectangle.Height),
                opacity,
                InterpolationMode == InterpolationMode.Linear
                    ? SlimDX.Direct2D.InterpolationMode.Linear
                    : SlimDX.Direct2D.InterpolationMode.NearestNeighbor,
                DirectXHelper.ConvertRectangleF(spriteSheet.Rectangle));
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
            var dxTexture = spriteSheet.Texture2D as DirectXTexture;
            if (dxTexture == null) throw new ArgumentException("DirectX10 expects a DirectXTexture as resource.");
            Bitmap dxBmp = dxTexture.GetBitmap();

            DirectXHelper.RenderTarget.DrawBitmap(dxBmp,
                new RectangleF(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height), opacity,
                InterpolationMode == InterpolationMode.Linear
                    ? SlimDX.Direct2D.InterpolationMode.Linear
                    : SlimDX.Direct2D.InterpolationMode.NearestNeighbor,
                DirectXHelper.ConvertRectangleF(spriteSheet.Rectangle));
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
            var dxTexture = texture as DirectXTexture;
            if (dxTexture == null) throw new ArgumentException("DirectX10 expects a DirectXTexture as resource.");
            Bitmap dxBmp = dxTexture.GetBitmap();

            DirectXHelper.RenderTarget.DrawBitmap(dxBmp, DirectXHelper.ConvertRectangleF(destination), opacity,
                InterpolationMode == InterpolationMode.Linear
                    ? SlimDX.Direct2D.InterpolationMode.Linear
                    : SlimDX.Direct2D.InterpolationMode.NearestNeighbor, DirectXHelper.ConvertRectangleF(source));
        }

        /// <summary>
        /// Measures the string.
        /// </summary>
        /// <param name="text">The String.</param>
        /// <param name="font">The Font.</param>
        /// <returns>Vector2.</returns>
        public Vector2 MeasureString(string text, Font font)
        {
            var dxFont = font.Instance as DirectXFont;
            if (dxFont == null) throw new ArgumentException("DirectX10 expects a DirectXFont as resource.");

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
        /// Sets the Transform.
        /// </summary>
        /// <param name="matrix">The Matrix.</param>
        public void SetTransform(Matrix2x3 matrix)
        {
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
            DirectXHelper.RenderTarget.Transform = Matrix3x2.Identity;
        }

        /// <summary>
        /// Draws a Rectangle.
        /// </summary>
        /// <param name="pen">The Pen.</param>
        /// <param name="rectangle">The Rectangle.</param>
        public void DrawRectangle(Pen pen, Rectangle rectangle)
        {
            var dxPen = pen.Instance as DirectXPen;
            if (dxPen == null) throw new ArgumentException("DirectX10 expects a DirectXPen as resource.");

            _renderTarget.DrawRectangle(dxPen.GetPen(), DirectXHelper.ConvertRectangle(rectangle));
        }

        /// <summary>
        /// Draws a Line between two points.
        /// </summary>
        /// <param name="pen">The Pen.</param>
        /// <param name="start">The Startpoint.</param>
        /// <param name="target">The Targetpoint.</param>
        public void DrawLine(Pen pen, Vector2 start, Vector2 target)
        {
            var dxPen = pen.Instance as DirectXPen;
            if (dxPen == null) throw new ArgumentException("DirectX10 expects a DirectXPen as resource.");

            _renderTarget.DrawLine(dxPen.GetPen(), DirectXHelper.ConvertPointF(start),
                DirectXHelper.ConvertPointF(target));
        }

        /// <summary>
        /// Draws a Ellipse.
        /// </summary>
        /// <param name="pen">The Pen.</param>
        /// <param name="ellipse">The Ellipse.</param>
        public void DrawEllipse(Pen pen, Ellipse ellipse)
        {
            var dxPen = pen.Instance as DirectXPen;
            if (dxPen == null) throw new ArgumentException("DirectX10 expects a DirectXPen as resource.");

            _renderTarget.DrawEllipse(dxPen.GetPen(), DirectXHelper.ConvertEllipse(ellipse));
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
            throw new NotSupportedException("DrawArc is not supported by DirectX10.");
        }

        /// <summary>
        /// Draws a Polygon.
        /// </summary>
        /// <param name="pen">The Pen.</param>
        /// <param name="polygon">The Polygon.</param>
        public void DrawPolygon(Pen pen, Polygon polygon)
        {
            var dxPen = pen.Instance as DirectXPen;
            if (dxPen == null) throw new ArgumentException("DirectX10 expects a DirectXPen as resource.");

            var geometry = new PathGeometry(DirectXHelper.Direct2DFactory);
            using (GeometrySink sink = geometry.Open())
            {
                sink.BeginFigure(DirectXHelper.ConvertVector(polygon.Points[0]), FigureBegin.Hollow);

                for (int i = 1; i < polygon.Points.Length; i++)
                    sink.AddLine(DirectXHelper.ConvertVector(polygon.Points[i]));

                sink.EndFigure(FigureEnd.Closed);
                sink.Close();

                _renderTarget.DrawGeometry(geometry, dxPen.GetPen(), dxPen.Width);
            }

            geometry.Dispose();
        }

        /// <summary>
        /// Fills a Rectangle.
        /// </summary>
        /// <param name="color">The Color.</param>
        /// <param name="rectangle">The Rectangle.</param>
        public void FillRectangle(Color color, Rectangle rectangle)
        {
            _renderTarget.FillRectangle(new SolidColorBrush(_renderTarget, DirectXHelper.ConvertColor(color)),
                DirectXHelper.ConvertRectangle(rectangle));
        }

        /// <summary>
        /// Fills a Ellipse.
        /// </summary>
        /// <param name="color">The Color.</param>
        /// <param name="ellipse">The Ellipse.</param>
        public void FillEllipse(Color color, Ellipse ellipse)
        {
            _renderTarget.FillEllipse(new SolidColorBrush(_renderTarget, DirectXHelper.ConvertColor(color)),
                DirectXHelper.ConvertEllipse(ellipse));
        }

        /// <summary>
        /// Fills a Polygon.
        /// </summary>
        /// <param name="color">The Color.</param>
        /// <param name="polygon">The Polygon.</param>
        public void FillPolygon(Color color, Polygon polygon)
        {
            var geometry = new PathGeometry(DirectXHelper.Direct2DFactory);
            using (GeometrySink sink = geometry.Open())
            {
                sink.BeginFigure(DirectXHelper.ConvertVector(polygon.Points[0]), FigureBegin.Filled);

                for (int i = 1; i < polygon.Points.Length; i++)
                    sink.AddLine(DirectXHelper.ConvertVector(polygon.Points[i]));

                sink.EndFigure(FigureEnd.Closed);
                sink.Close();

                _renderTarget.FillGeometry(geometry,
                    new SolidColorBrush(DirectXHelper.RenderTarget, DirectXHelper.ConvertColor(color)));
            }

            geometry.Dispose();
        }
    }
}