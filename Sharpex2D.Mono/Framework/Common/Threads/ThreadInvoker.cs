using System;
using System.Windows.Forms;

namespace Sharpex2D.Framework.Common.Threads
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Tested)]
    public class ThreadInvoker : IThreadInvoker, IDisposable
    {
        #region IDisposable Implementation

        private bool _isDisposed;

        /// <summary>
        ///     Disposes the object.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        ///     Disposes the object.
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
        ///     Initializes a new ThreadInvoker class.
        /// </summary>
        public ThreadInvoker()
        {
            _invokeableControl = new Control();
        }

        /// <summary>
        ///     Invokes an action in a special thread.
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