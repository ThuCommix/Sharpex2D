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

using System;
using System.Windows.Forms;

namespace Sharpex2D.Common
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Tested)]
    public class ThreadInvoker : IThreadInvoker, IDisposable
    {
        #region IDisposable Implementation

        private bool _isDisposed;

        /// <summary>
        /// Disposes the object.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Disposes the object.
        /// </summary>
        /// <param name="disposing">Indicates whether managed resources should be disposed.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!_isDisposed)
            {
                _isDisposed = true;
                if (disposing)
                {
                    _invokeableControl.Dispose();
                }
            }
        }

        #endregion

        private readonly Control _invokeableControl;

        /// <summary>
        /// Initializes a new ThreadInvoker class.
        /// </summary>
        public ThreadInvoker()
        {
            _invokeableControl = new Control();
        }

        /// <summary>
        /// Invokes an action in a special thread.
        /// </summary>
        /// <param name="action">The Action.</param>
        public void Invoke(Action action)
        {
            //There is no other known way, to invoke in the main thread.

            if (!_invokeableControl.Created || !_invokeableControl.IsHandleCreated)
            {
                _invokeableControl.CreateControl();
                while (!_invokeableControl.IsHandleCreated)
                {
                }
            }

            _invokeableControl.Invoke(action);
        }
    }
}