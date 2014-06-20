using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using SharpDX;
using SharpDX.Direct2D1;
using SharpDX.DXGI;
using Sharpex2D.Framework.Content.Pipeline;
using Bitmap = SharpDX.Direct2D1.Bitmap;
using PixelFormat = SharpDX.Direct2D1.PixelFormat;
using Rectangle = System.Drawing.Rectangle;

namespace Sharpex2D.Framework.Rendering.DirectX11
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Tested)]
    [Content("DirectX11 Texture")]
    public class DirectXTexture : Texture2D
    {
        #region Texture2D Implementation

        private readonly int _height;
        private readonly int _width;

        /// <summary>
        ///     Gets the Width.
        /// </summary>
        public override int Width
        {
            get { return _width; }
        }

        /// <summary>
        ///     Gets the Height.
        /// </summary>
        public override int Height
        {
            get { return _height; }
        }

        #endregion

        private readonly Bitmap _bmp;

        /// <summary>
        ///     Initializes a new DirectXTexture class.
        /// </summary>
        /// <param name="bmp">The Bitmap.</param>
        internal DirectXTexture(System.Drawing.Bitmap bmp)
        {
            RawBitmap = (System.Drawing.Bitmap) bmp.Clone();
            _width = bmp.Width;
            _height = bmp.Height;
            var sourceArea = new Rectangle(0, 0, bmp.Width, bmp.Height);
            var bitmapProperties = new BitmapProperties(new PixelFormat(Format.R8G8B8A8_UNorm, AlphaMode.Premultiplied));
            var size = new Size2(bmp.Width, bmp.Height);

            int stride = bmp.Width*sizeof (int);
            using (var tempStream = new DataStream(bmp.Height*stride, true, true))
            {
                BitmapData bitmapData = bmp.LockBits(sourceArea, ImageLockMode.ReadOnly,
                    System.Drawing.Imaging.PixelFormat.Format32bppPArgb);

                for (int y = 0; y < bmp.Height; y++)
                {
                    int offset = bitmapData.Stride*y;
                    for (int x = 0; x < bmp.Width; x++)
                    {
                        byte b = Marshal.ReadByte(bitmapData.Scan0, offset++);
                        byte g = Marshal.ReadByte(bitmapData.Scan0, offset++);
                        byte r = Marshal.ReadByte(bitmapData.Scan0, offset++);
                        byte a = Marshal.ReadByte(bitmapData.Scan0, offset++);
                        int rgba = r | (g << 8) | (b << 16) | (a << 24);
                        tempStream.Write(rgba);
                    }
                }
                bmp.UnlockBits(bitmapData);
                tempStream.Position = 0;
                _bmp = new Bitmap(DirectXHelper.RenderTarget, size, tempStream, stride, bitmapProperties);
            }
        }

        /// <summary>
        ///     Gets the RawBitmap.
        /// </summary>
        internal System.Drawing.Bitmap RawBitmap { get; private set; }

        /// <summary>
        ///     Gets the current Bitmap.
        /// </summary>
        /// <returns></returns>
        public Bitmap GetBitmap()
        {
            return _bmp;
        }
    }
}