using System;

namespace Sharpex2D.Framework.Common.Threads
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Tested)]
    public interface IThreadInvoker
    {
        /// <summary>
        ///     Invokes an action in a special thread.
        /// </summary>
        /// <param name="action">The Action.</param>
        void Invoke(Action action);
    }
}