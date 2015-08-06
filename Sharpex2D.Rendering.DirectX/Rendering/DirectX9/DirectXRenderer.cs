// Copyright (c) 2012-2015 Sharpex2D - Kevin Scholz (ThuCommix)
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
using System.IO;
using SharpDX;
using SharpDX.Direct3D9;

namespace Sharpex2D.Framework.Rendering.DirectX9
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Tested)]
    internal class DirectXRenderer : IRenderer
    {
        private readonly List<ITexture> _textures;
        private Direct3D _direct3D;
        private GraphicsDevice _graphicsDevice;
        private Sprite _sprite;

        /// <summary>
        /// Initializes a new DirectXRenderer class.
        /// </summary>
        public DirectXRenderer()
        {
            _textures = new List<ITexture>();
        }

        /// <summary>
        /// Gets the current Device.
        /// </summary>
        public static Device CurrentDevice { get; private set; }


        /// <summary>
        /// Initializes the renderer.
        /// </summary>
        /// <param name="game">The Game.</param>
        public void Initialize(Game game)
        {
            _graphicsDevice = game.Get<GraphicsDevice>();
            _direct3D = new Direct3D();
            AdapterInformation primaryAdaptor = _direct3D.Adapters[0];

            var presentationParameters = new PresentParameters
            {
                BackBufferCount = 1,
                BackBufferWidth = _graphicsDevice.GraphicsManager.PreferredBackBufferWidth,
                BackBufferHeight = _graphicsDevice.GraphicsManager.PreferredBackBufferHeight,
                DeviceWindowHandle = _graphicsDevice.GameWindow.Handle,
                SwapEffect = SwapEffect.Discard,
                Windowed = true,
                BackBufferFormat = Format.A8R8G8B8,
                PresentationInterval = PresentInterval.Immediate,
            };

            CurrentDevice = new Device(_direct3D, primaryAdaptor.Adapter, DeviceType.Hardware,
                _graphicsDevice.GameWindow.Handle,
                CreateFlags.HardwareVertexProcessing, presentationParameters);
            CurrentDevice.SetRenderState(RenderState.MultisampleAntialias, true);
            CurrentDevice.SetRenderState(RenderState.AlphaBlendEnable, true);
            CurrentDevice.SetRenderState(RenderState.AlphaFunc, Compare.Less);
            CurrentDevice.SetRenderState(RenderState.SourceBlend, Blend.SourceAlpha);
            _sprite = new Sprite(CurrentDevice);
        }

        /// <summary>
        /// Clears the buffer.
        /// </summary>
        public void Clear()
        {
            CurrentDevice.BeginScene();
            CurrentDevice.Clear(ClearFlags.Target, DirectXHelper.ConvertColor(_graphicsDevice.ClearColor), 0, 0);
            _sprite.Transform = Matrix.Identity;
            _sprite.Begin(SpriteFlags.AlphaBlend);
        }

        /// <summary>
        /// Draws a range of textures.
        /// </summary>
        /// <param name="drawOperations">The DrawOperations.</param>
        public void DrawTextures(IEnumerable<DrawOperation> drawOperations)
        {
            foreach (var operation in drawOperations)
            {
                //Already buffered by the driver, so we can safely call the draw texture. As in later implementations
                //we draw all textures in this method using and regenerating the vertex buffer
                DrawTexture(operation.Texture, operation.Source, operation.Destination, operation.Color,
                    operation.Opacity);
            }
        }

        /// <summary>
        /// Presents the buffer.
        /// </summary>
        public void Present()
        {
            _sprite.End();
            CurrentDevice.EndScene();
            CurrentDevice.Present();
        }

        /// <summary>
        /// Draws a Texture.
        /// </summary>
        /// <param name="texture">The Texture.</param>
        /// <param name="position">The Position.</param>
        /// <param name="opacity">The Opacity.</param>
        /// <param name="color">The Color.</param>
        public void DrawTexture(ITexture texture, Vector2 position, Color color, float opacity = 1)
        {
            var dxTexture = texture as DirectXTexture;
            if (dxTexture == null) throw new ArgumentException("Expected a DirectXTexture as resource.");

            color.A = (byte) (opacity*255);

            _sprite.Draw(dxTexture.InternalTexture, DirectXHelper.ConvertColor(color), null, null,
                DirectXHelper.ConvertVector2(position));
        }

        /// <summary>
        /// Draws a Texture.
        /// </summary>
        /// <param name="texture">The Texture.</param>
        /// <param name="rectangle">The Rectangle.</param>
        /// <param name="opacity">The Opacity.</param>
        /// <param name="color">The Color.</param>
        public void DrawTexture(ITexture texture, Rectangle rectangle, Color color, float opacity = 1)
        {
            var dxTexture = texture as DirectXTexture;
            if (dxTexture == null) throw new ArgumentException("Expected a DirectXTexture as resource.");

            float scaleX = rectangle.Width/texture.Width;
            float scaleY = rectangle.Height/texture.Height;

            _sprite.Transform = Matrix.Scaling(scaleX, scaleY, 1f);
            color.A = (byte) (opacity*255);

            _sprite.Draw(dxTexture.InternalTexture, DirectXHelper.ConvertColor(color), null, null,
                new Vector3(rectangle.X, rectangle.Y, 0));

            _sprite.Transform = Matrix.Identity;
        }

        /// <summary>
        /// Draws a Texture.
        /// </summary>
        /// <param name="texture">The Texture.</param>
        /// <param name="spriteSheet">The SpriteSheet.</param>
        /// <param name="position">The Position.</param>
        /// <param name="color">The Color.</param>
        /// <param name="opacity">The Opacity.</param>
        public void DrawTexture(ITexture texture, SpriteSheet spriteSheet, Vector2 position, Color color,
            float opacity = 1)
        {
            var dxTexture = texture as DirectXTexture;
            if (dxTexture == null) throw new ArgumentException("Expected a DirectXTexture as resource.");

            color.A = (byte) (opacity*255);

            _sprite.Draw(dxTexture.InternalTexture, DirectXHelper.ConvertColor(color),
                DirectXHelper.ConvertRectangle(spriteSheet.Rectangle), null, DirectXHelper.ConvertVector2(position));
        }

        /// <summary>
        /// Draws a Texture.
        /// </summary>
        /// <param name="texture">The Texture.</param>
        /// <param name="spriteSheet">The SpriteSheet.</param>
        /// <param name="rectangle">The Rectangle.</param>
        /// <param name="color">The Color.</param>
        /// <param name="opacity">The Opacity.</param>
        public void DrawTexture(ITexture texture, SpriteSheet spriteSheet, Rectangle rectangle, Color color,
            float opacity = 1)
        {
            var dxTexture = texture as DirectXTexture;
            if (dxTexture == null) throw new ArgumentException("Expected a DirectXTexture as resource.");

            float scaleX = rectangle.Width/spriteSheet.Rectangle.Width;
            float scaleY = rectangle.Height/spriteSheet.Rectangle.Height;

            _sprite.Transform = Matrix.Scaling(scaleX, scaleY, 1f);
            color.A = (byte) (opacity*255);

            _sprite.Draw(dxTexture.InternalTexture, DirectXHelper.ConvertColor(color),
                DirectXHelper.ConvertRectangle(spriteSheet.Rectangle), null,
                DirectXHelper.ConvertVector2(new Vector2(rectangle.X/scaleX, rectangle.Y/scaleY)));

            _sprite.Transform = Matrix.Identity;
        }

        /// <summary>
        /// Draws a Texture.
        /// </summary>
        /// <param name="texture">The Texture.</param>
        /// <param name="source">The SourceRectangle.</param>
        /// <param name="destination">The DestinationRectangle.</param>
        /// <param name="color">The Color.</param>
        /// <param name="opacity">The Opacity.</param>
        public void DrawTexture(ITexture texture, Rectangle source, Rectangle destination, Color color,
            float opacity = 1)
        {
            var dxTexture = texture as DirectXTexture;
            if (dxTexture == null) throw new ArgumentException("Expected a DirectXTexture as resource.");

            float scaleX = destination.Width/source.Width;
            float scaleY = destination.Height/source.Height;

            _sprite.Transform = Matrix.Scaling(scaleX, scaleY, 1f);
            color.A = (byte) (opacity*255);

            _sprite.Draw(dxTexture.InternalTexture, DirectXHelper.ConvertColor(color),
                DirectXHelper.ConvertRectangle(source), null,
                DirectXHelper.ConvertVector2(new Vector2(destination.X/scaleX, destination.Y/scaleY)));

            _sprite.Transform = Matrix.Identity;
        }

        /// <summary>
        /// Sets the Transform.
        /// </summary>
        /// <param name="matrix">The Matrix.</param>
        public void SetTransform(Matrix2x3 matrix)
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
        /// Resets the Transform.
        /// </summary>
        public void ResetTransform()
        {
            _sprite.Transform = Matrix.Identity;
        }

        /// <summary>
        /// Creates a new Resource.
        /// </summary>
        /// <param name="path">The Path.</param>
        /// <returns>ITexture.</returns>
        public ITexture CreateResource(string path)
        {
            var texture = new DirectXTexture(path);
            _textures.Add(texture);
            return texture;
        }

        /// <summary>
        /// Creates a new Resource.
        /// </summary>
        /// <param name="width">The Width.</param>
        /// <param name="height">The Height.</param>
        /// <returns>ITexture.</returns>
        public ITexture CreateResource(int width, int height)
        {
            return new DirectXTexture(width, height);
        }

        /// <summary>
        /// Creates a new Resource.
        /// </summary>
        /// <param name="stream">The Stream.</param>
        /// <returns>ITexture.</returns>
        public ITexture CreateResource(Stream stream)
        {
            return new DirectXTexture(stream);
        }

        /// <summary>
        /// Disposes the object.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Deconstructs the DirectXRenderer class.
        /// </summary>
        ~DirectXRenderer()
        {
            Dispose(false);
        }

        /// <summary>
        /// Disposes the object.
        /// </summary>
        /// <param name="disposing">The disposing state.</param>
        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                foreach (ITexture texture in _textures)
                {
                    texture.Dispose();
                }

                _sprite.Dispose();
                _direct3D.Dispose();
                DirectXDeviceManager.Instance.DirectXDevice.Dispose();
            }
        }
    }
}