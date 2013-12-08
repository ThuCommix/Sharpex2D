using System.Runtime.InteropServices;
using SharpDX;
using SharpDX.DXGI;
using SharpDX.Direct2D1;
using SharpexGL.Framework.Content;
using AlphaMode = SharpDX.Direct2D1.AlphaMode;
using Bitmap = System.Drawing.Bitmap;
using PixelFormat = SharpDX.Direct2D1.PixelFormat;

namespace SharpexGL.Framework.Rendering.DirectX
{
    public class DirectXTexture : ITexture, IContent
    {
        #region ITexture Implementation
        /// <summary>
        /// Gets the Width.
        /// </summary>
        public int Width { get; private set; }
        /// <summary>
        /// Gets the Height.
        /// </summary>
        public int Height { get; private set; }

        #endregion

        private readonly SharpDX.Direct2D1.Bitmap _bmp;

        /// <summary>
        /// Gets the RawBitmap.
        /// </summary>
        internal Bitmap RawBitmap { get; private set; }

        /// <summary>
        /// Initializes a new DirectXTexture class.
        /// </summary>
        /// <param name="bmp">The Bitmap.</param>
        internal DirectXTexture(Bitmap bmp)
        {
            RawBitmap = (Bitmap)bmp.Clone();
            Width = bmp.Width;
            Height = bmp.Height;
            var sourceArea = new System.Drawing.Rectangle(0, 0, bmp.Width, bmp.Height);
            var bitmapProperties = new BitmapProperties(new PixelFormat(Format.R8G8B8A8_UNorm, AlphaMode.Premultiplied));
            var size = new Size2(bmp.Width, bmp.Height);

            var stride = bmp.Width*sizeof (int);
            using (var tempStream = new DataStream(bmp.Height*stride, true, true))
            {
                var bitmapData = bmp.LockBits(sourceArea, System.Drawing.Imaging.ImageLockMode.ReadOnly,
                    System.Drawing.Imaging.PixelFormat.Format32bppPArgb);

                for (var y = 0; y < bmp.Height; y++)
                {
                    var offset = bitmapData.Stride*y;
                    for (var x = 0; x < bmp.Width; x++)
                    {
                        var b = Marshal.ReadByte(bitmapData.Scan0, offset++);
                        var g = Marshal.ReadByte(bitmapData.Scan0, offset++);
                        var r = Marshal.ReadByte(bitmapData.Scan0, offset++);
                        var a = Marshal.ReadByte(bitmapData.Scan0, offset++);
                        var rgba = r | (g << 8) | (b << 16) | (a << 24);
                        tempStream.Write(rgba);
                    }

                }
                bmp.UnlockBits(bitmapData);
                tempStream.Position = 0;
                _bmp = new SharpDX.Direct2D1.Bitmap(DirectXHelper.RenderTarget, size, tempStream, stride, bitmapProperties);
            }
        }

        /// <summary>
        /// Gets the current Bitmap.
        /// </summary>
        /// <returns></returns>
        public SharpDX.Direct2D1.Bitmap GetBitmap()
        {
            return _bmp;
        }
    }
}
