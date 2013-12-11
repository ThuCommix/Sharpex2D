using System;
using System.Drawing;
using SharpexGL.Framework.Content;

namespace SharpexGL.Framework.Rendering.GDI
{
    internal class GdiSpriteLoader : IContentExtension
    {
        #region IContentExtension Implementation
        /// <summary>
        /// Gets the Guid of the ContentExtension.
        /// </summary>
        public Guid Guid { get; private set; }
        /// <summary>
        /// Gets the ContentType.
        /// </summary>
        public Type ContentType { get { return typeof (GdiSpriteSheet); } }
        /// <summary>
        /// Creates the IContent from the given Path.
        /// </summary>
        /// <param name="path">The Path.</param>
        /// <returns>IContent</returns>
        public IContent Create(string path)
        {
            return new GdiSpriteSheet(new GdiTexture((Bitmap) Image.FromFile(path)));
        }
        #endregion

        /// <summary>
        /// Initializes a new GdiSpriteLoader class.
        /// </summary>
        internal GdiSpriteLoader()
        {
            Guid = new Guid("F6D059CD-A6B4-4C4B-BB0A-B9802FE038A6");
        }
    }
}
