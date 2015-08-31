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

namespace Sharpex2D.Framework.Rendering.OpenGL
{
    internal enum TextureParam : uint
    {
        /// <summary>
        /// Sampler 0.
        /// </summary>
        Texture0 = 0x84C0,

        /// <summary>
        /// Repeat.
        /// </summary>
        Repeat = 0x2901,

        /// <summary>
        /// Wrap s.
        /// </summary>
        WrapS = 0x2802,

        /// <summary>
        /// Wrap t.
        /// </summary>
        WrapT = 0x2803,

        /// <summary>
        /// Nearest neighbor interpolation.
        /// </summary>
        Nearest = 9728u,

        /// <summary>
        /// Linear interpolation.
        /// </summary>
        Linear = 9729u,

        /// <summary>
        /// Mag filter.
        /// </summary>
        MagFilter = 10240u,

        /// <summary>
        /// Min filter.
        /// </summary>
        MinFilter = 10241u,

        /// <summary>
        /// Texture2D.
        /// </summary>
        Texture2D = 3553u
    }
}
