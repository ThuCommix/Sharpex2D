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

namespace Sharpex2D.Framework.Audio.OpenAL
{
    internal class ALContext : IDisposable
    {
        /// <summary>
        /// Gets the handle
        /// </summary>
        public IntPtr Handle { private set; get; }

        /// <summary>
        /// Initializes a new ALContext class
        /// </summary>
        /// <param name="contextHandle">The handle</param>
        private ALContext(IntPtr contextHandle)
        {
            Handle = contextHandle;
        }

        /// <summary>
        /// Makes the context the current context
        /// </summary>
        public void MakeCurrent()
        {
            ALInterops.alcMakeContextCurrent(Handle);
        }

        /// <summary>
        /// Deconstructs the ALContext class
        /// </summary>
        ~ALContext()
        {
            Dispose(false);
        }

        /// <summary>
        /// Disposes the openal context
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Disposes the openal context
        /// </summary>
        /// <param name="disposing">The disposing state</param>
        protected void Dispose(bool disposing)
        {
            if (Handle != IntPtr.Zero)
            {
                ALInterops.alcDestroyContext(Handle);
                Handle = IntPtr.Zero;
            }
        }

        /// <summary>
        /// Creates a new openal context
        /// </summary>
        /// <param name="deviceHandle">The device handle</param>
        /// <returns>OpenALContext</returns>
        public static ALContext CreateContext(IntPtr deviceHandle)
        {
            return new ALContext(ALInterops.alcCreateContext(deviceHandle, IntPtr.Zero));
        }
    }
}
