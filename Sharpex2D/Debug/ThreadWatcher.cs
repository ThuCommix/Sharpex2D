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
using System.Diagnostics;
using System.Threading.Tasks;

namespace Sharpex2D.Debug
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Tested)]
    public class ThreadWatcher : IDebugWatcher
    {
        #region IDebugWatcher Implementation

        /// <summary>
        ///     Gets the Guid.
        /// </summary>
        public Guid Guid { get; private set; }

        /// <summary>
        ///     Disposes the object.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        ///     Starts the component.
        /// </summary>
        public void Start()
        {
            if (!IsRunning)
            {
                IsRunning = true;
                _runTask = new Task(RunInner);
                _runTask.Start();
            }
        }

        /// <summary>
        ///     Stops the component.
        /// </summary>
        public void Stop()
        {
            IsRunning = false;
        }

        /// <summary>
        ///     A value indicating whether the component is running.
        /// </summary>
        public bool IsRunning { get; private set; }

        /// <summary>
        ///     Disposes the object.
        /// </summary>
        /// <param name="disposing">The Disposing state.</param>
        public virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                IsRunning = false;
            }
        }

        #endregion

        private readonly Process _currentProcess;
        private Task _runTask;

        /// <summary>
        ///     Initializes a new ThreadWatcher class.
        /// </summary>
        public ThreadWatcher()
        {
            Guid = new Guid("556AA31B-1FAD-4FF3-A8C1-F1FAE5DD08CC");
            _currentProcess = Process.GetCurrentProcess();
        }

        /// <summary>
        ///     Gets the amount of running Threads.
        /// </summary>
        public int Count { private set; get; }

        /// <summary>
        ///     Gets the ProcessThreads.
        /// </summary>
        public ProcessThread[] Threads { private set; get; }

        /// <summary>
        ///     The Run loop.
        /// </summary>
        private void RunInner()
        {
            while (IsRunning)
            {
                Count = _currentProcess.Threads.Count;
                Threads = new ProcessThread[Count];
                _currentProcess.Threads.CopyTo(Threads, 0);
                _runTask.Wait(1000);
            }
        }
    }
}