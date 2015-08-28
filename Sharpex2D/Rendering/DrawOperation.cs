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

namespace Sharpex2D.Framework.Rendering
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Tested)]
    public class DrawOperation
    {
        /// <summary>
        /// Initializes a new DrawOperation class.
        /// </summary>
        /// <param name="texture">The Texture.</param>
        /// <param name="source">The SourceRectangle.</param>
        /// <param name="destination">The DestinationRectangle.</param>
        /// <param name="color">The Color.</param>
        /// <param name="opacity">The Opacity.</param>
        public DrawOperation(ITexture texture, Rectangle source, Rectangle destination, Color color,
            float opacity = 1f)
        {
            Texture = texture;
            Source = source;
            Destination = destination;
            Color = color;
            Opacity = opacity;
        }

        /// <summary>
        /// Gets the texture.
        /// </summary>
        public ITexture Texture { private set; get; }

        /// <summary>
        /// Gets the source rectangle.
        /// </summary>
        public Rectangle Source { private set; get; }

        /// <summary>
        /// Gets the destination rectangle.
        /// </summary>
        public Rectangle Destination { private set; get; }

        /// <summary>
        /// Gets the color.
        /// </summary>
        public Color Color { private set; get; }

        /// <summary>
        /// Gets the opacity.
        /// </summary>
        public float Opacity { private set; get; }
    }
}