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

using System.Collections.Generic;
using System.Linq;

namespace Sharpex2D.Rendering.OpenGL
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Tested)]
    internal class TextEntityManager
    {
        /// <summary>
        /// Gets the maximum TextEntity cache amount.
        /// </summary>
        public const int MaxEntityCache = 10;

        private readonly Dictionary<int, TextEntity> _cache;

        /// <summary>
        /// Initializes a new TextEntityManager class.
        /// </summary>
        public TextEntityManager()
        {
            _cache = new Dictionary<int, TextEntity>();
        }

        /// <summary>
        /// Gets the font texture.
        /// </summary>
        /// <param name="text">The Text.</param>
        /// <param name="font">The Font.</param>
        /// <param name="color">The Color.</param>
        /// <param name="wrapWidth">The WrapWidth.</param>
        /// <returns></returns>
        public OpenGLTexture GetFontTexture(string text, OpenGLFont font, Color color, int wrapWidth = 0)
        {
            var textEntity = new TextEntity(text, font, color, wrapWidth);
            if (_cache.ContainsKey(textEntity.Id))
            {
                return _cache[textEntity.Id].Texture;
            }

            if (_cache.Count > MaxEntityCache)
            {
                var entity = _cache.Values.First();
                _cache.Remove(entity.Id);
            }

            textEntity.DrawText();
            _cache.Add(textEntity.Id, textEntity);
            return textEntity.Texture;
        }
    }
}
