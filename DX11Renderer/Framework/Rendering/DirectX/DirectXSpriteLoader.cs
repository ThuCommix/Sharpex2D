using System;
using System.Drawing;
using Sharpex2D.Framework.Content;

namespace Sharpex2D.Framework.Rendering.DirectX
{
    public class DirectXSpriteLoader : IContentExtension
    {
         #region IContentExtension Implementation
        /// <summary>
        /// Gets the Guid of the ContentExtension.
        /// </summary>
        public Guid Guid { get; private set; }
        /// <summary>
        /// Gets the ContentType.
        /// </summary>
        public Type ContentType { get { return typeof (DirectXSpriteSheet); } }
        /// <summary>
        /// Creates the IContent from the given Path.
        /// </summary>
        /// <param name="path">The Path.</param>
        /// <returns>IContent</returns>
        public IContent Create(string path)
        {
            return new DirectXSpriteSheet(new DirectXTexture((Bitmap) Image.FromFile(path)));
        }
        #endregion

        /// <summary>
        /// Initializes a new DirectXSpriteLoader class.
        /// </summary>
        internal DirectXSpriteLoader()
        {
            Guid = new Guid("E2736DB5-BF53-4FDA-926F-84F0E8FCC096");
        }
    }
}
