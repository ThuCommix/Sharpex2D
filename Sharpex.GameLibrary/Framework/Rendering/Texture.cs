using System;
using System.Drawing;
using SharpexGL.Framework.Content.Factory;

namespace SharpexGL.Framework.Rendering
{
    [Serializable]
    public class Texture : IDisposable
    {
        /// <summary>
        /// Gets or sets the texture.
        /// </summary>
        public Bitmap Texture2D
        {
            get;
            internal set;
        }
        /// <summary>
        /// Disposes the texture.
        /// </summary>
        public void Dispose()
        {
            Texture2D.Dispose();
        }

        /// <summary>
        /// Sets or gets the TextureFactory.
        /// </summary>
        public static TextureFactory Factory {private set; get; }

        /// <summary>
        /// Static ctor.
        /// </summary>
        static Texture()
        {
            Factory = new TextureFactory();
        }

        /// <summary>
        /// Internal ctor.
        /// </summary>
        internal Texture()
        {
            
        }
    }
}
