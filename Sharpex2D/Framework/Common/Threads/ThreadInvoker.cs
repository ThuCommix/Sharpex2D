using System;
using System.Windows.Forms;

namespace Sharpex2D.Framework.Common.Threads
{
    public class ThreadInvoker : IThreadInvoker
    {
        /// <summary>
        /// Initializes a new ThreadInvoker class.
        /// </summary>
        public ThreadInvoker()
        {
            _invokeableControl = new Control();
        }

        private readonly Control _invokeableControl;

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
                while (!_invokeableControl.IsHandleCreated) {}
            }

            _invokeableControl.Invoke(action);
        }
    }
}
