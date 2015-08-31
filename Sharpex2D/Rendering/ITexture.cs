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

using System;
using Sharpex2D.Framework.Content;

namespace Sharpex2D.Framework.Rendering
{
    public interface ITexture : IContent, IDisposable
    {
        /// <summary>
        /// Gets the Width.
        /// </summary>
        int Width { get; }

        /// <summary>
        /// Gets the Height.
        /// </summary>
        int Height { get; }

        /// <summary>
        /// A value indicating whether the texture is locked.
        /// </summary>
        bool IsLocked { get; }

        /// <summary>
        /// Gets or sets the color of the specified texel.
        /// </summary>
        /// <param name="x">The x offset.</param>
        /// <param name="y">The y offset.</param>
        /// <returns>Color.</returns>
        /// <remarks>The texture must be locked before accessing the color data.</remarks>
        Color this[int x, int y] { set; get; }

        /// <summary>
        /// Locks the texture.
        /// </summary>
        void Lock();

        /// <summary>
        /// Unlocks the data.
        /// </summary>
        void Unlock();
    }
}
