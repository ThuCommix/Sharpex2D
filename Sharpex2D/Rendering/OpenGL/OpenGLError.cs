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
    internal enum OpenGLError
    {
        /// <summary>
        /// No error.
        /// </summary>
        GL_NO_ERROR = 0,

        /// <summary>
        /// Invalid enum.
        /// </summary>
        GL_INVALID_ENUM = 0x0500,

        /// <summary>
        /// Invalid value.
        /// </summary>
        GL_INVALID_VALUE = 0x0501,

        /// <summary>
        /// Invalid operation.
        /// </summary>
        GL_INVALID_OPERATION = 0x0502,

        /// <summary>
        /// Stackoverflow.
        /// </summary>
        GL_STACK_OVERFLOW = 0x0503,

        /// <summary>
        /// Stackunderflow.
        /// </summary>
        GL_STACK_UNDERFLOW = 0x0504,

        /// <summary>
        /// Out of memory.
        /// </summary>
        GL_OUT_OF_MEMORY = 0x0505,

        /// <summary>
        /// Invalid framebuffer operation.
        /// </summary>
        GL_INVALID_FRAMEBUFFER_OPERATION = 0x0506,

        /// <summary>
        /// Table is too large.
        /// </summary>
        GL_TABLE_TOO_LARGE = 0x8031
    }
}