using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Sharpex2D.Framework.Components;

namespace Sharpex2D.Framework.Common.Threads
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Tested)]
    [ComVisible(false)]
    public abstract class TaskWorker : IComponent, IDisposable
    {
        #region IComponent Implementation

        /// <summary>
        ///     Gets the Guid.
        /// </summary>
        public Guid Guid { get; set; }

        #endregion

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
                    _task.Dispose();
                }
            }
        }

        #endregion

        private readonly Task _task;

        /// <summary>
        ///     Initializes a new TaskComponent class.
        /// </summary>
        protected TaskWorker()
        {
            Guid = Guid.NewGuid();
            _task = new Task(Work);
        }

        /// <summary>
        ///     Gets the ProgressPercentage.
        /// </summary>
        public abstract int ProgressPercentage { get; }

        /// <summary>
        ///     Starts the
        /// </summary>
        public void Start()
        {
            if (_task.Status != TaskStatus.Running)
            {
                _task.Start();
            }
        }

        /// <summary>
        ///     Starts the work.
        /// </summary>
        public virtual void Work()
        {
        }
    }
}