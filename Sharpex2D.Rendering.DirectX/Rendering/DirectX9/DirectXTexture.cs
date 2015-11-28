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
using SharpDX;
using SharpDX.Direct3D9;

namespace Sharpex2D.Framework.Rendering.DirectX9
{
    internal class DirectXTexture : ITexture
    {
        private readonly Bitmap _bitmap;
        private BitmapData _bitmapData;

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
        public int Width { get; }

        /// <summary>
        /// Gets the Height.
        /// </summary>
        public int Height { get; }

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
            //8Bit = 1Byte, the colors are stored simply as ByteA, ByteR, ByteG, ByteB
            //LockDataRectangle requires Usage.Dynamic [and] Pool.Managed
            get
            {
                var stride = _bitmapData.Stride;
                unsafe
                {
                    var ptr = (byte*) _bitmapData.Scan0;

                    return new Color(ptr[(x*4) + y*stride], ptr[(x*4) + y*stride + 1], ptr[(x*4) + y*stride + 2],
                        ptr[(x*4) + y*stride + 3]);
                }

                //DataStream dataStream;
                //var datarect = InternalTexture.LockRectangle(0, LockFlags.None, out dataStream);

                //int offset = x*4+(y*(datarect.Pitch));

                //var pData = new byte[4];
                //dataStream.Seek(offset, SeekOrigin.Begin);
                //dataStream.Read(pData, 0, pData.Length);

                //dataStream.Dispose();
                //InternalTexture.UnlockRectangle(0);
                //System.Diagnostics.Debug.WriteLine(Color.FromArgb(pData[3], pData[2], pData[1], pData[0]));
                //return Color.FromArgb(pData[3], pData[2], pData[1], pData[0]);
            }
            set
            {
                var stride = _bitmapData.Stride;
                unsafe
                {
                    var ptr = (byte*)_bitmapData.Scan0;
                    ptr[(x*4) + y*stride] = value.B;
                    ptr[(x*4) + y*stride + 1] = value.G;
                    ptr[(x*4) + y*stride + 2] = value.R;
                    ptr[(x*4) + y*stride + 3] = value.A;
                }
            }
        }

        /// <summary>
        /// Locks the texture.
        /// </summary>
        public void Lock()
        {
            IsLocked = true;
            _bitmapData = _bitmap.LockBits(new System.Drawing.Rectangle(0, 0, _bitmap.Width, _bitmap.Height),
                ImageLockMode.ReadWrite,
                _bitmap.PixelFormat);
        }

        /// <summary>
        /// Unlocks the data.
        /// </summary>
        public void Unlock()
        {
            _bitmap.UnlockBits(_bitmapData);
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
