using System;
using System.Collections.Generic;
using Sharpex2D.Framework.Content;
using Sharpex2D.Framework.Content.Serialization;
using Sharpex2D.Framework.Math;
using Sharpex2D.Framework.Rendering.DirectX9.Font;
using Sharpex2D.Framework.Rendering.Font;
using SlimDX.Direct3D9;
using Vector2 = Sharpex2D.Framework.Math.Vector2;

namespace Sharpex2D.Framework.Rendering.DirectX9
{
    public class DirectXRenderer : IRenderer
    {
        #region IComponent Implementation
        /// <summary>
        /// Gets the Guid.
        /// </summary>
        public Guid Guid { get { return new Guid("5F2099E6-8A53-43BA-9EB7-C9F6D69C81CF"); } }
        #endregion

        private Device _direct3D9Device;
        private Direct3D _direct3D;
        private Sprite _sprite;

        #region IConstructable Implementation
        /// <summary>
        /// Constructs the component.
        /// </summary>
        public void Construct()
        {
            GraphicsDevice.ClearColor = Color.CornflowerBlue;

            _direct3D = new Direct3D();
            var primaryAdaptor = _direct3D.Adapters.DefaultAdapter;

            var presentationParameters = new PresentParameters
            {
                BackBufferCount = 1,
                BackBufferWidth = GraphicsDevice.DisplayMode.Width,
                BackBufferHeight = GraphicsDevice.DisplayMode.Height,
                DeviceWindowHandle = GraphicsDevice.RenderTarget.Handle,
                SwapEffect = SwapEffect.Discard,
                Windowed = true,
                BackBufferFormat = Format.A8R8G8B8,
            };

            SetHighestMultisampleType(presentationParameters);

            presentationParameters.PresentationInterval = VSync ? PresentInterval.One : PresentInterval.Immediate;

            _direct3D9Device = new Device(_direct3D, primaryAdaptor.Adapter, DeviceType.Hardware, GraphicsDevice.RenderTarget.Handle,
                CreateFlags.HardwareVertexProcessing, presentationParameters);
            _direct3D9Device.SetRenderState(RenderState.MultisampleAntialias, true);

            DirectXHelper.Direct3D9 = _direct3D9Device;

            _sprite = new Sprite(_direct3D9Device);
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

            var line = new Line(_direct3D9Device) { Antialias = true, Width = dxPen.Width };
            line.Begin();

            line.Draw(
                DirectXHelper.ConvertToVertex(new Vector2(0, 100),
                    new Vector2(100, 100)), DirectXHelper.ConvertColor(dxPen.Color));

            line.Draw(
                DirectXHelper.ConvertToVertex(new Vector2(rectangle.X, rectangle.Y + rectangle.Height),
                    new Vector2(rectangle.X + rectangle.Width, rectangle.Y + rectangle.Height)), DirectXHelper.ConvertColor(dxPen.Color));

            line.Draw(
                DirectXHelper.ConvertToVertex(new Vector2(rectangle.X, rectangle.Y),
                    new Vector2(rectangle.X, rectangle.Y + rectangle.Height)),
                DirectXHelper.ConvertColor(dxPen.Color));

            line.Draw(
                DirectXHelper.ConvertToVertex(new Vector2(rectangle.X + rectangle.Width, rectangle.Y),
                    new Vector2(rectangle.X + rectangle.Width, rectangle.Y + rectangle.Height)),
                DirectXHelper.ConvertColor(dxPen.Color));

            line.End();
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

            var line = new Line(_direct3D9Device) {Antialias = true, Width = dxPen.Width};
            line.Begin();
            line.Draw(DirectXHelper.ConvertToVertex(start, target), DirectXHelper.ConvertColor(dxPen.Color));
            line.End();
        }

