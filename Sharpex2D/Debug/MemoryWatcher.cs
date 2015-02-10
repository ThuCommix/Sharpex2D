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
using System.Threading.Tasks;

namespace Sharpex2D.Debug
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Tested)]
    public class MemoryWatcher : IDebugWatcher
    {
        private Task _runTask;

        /// <summary>
        /// Initializes a new MemoryWatcher class.
        /// </summary>
        public MemoryWatcher()
        {
            Guid = new Guid("FF7BDAD4-1F3A-4C77-8BDA-82F1CEA43FD8");
        }

        /// <summary>
        /// Gets the used memory.
        /// </summary>
        public Memory Memory { private set; get; }

        /// <summary>
        /// The Run loop.
        /// </summary>
        private void RunInner()
        {
            while (IsRunning)
            {
                Memory = new Memory(Environment.WorkingSet, MemoryUnit.Byte);
                _runTask.Wait(100);
            }
        }

        #region IDebugWatcher Implementation

        /// <summary>
        /// Gets the Guid.
        /// </summary>
        public Guid Guid { get; private set; }

        /// <summary>
        /// A value indicating whether the component is running.
        /// </summary>
        public bool IsRunning { private set; get; }

        /// <summary>
        /// Starts the component.
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
        /// Stops the component.
        /// </summary>
        public void Stop()
        {
            IsRunning = false;
        }

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
        /// <param name="disposing">The Disposing state.</param>
        public virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                IsRunning = false;
            }
        }

        #endregion
    }
}