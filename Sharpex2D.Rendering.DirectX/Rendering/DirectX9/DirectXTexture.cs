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
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using SharpDX.Direct3D9;

namespace Sharpex2D.Framework.Rendering.DirectX9
{
    internal class DirectXTexture : ITexture
    {
        private readonly Bitmap _bitmap;

        /// <summary>
        /// Initializes a new DirectXTexture class.
        /// </summary>
        /// <param name="path">The Path.</param>
        internal DirectXTexture(string path)
        {
            var image = (Bitmap) Image.FromFile(path);
            Width = image.Width;
            Height = image.Height;
            _bitmap = image;

            InternalTexture = Texture.FromFile(DirectXRenderer.CurrentDevice, path, Width, Height, 0,
                Usage.RenderTarget, Format.A8R8G8B8, Pool.Default,
                Filter.None, Filter.None, 0);
        }

        /// <summary>
        /// Initializes a new DirectXTexture class.
        /// </summary>
        /// <param name="stream">The Stream.</param>
        internal DirectXTexture(Stream stream)
        {
            var bmp = (Bitmap) Image.FromStream(stream);
            Width = bmp.Width;
            Height = bmp.Height;

            var converter = new ImageConverter();
            var result = (byte[]) converter.ConvertTo(bmp, typeof (byte[]));

            InternalTexture = Texture.FromMemory(DirectXRenderer.CurrentDevice, result, Width, Height, 0,
                Usage.RenderTarget, Format.A8R8G8B8, Pool.Default,
                Filter.None, Filter.None, 0);

            _bitmap = bmp;
        }

        /// <summary>
        /// Initializes a new DirectXTexture class.
        /// </summary>
        /// <param name="width">The Width.</param>
        /// <param name="height">The Height.</param>
        internal DirectXTexture(int width, int height)
        {
            var emptyBmp = new Bitmap(width, height, PixelFormat.Format32bppArgb);
            Graphics graphics = Graphics.FromImage(emptyBmp);
            graphics.Clear(System.Drawing.Color.Transparent);
            graphics.Dispose();

            Width = width;
            Height = height;

            var converter = new ImageConverter();
            var result = (byte[]) converter.ConvertTo(emptyBmp, typeof (byte[]));

            InternalTexture = Texture.FromMemory(DirectXRenderer.CurrentDevice, result, Width, Height, 0,
                Usage.RenderTarget, Format.A8R8G8B8, Pool.Default,
                Filter.None, Filter.None, 0);

            _bitmap = emptyBmp;
        }

        /// <summary>
        /// Gets the InternalTexture.
        /// </summary>
        internal Texture InternalTexture { get; private set; }

        /// <summary>
        /// Gets the Width.
        /// </summary>
        public int Width { get; private set; }

        /// <summary>
        /// Gets the Height.
        /// </summary>
        public int Height { get; private set; }

        /// <summary>
        /// A value indicating whether the texture is locked.
        /// </summary>
        public bool IsLocked { private set; get; }

        /// <summary>
        /// Gets or sets the color of the specified texel.
        /// </summary>
        /// <param name="x">The x offset.</param>
        /// <param name="y">The y offset.</param>
        /// <returns>Color.</returns>
        public Color this[int x, int y]
        {
            //TODO Until I figured out how the datarectangle works, we will hoax with a bitmap
            //8Bit = 1Byte, the colors are stored simply as ByteA, ByteR, ByteG, ByteB
            //LockDataRectangle requires Usage.None [and] Pool.Managed
            get
            {
                System.Drawing.Color result = _bitmap.GetPixel(x, y);
                return Color.FromArgb(result.A, result.R, result.G, result.B);

                /*DataStream dataStream;
                var datarect = InternalTexture.LockRectangle(0, SharpDX.Direct3D9.LockFlags.Discard, out dataStream);

                int offset = x*4 + (y*(datarect.Pitch));

                var pData = new byte[4];
                dataStream.Read(pData, offset, pData.Length);

                dataStream.Dispose();
                InternalTexture.UnlockRectangle(0);

                return Color.FromArgb(pData[0], pData[1], pData[2], pData[3]);*/
            }
            set { _bitmap.SetPixel(x, y, System.Drawing.Color.FromArgb(value.A, value.R, value.G, value.B)); }
        }

        /// <summary>
        /// Locks the texture.
        /// </summary>
        public void Lock()
        {
            IsLocked = true;
        }

        /// <summary>
        /// Unlocks the data.
        /// </summary>
        public void Unlock()
        {
            var converter = new ImageConverter();
            var result = (byte[]) converter.ConvertTo(_bitmap, typeof (byte[]));

            InternalTexture.Dispose();
            InternalTexture = Texture.FromMemory(DirectXRenderer.CurrentDevice, result, Width, Height, 0,
                Usage.RenderTarget, Format.A8R8G8B8, Pool.Default,
                Filter.None, Filter.None, 0);

            IsLocked = false;
        }

        /// <summary>
        /// Disposes the texture.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Deconstructs the DirectXTexture class.
        /// </summary>
        ~DirectXTexture()
        {
            Dispose(false);
        }

        /// <summary>
        /// Disposes the texture.
        /// </summary>
        /// <param name="disposing">The disposing state.</param>
        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                _bitmap.Dispose();
            }

            InternalTexture.Dispose();
        }
    }
}
