using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using Sharpex2D.Framework.Content.Pipeline;
using SlimDX.Direct3D9;

namespace Sharpex2D.Framework.Rendering.DirectX9
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Tested)]
    [Content("DirectX9 Texture")]
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

        private readonly Texture _texture;

        /// <summary>
        ///     Initializes a new DirectXTexture class.
        /// </summary>
        /// <param name="path">The Path.</param>
        internal DirectXTexture(string path)
        {
            //dirty but we need the image informations
            var bmp = (Bitmap) Image.FromFile(path);

            _width = bmp.Width;
            _height = bmp.Height;

            RawBitmap = bmp;

            _texture = Texture.FromFile(DirectXHelper.Direct3D9, path, Width, Height, 0, Usage.RenderTarget,
                Format.A8R8G8B8, Pool.Default, Filter.None, Filter.None, 0);
        }

        /// <summary>
        ///     Initializes a new DirectXTexture class.
        /// </summary>
        /// <param name="stream">The Stream.</param>
        internal DirectXTexture(Stream stream)
        {
            //dirty but we need the image informations
            var bmp = (Bitmap) Image.FromStream(stream);

            _width = bmp.Width;
            _height = bmp.Height;

            RawBitmap = bmp;

            stream.Seek(0, SeekOrigin.Begin);

            _texture = Texture.FromStream(DirectXHelper.Direct3D9, stream, Width, Height, 0, Usage.RenderTarget,
                Format.A8R8G8B8, Pool.Default, Filter.None, Filter.None, 0);
        }

        /// <summary>
        ///     Initializes a new DirectXTexture class.
        /// </summary>
        /// <param name="bitmap">The Bitmap.</param>
        internal DirectXTexture(Bitmap bitmap)
        {
            _width = bitmap.Width;
            _height = bitmap.Height;

            RawBitmap = bitmap;

            var memorySteam = new MemoryStream();

            bitmap.Save(memorySteam, ImageFormat.Png);

            _texture = Texture.FromStream(DirectXHelper.Direct3D9, memorySteam, Width, Height, 0, Usage.RenderTarget,
                Format.A8R8G8B8, Pool.Default, Filter.None, Filter.None, 0);

            memorySteam.Dispose();
        }

        /// <summary>
        ///     Gets the RawBitmap.
        /// </summary>
        internal Bitmap RawBitmap { private set; get; }

        /// <summary>
        ///     Gets the Texture.
        /// </summary>
        /// <returns>Texture.</returns>
        internal Texture GetTexture()
        {
            return _texture;
        }
    }
}