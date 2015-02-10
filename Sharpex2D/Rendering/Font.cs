// Copyright (c) 2012-2014 Sharpex2D - Kevin Scholz (ThuCommix)
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

namespace Sharpex2D.Rendering
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Tested)]
    [MetaData("Name", "Font")]
    public class Font
    {
        /// <summary>
        /// Initializes a new Font class.
        /// </summary>
        /// <param name="typeface">The Typeface.</param>
        public Font(Typeface typeface)
        {
            SpriteBatch spriteBatch = SGL.SpriteBatch;
            Instance = spriteBatch.Graphics.ResourceManager.CreateResource(typeface);
        }

        /// <summary>
        /// Initializes a new Font class.
        /// </summary>
        /// <param name="familyName">The FamilyName.</param>
        /// <param name="size">The Size.</param>
        /// <param name="style">The Style.</param>
        public Font(string familyName, float size, TypefaceStyle style)
            : this(new Typeface {FamilyName = familyName, Size = size, Style = style})
        {
        }

        /// <summary>
        /// Gets the Instance.
        /// </summary>
        internal IFont Instance { private set; get; }

        /// <summary>
        /// Gets the Typeface.
        /// </summary>
        public Typeface Typeface
        {
            get { return Instance.Typeface; }
        }
    }
}