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

using System.Diagnostics;

namespace Sharpex2D.Framework.Rendering.OpenGL
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Tested)]
    internal static class OpenGLHelper
    {
        /// <summary>
        /// Converts the Sharpex2D.Rendering.Color class into a OpenGLColor class.
        /// </summary>
        /// <param name="color">The Color.</param>
        /// <returns>The OpenGLColor.</returns>
        public static OpenGLColor ConvertColor(Color color)
        {
            float a = color.A/255f;
            float r = color.R/255f;
            float g = color.G/255f;
            float b = color.B/255f;

            return new OpenGLColor(a, r, g, b);
        }

        /// <summary>
        /// Throws the last error.
        /// </summary>
        public static void ThrowLastError()
        {
            OpenGLError glError = OpenGLInterops.GetError();
            if (glError != OpenGLError.GL_NO_ERROR)
            {
                throw new GraphicsException(glError.ToString());
            }
        }

        /// <summary>
        /// Clears the last opengl error.
        /// </summary>
        public static void ClearLastError()
        {
            OpenGLInterops.GetError();
        }

        /// <summary>
        /// Gets the last error.
        /// </summary>
        /// <returns>OpenGLError.</returns>
        public static OpenGLError GetLastError()
        {
            return OpenGLInterops.GetError();
        }

        /// <summary>
        /// Displays the last error.
        /// </summary>
        public static void DisplayLastError()
        {
            OpenGLError error = GetLastError();
            if (error != OpenGLError.GL_NO_ERROR)
            {
                string methodName = new StackFrame(1).GetMethod().Name;
                Debug.WriteLine("{0} failed with {1}.", methodName, error);
            }
        }
    }
}