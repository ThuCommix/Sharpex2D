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

namespace Sharpex2D.Rendering.OpenGL
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Tested)]
    public enum OpenGLVersion
    {
        /// <summary>
        /// This will mainly create the highest version available.
        /// </summary>
        [Version(0, 0)] Compatibility,

        /// <summary>
        /// Version 1.1.
        /// </summary>
        [Version(1, 1)] OpenGL11,

        /// <summary>
        /// Version 1.2.
        /// </summary>
        [Version(1, 2)] OpenGL12,

        /// <summary>
        /// Version 1.3.
        /// </summary>
        [Version(1, 3)] OpenGL13,

        /// <summary>
        /// Version 1.4.
        /// </summary>
        [Version(1, 4)] OpenGL14,

        /// <summary>
        /// Version 1.5.
        /// </summary>
        [Version(1, 5)] OpenGL15,

        /// <summary>
        /// OpenGL 2.0.
        /// </summary>
        [Version(2, 0)] OpenGL20,

        /// <summary>
        /// OpenGL 2.1.
        /// </summary>
        [Version(2, 1)] OpenGL21,

        /// <summary>
        /// OpenGL 3.0.
        /// </summary>
        [Version(3, 0)] OpenGL30,

        /// <summary>
        /// OpenGL 3.1.
        /// </summary>
        [Version(3, 1)] OpenGL31,

        /// <summary>
        /// OpenGL 3.2.
        /// </summary>
        [Version(3, 2)] OpenGL32,

        /// <summary>
        /// OpenGL 3.3.
        /// </summary>
        [Version(3, 3)] OpenGL33,

        /// <summary>
        /// OpenGL 4.0.
        /// </summary>
        [Version(4, 0)] OpenGL40,

        /// <summary>
        /// OpenGL 4.1.
        /// </summary>
        [Version(4, 1)] OpenGL41,

        /// <summary>
        /// OpenGL 4.2.
        /// </summary>
        [Version(4, 2)] OpenGL42,

        /// <summary>
        /// OpenGL 4.3.
        /// </summary>
        [Version(4, 3)] OpenGL43,

        /// <summary>
        /// OpenGL 4.4.
        /// </summary>
        [Version(4, 4)] OpenGL44
    }
}