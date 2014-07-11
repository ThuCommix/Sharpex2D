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
using System.Collections.Generic;
using Sharpex2D.Content.Pipeline;
using Sharpex2D.Content.Pipeline.Processors;
using Sharpex2D.Math;
using SlimDX.Direct3D9;
using Matrix = SlimDX.Matrix;

namespace Sharpex2D.Rendering.DirectX9
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Tested)]
    [Device("DirectX9 Device")]
    public class DirectXRenderDevice : RenderDevice
    {
        private readonly IContentProcessor[] _contentProcessors;
        private readonly InterpolationMode _interpolationMode;
        private readonly SmoothingMode _smoothingMode;
        private Direct3D _direct3D;
        private Device _direct3D9Device;
        private Sprite _sprite;

        /// <summary>
        ///     Initializes a new DirectXRenderDevice class.
        /// </summary>
        /// <param name="interpolationMode">The InterpolationMode.</param>
        /// <param name="smoothingMode">The SmoothingMode.</param>
        public DirectXRenderDevice(InterpolationMode interpolationMode, SmoothingMode smoothingMode)
            : base(new DirectXResourceManager(), new Guid("5F2099E6-8A53-43BA-9EB7-C9F6D69C81CF"))
        {
            _contentProcessors = new IContentProcessor[]
            {new DirectXFontContentProcessor(), new DirectXPenContentProcessor(), new DirectXTextureContentProcessor()};

            _interpolationMode = interpolationMode;
            _smoothingMode = smoothingMode;
        }

        /// <summary>
        ///     Initializes a new DirectXRenderDevice class.
        /// </summary>
        public DirectXRenderDevice()
            : base(new DirectXResourceManager(), new Guid("5F2099E6-8A53-43BA-9EB7-C9F6D69C81CF"))
        {
            _contentProcessors = new IContentProcessor[]
            {new DirectXFontContentProcessor(), new DirectXPenContentProcessor(), new DirectXTextureContentProcessor()};

            _interpolationMode = InterpolationMode.Linear;
            _smoothingMode = SmoothingMode.AntiAlias;
        }

        /// <summary>
        ///     Gets the required PlatformVersion.
        /// </summary>
        public override Version PlatformVersion
        {
            get { return new Version(5, 1); }
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
            GraphicsDevice.ClearColor = Color.CornflowerBlue;

            _direct3D = new Direct3D();
            AdapterInformation primaryAdaptor = _direct3D.Adapters.DefaultAdapter;

            var presentationParameters = new PresentParameters
            {
                BackBufferCount = 1,
                BackBufferWidth = GraphicsDevice.BackBuffer.Width,
                BackBufferHeight = GraphicsDevice.BackBuffer.Height,
                DeviceWindowHandle = GraphicsDevice.RenderTarget.Handle,
                SwapEffect = SwapEffect.Discard,
                Windowed = true,
                BackBufferFormat = Format.A8R8G8B8,
            };

            if (_smoothingMode == SmoothingMode.AntiAlias)
            {
                SetHighestMultisampleType(presentationParameters);
                _direct3D9Device.SetRenderState(RenderState.MultisampleAntialias, true);
            }

            presentationParameters.PresentationInterval = PresentInterval.Immediate;

            _direct3D9Device = new Device(_direct3D, primaryAdaptor.Adapter, DeviceType.Hardware,
                GraphicsDevice.RenderTarget.Handle,
                CreateFlags.HardwareVertexProcessing, presentationParameters);

            DirectXHelper.Direct3D9 = _direct3D9Device;

            _sprite = new Sprite(_direct3D9Device);
        }

        /// <summary>
        ///     Begins the draw operation.
        /// </summary>
        public override void Begin()
        {
            _direct3D9Device.BeginScene();
            _direct3D9Device.Clear(ClearFlags.Target, DirectXHelper.ConvertColor(GraphicsDevice.ClearColor), 0, 0);
            _sprite.Transform = Matrix.Identity;
            _sprite.Begin(SpriteFlags.AlphaBlend);
        }

        /// <summary>
        ///     Ends the draw operation.
        /// </summary>
        public override void End()
        {
            _sprite.End();
            _direct3D9Device.EndScene();
            _direct3D9Device.Present();
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

            dxFont.GetFont()
                .DrawString(_sprite, text, DirectXHelper.ConvertToWinRectangle(rectangle), DrawTextFormat.WordBreak,
                    DirectXHelper.ConvertColor(color));
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

            dxFont.GetFont()
                .DrawString(_sprite, text, (int) position.X, (int) position.Y, DirectXHelper.ConvertColor(color));
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

            _sprite.Draw(dxTexture.GetTexture(), null, DirectXHelper.ConvertVector2(position),
                DirectXHelper.ConvertColor(color));
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

            //calc percentages for scaling

            float scaleX = rectangle.Width/texture.Width;
            float scaleY = rectangle.Height/texture.Height;

            _sprite.Transform = Matrix.Scaling(scaleX, scaleY, 1f);

            _sprite.Draw(dxTexture.GetTexture(), null,
                DirectXHelper.ConvertVector2(new Vector2(rectangle.X/scaleX, rectangle.Y/scaleY)),
                DirectXHelper.ConvertColor(color));

            _sprite.Transform = Matrix.Identity;
        }

        /// <summary>
        ///     Draws a Texture.
        /// </summary>
        /// <param name="spriteSheet">The SpriteSheet.</param>
        /// <param name="position">The Position.</param>
        /// <param name="color">The Color.</param>
        /// <param name="opacity">The Opacity.</param>
        public override void DrawTexture(SpriteSheet spriteSheet, Vector2 position, Color color, float opacity = 1)
        {
            var dxTexture = spriteSheet.Texture2D as DirectXTexture;
            if (dxTexture == null) throw new ArgumentException("DirectXRenderer expects a DirectXTexture as resource.");

            _sprite.Draw(dxTexture.GetTexture(), DirectXHelper.ConvertToWinRectangle(spriteSheet.Rectangle), null,
                DirectXHelper.ConvertVector3(position), DirectXHelper.ConvertColor(color));
        }

        /// <summary>
        ///     Draws a Texture.
        /// </summary>
        /// <param name="spriteSheet">The SpriteSheet.</param>
        /// <param name="rectangle">The Rectangle.</param>
        /// <param name="color">The Color.</param>
        /// <param name="opacity">The Opacity.</param>
        public override void DrawTexture(SpriteSheet spriteSheet, Rectangle rectangle, Color color, float opacity = 1)
        {
            var dxTexture = spriteSheet.Texture2D as DirectXTexture;
            if (dxTexture == null) throw new ArgumentException("DirectXRenderer expects a DirectXTexture as resource.");

            //calc percentages for scaling

            float scaleX = rectangle.Width/spriteSheet.Rectangle.Width;
            float scaleY = rectangle.Height/spriteSheet.Rectangle.Height;

            _sprite.Transform = Matrix.Scaling(scaleX, scaleY, 1f);

            _sprite.Draw(dxTexture.GetTexture(), DirectXHelper.ConvertToWinRectangle(spriteSheet.Rectangle), null,
                DirectXHelper.ConvertVector3(new Vector2(rectangle.X/scaleX, rectangle.Y/scaleY)),
                DirectXHelper.ConvertColor(color));

            _sprite.Transform = Matrix.Identity;
        }

        /// <summary>
        ///     Draws a Texture.
        /// </summary>
        /// <param name="texture">The Texture.</param>
        /// <param name="source">The SourceRectangle.</param>
        /// <param name="destination">The DestinationRectangle.</param>
        /// <param name="color">The Color.</param>
        /// <param name="opacity">The Opacity.</param>
        public override void DrawTexture(Texture2D texture, Rectangle source, Rectangle destination, Color color,
            float opacity = 1)
        {
            var dxTexture = texture as DirectXTexture;
            if (dxTexture == null) throw new ArgumentException("DirectXRenderer expects a DirectXTexture as resource.");

            //calc percentages for scaling

            float scaleX = destination.Width/source.Width;
            float scaleY = destination.Height/source.Height;

            _sprite.Transform = Matrix.Scaling(scaleX, scaleY, 1f);

            _sprite.Draw(dxTexture.GetTexture(), DirectXHelper.ConvertToWinRectangle(source), null,
                DirectXHelper.ConvertVector3(new Vector2(destination.X/scaleX, destination.Y/scaleY)),
                DirectXHelper.ConvertColor(color));

            _sprite.Transform = Matrix.Identity;
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

            System.Drawing.Rectangle result = dxFont.GetFont().MeasureString(_sprite, text, DrawTextFormat.WordBreak);

            return new Vector2(result.Width, result.Width);
        }

        /// <summary>
        ///     Sets the Transform.
        /// </summary>
        /// <param name="matrix">The Matrix.</param>
        public override void SetTransform(Matrix2x3 matrix)
        {
            Matrix m = Matrix.Identity;

            m.M12 = matrix[1, 0];
            m.M21 = matrix[0, 1];
            m.M11 = matrix[0, 0];
            m.M22 = matrix[1, 1];
            m.M33 = 1f;
            m.M41 = matrix.OffsetX;
            m.M42 = matrix.OffsetY;
            m.M43 = 0;

            _sprite.Transform = m;
        }

        /// <summary>
        ///     Resets the Transform.
        /// </summary>
        public override void ResetTransform()
        {
            _sprite.Transform = Matrix.Identity;
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

            var line = new Line(_direct3D9Device) {Antialias = true, Width = dxPen.Width};
            line.Begin();

            line.Draw(
                DirectXHelper.ConvertToVertex(
                    new Vector2(rectangle.X, rectangle.Y),
                    new Vector2(rectangle.X + rectangle.Width, rectangle.Y),
                    new Vector2(rectangle.X, rectangle.Y + rectangle.Height),
                    new Vector2(rectangle.X + rectangle.Width, rectangle.Y + rectangle.Height),
                    new Vector2(rectangle.X, rectangle.Y),
                    new Vector2(rectangle.X, rectangle.Y + rectangle.Height),
                    new Vector2(rectangle.X + rectangle.Width, rectangle.Y),
                    new Vector2(rectangle.X + rectangle.Width, rectangle.Y + rectangle.Height)),
                DirectXHelper.ConvertColor(dxPen.Color));

            line.End();
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

            var line = new Line(_direct3D9Device) {Antialias = true, Width = dxPen.Width};
            line.Begin();
            line.Draw(DirectXHelper.ConvertToVertex(start, target), DirectXHelper.ConvertColor(dxPen.Color));
            line.End();
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

            var line = new Line(_direct3D9Device) {Antialias = true, Width = dxPen.Width};
            line.Begin();

            line.Draw(DirectXHelper.ConvertToVertex(ellipse.Points), DirectXHelper.ConvertColor(dxPen.Color));

            line.End();
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

            var line = new Line(_direct3D9Device) {Antialias = true, Width = dxPen.Width};
            line.Begin();
            line.Draw(DirectXHelper.ConvertToVertex(polygon.Points), DirectXHelper.ConvertColor(dxPen.Color));
            line.End();
        }

        /// <summary>
        ///     Fills a Rectangle.
        /// </summary>
        /// <param name="color">The Color.</param>
        /// <param name="rectangle">The Rectangle.</param>
        public override void FillRectangle(Color color, Rectangle rectangle)
        {
            var line = new Line(_direct3D9Device) {Antialias = true, Width = rectangle.Height};
            line.Begin();
            line.Draw(
                DirectXHelper.ConvertToVertex(new Vector2(rectangle.X, rectangle.Center.Y),
                    new Vector2(rectangle.X + rectangle.Width, rectangle.Center.Y)),
                DirectXHelper.ConvertColor(color));
            line.End();
        }

        /// <summary>
        ///     Fills a Ellipse.
        /// </summary>
        /// <param name="color">The Color.</param>
        /// <param name="ellipse">The Ellipse.</param>
        public override void FillEllipse(Color color, Ellipse ellipse)
        {
            float whalfellipse = ellipse.RadiusX;
            float hhalfellipse = ellipse.RadiusY;

            var vertexList = new List<Vector2>();

            while (whalfellipse > 0 || hhalfellipse > 0)
            {
                for (int i = 1; i <= 360; i++)
                {
                    vertexList.Add(
                        new Vector2(
                            whalfellipse*MathHelper.Cos(i*(float) MathHelper.PiOverOneEighty) + ellipse.Position.X,
                            hhalfellipse*MathHelper.Sin(i*(float) MathHelper.PiOverOneEighty) + ellipse.Position.Y));
                }
                whalfellipse -= 1f;
                hhalfellipse -= 1f;
            }


            var line = new Line(_direct3D9Device) {Antialias = false, Width = 2};
            line.Begin();

            line.Draw(DirectXHelper.ConvertToVertex(vertexList.ToArray()), DirectXHelper.ConvertColor(color));

            line.End();
        }

        /// <summary>
        ///     Fills a Polygon.
        /// </summary>
        /// <param name="color">The Color.</param>
        /// <param name="polygon">The Polygon.</param>
        public override void FillPolygon(Color color, Polygon polygon)
        {
            throw new NotSupportedException("FillPolygon is not supported by DirectXRenderer");
        }

        /// <summary>
        ///     Sets the highest MultisampleType.
        /// </summary>
        /// <param name="presentParameters">The PresentParameters.</param>
        private void SetHighestMultisampleType(PresentParameters presentParameters)
        {
            var possibleMultisampleTypes = new List<MultisampleType>
            {
                MultisampleType.None,
                MultisampleType.NonMaskable,
                MultisampleType.TwoSamples,
                MultisampleType.ThreeSamples,
                MultisampleType.FourSamples,
                MultisampleType.FiveSamples,
                MultisampleType.SixSamples,
                MultisampleType.SevenSamples,
                MultisampleType.EightSamples,
                MultisampleType.NineSamples,
                MultisampleType.TenSamples,
                MultisampleType.ElevenSamples,
                MultisampleType.TwelveSamples,
                MultisampleType.ThirteenSamples,
                MultisampleType.FourteenSamples,
                MultisampleType.FifteenSamples,
                MultisampleType.SixteenSamples
            };

            var highestSample = MultisampleType.None;
            int qualityLevel = 0;

            foreach (MultisampleType sampleType in possibleMultisampleTypes)
            {
                if (_direct3D.CheckDeviceMultisampleType(_direct3D.Adapters.DefaultAdapter.Adapter, DeviceType.Hardware,
                    Format.A8R8G8B8, true, sampleType, out qualityLevel))
                {
                    highestSample = sampleType;
                }
            }

            presentParameters.Multisample = highestSample;
            presentParameters.MultisampleQuality = qualityLevel;
        }
    }
}