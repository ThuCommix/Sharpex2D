using System;
using System.Windows.Forms;

namespace SharpexGL.Framework.Common.Threads
{
    public class MainThreadInvoker : IThreadInvoker
    {
        /// <summary>
        /// Invokes an action in a special thread.
        /// </summary>
        /// <param name="action">The Action.</param>
        public void Invoke(Action action)
        {
            //There is no other known way, to invoke in the main thread.
            var control = new Control();
            MethodInvoker br = action.Invoke;
            control.Invoke(br);
        }
    }
}