        /// <summary>
        /// Draws a Ellipse.
        /// </summary>
        /// <param name="pen">The Pen.</param>
        /// <param name="rectangle">The Rectangle.</param>
        /// <remarks>Different radii are not in calculation.</remarks>
        public void DrawEllipse(IPen pen, Rectangle rectangle)
        {
            CheckDisposed();

            var dxPen = pen as DirectXPen;
            if (dxPen == null) throw new ArgumentException("DirectXRenderer expects a DirectXPen as resource.");

            var whalfellipse = rectangle.Width/2;
            var hhalfellipse = rectangle.Height / 2;

            var vertexList = new List<Vector2>();

            for (var i = 1; i <= 360; i++)
            {
                vertexList.Add(
                    new Vector2(whalfellipse*MathHelper.Cos(i*(float)MathHelper.PiOverOneEighty) + rectangle.Center.X,
                        hhalfellipse * MathHelper.Sin(i * (float)MathHelper.PiOverOneEighty) + rectangle.Center.Y));
            }


            var line = new Line(_direct3D9Device) {Antialias = true, Width = dxPen.Width};
            line.Begin();

            line.Draw(DirectXHelper.ConvertToVertex(vertexList.ToArray()), DirectXHelper.ConvertColor(dxPen.Color));

            line.End();
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

            var line = new Line(_direct3D9Device) { Antialias = true, Width = dxPen.Width };
            line.Begin();
            line.Draw(DirectXHelper.ConvertToVertex(points), DirectXHelper.ConvertColor(dxPen.Color));
            line.End();
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

            var line = new Line(_direct3D9Device) { Antialias = true, Width = rectangle.Height };
            line.Begin();
            line.Draw(
                DirectXHelper.ConvertToVertex(new Vector2(rectangle.X, rectangle.Center.Y),
                    new Vector2(rectangle.X + rectangle.Width, rectangle.Center.Y)),
                DirectXHelper.ConvertColor(color));
            line.End();
        }
        /// <summary>
        /// Fills a Ellipse.
        /// </summary>
        /// <param name="color">The Color.</param>
        /// <param name="rectangle">The Rectangle.</param>
        public void FillEllipse(Color color, Rectangle rectangle)
        {
            CheckDisposed();

            var whalfellipse = rectangle.Width / 2;
            var hhalfellipse = rectangle.Height / 2;

            var vertexList = new List<Vector2>();

            while (whalfellipse > 0 || hhalfellipse > 0)
            {
                for (var i = 1; i <= 360; i++)
                {
                    vertexList.Add(
                        new Vector2(
                            whalfellipse*MathHelper.Cos(i*(float) MathHelper.PiOverOneEighty) + rectangle.Center.X,
                            hhalfellipse*MathHelper.Sin(i*(float) MathHelper.PiOverOneEighty) + rectangle.Center.Y));
                }
                whalfellipse -= 1f;
                hhalfellipse -= 1f;
            }


            var line = new Line(_direct3D9Device) { Antialias = false, Width = 2 };
            line.Begin();

            line.Draw(DirectXHelper.ConvertToVertex(vertexList.ToArray()), DirectXHelper.ConvertColor(color));

            line.End();
        }
        /// <summary>
        /// Fills a Polygon.
        /// </summary>
        /// <param name="color">The Color.</param>
        /// <param name="points">The Points.</param>
        public void FillPolygon(Color color, Vector2[] points)
        {
            CheckDisposed();

            throw new NotSupportedException("FillPolygon is not supported by DirectXRenderer");
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
            if (!IsDisposed)
            {
                IsDisposed = true;
                _direct3D.Dispose();
                _direct3D9Device.Dispose();
            }
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
            _direct3D9Device.BeginScene();
            _direct3D9Device.Clear(ClearFlags.Target, DirectXHelper.ConvertColor(GraphicsDevice.ClearColor), 0, 0);
            _sprite.Transform = SlimDX.Matrix.Identity;
            _sprite.Begin(SpriteFlags.AlphaBlend);
        }
        /// <summary>
        /// Flushes the buffer.
        /// </summary>
        public void Close()
        {
            _sprite.End();
            _direct3D9Device.EndScene();
            _direct3D9Device.Present();
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

            dxFont.GetFont()
                .DrawString(_sprite, text, DirectXHelper.ConvertToWinRectangle(rectangle), DrawTextFormat.WordBreak,
                    DirectXHelper.ConvertColor(color));
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

            dxFont.GetFont().DrawString(_sprite, text, (int)position.X, (int)position.Y, DirectXHelper.ConvertColor(color));
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

            _sprite.Draw(dxTexture.GetTexture(), null, DirectXHelper.ConvertVector2(position),
                DirectXHelper.ConvertColor(color));
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

            //calc percentages for scaling

            var scaleX = rectangle.Width/texture.Width;
            var scaleY = rectangle.Height/texture.Height;

            _sprite.Transform = SlimDX.Matrix.Scaling(scaleX, scaleY, 1f);

            _sprite.Draw(dxTexture.GetTexture(), null, DirectXHelper.ConvertVector2(new Vector2(rectangle.X, rectangle.Y)),
                DirectXHelper.ConvertColor(color));

            _sprite.Transform = SlimDX.Matrix.Identity;
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

            var result = dxFont.GetFont().MeasureString(_sprite, text, DrawTextFormat.WordBreak);

            return new Vector2(result.Width, result.Width);
        }

        /// <summary>
        /// Sets the Transform.
        /// </summary>
        /// <param name="matrix">The Matrix.</param>
        public void SetTransform(Matrix2x3 matrix)
        {
            var m = SlimDX.Matrix.Identity;

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
        /// Resets the Transform.
        /// </summary>
        public void ResetTransform()
        {
            _sprite.Transform = SlimDX.Matrix.Identity;
        }

        #endregion

        /// <summary>
        /// Initializes a new DirectXRenderer class.
        /// </summary>
        public DirectXRenderer()
        {
            SGL.Components.Get<Implementation.ImplementationManager>().AddImplementation(new DirectX9TextureSerializer());
            SGL.Components.Get<ContentManager>().Extend(new DirectX9TextureLoader());
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
        /// Sets the highest MultisampleType.
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
            var qualityLevel = 0;

            foreach (var sampleType in possibleMultisampleTypes)
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

        /// <summary>
        /// Deconstructs the DirectXRenderer.
        /// </summary>
        ~DirectXRenderer()
        {
            Dispose();
        }
    }
}
