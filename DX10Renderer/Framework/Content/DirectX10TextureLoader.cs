using System;
using System.Drawing;
using Sharpex2D.Framework.Rendering.DirectX10;

namespace Sharpex2D.Framework.Content
{
    public class DirectX10TextureLoader : IContentExtension
    {
        /// <summary>
        /// Gets the Guid.
        /// </summary>
        public Guid Guid { get { return new Guid("{C4546CDA-9C69-4081-96D5-86B8C0553371}"); } }
        /// <summary>
        /// Gets the ContentType.
        /// </summary>
        public Type ContentType { get{return typeof(DirectXTexture);} }
        /// <summary>
        /// Creates the Content.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public IContent Create(string path)
        {
            return new DirectXTexture((Bitmap) Image.FromFile(path));
        }
    }
}
