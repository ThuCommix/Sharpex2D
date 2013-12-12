using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpexGL.Framework.Math;
using SharpexGL.Framework.Rendering;

namespace SharpexGL.Framework.Physics.Shapes
{
    public class TextureBasedCircle : Circle
    {
        /// <summary>
        /// Gets the Texture.
        /// </summary>
        public ITexture Texture { private set; get; }
        /// <summary>
        /// Initializes a new TextureBasedCircle class.
        /// </summary>
        /// <param name="texture">The underlying Texture.</param>
        public TextureBasedCircle(ITexture texture)
        {
            Texture = texture;
        }
    }
}
