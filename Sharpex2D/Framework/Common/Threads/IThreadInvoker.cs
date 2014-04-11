using System;

namespace Sharpex2D.Framework.Common.Threads
{
    public interface IThreadInvoker
    {
        /// <summary>
        /// Invokes an action in a special thread.
        /// </summary>
        /// <param name="action">The Action.</param>
        void Invoke(Action action);
    }
}
