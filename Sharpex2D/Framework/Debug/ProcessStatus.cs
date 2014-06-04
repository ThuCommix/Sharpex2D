using System;
using System.Diagnostics;
using System.Threading;
using Sharpex2D.Framework.Components;

namespace Sharpex2D.Framework.Debug
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Tested)]
    public class ProcessStatus : IComponent, IDisposable
    {
        #region IComponent Implementation

        /// <summary>
        ///     Gets the guid.
        /// </summary>
        public Guid Guid
        {
            get { return new Guid("493FA7A2-5B9A-485A-A7C1-3E8F6C2B55C8"); }
        }

        #endregion

        #region IDisposable Implementation

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
                    _perf.Dispose();
                }
            }
        }

        #endregion

        private readonly Process _cproc;
        private readonly PerformanceCounter _perf;
        private bool _cancel;
        private bool _isDisposed;

        /// <summary>
        ///     Initializes a new ProcessStatus class.
        /// </summary>
        public ProcessStatus()
        {
            SGL.Components.AddComponent(this);
            _cproc = Process.GetCurrentProcess();
            _perf = new PerformanceCounter("Process", "% Processor Time", _cproc.ProcessName);
            new Thread(RefreshValues) {IsBackground = true, Priority = ThreadPriority.Lowest}.Start();
        }

        /// <summary>
        ///     Gets the memory usage in bytes.
        /// </summary>
        public long MemoryUsage { get; private set; }

        /// <summary>
        ///     Gets the amount of threads.
        /// </summary>
        public int TotalThreads { get; private set; }

        /// <summary>
        ///     Gets the cpu usage.
        /// </summary>
        public int CpuUsage { get; private set; }

        /// <summary>
        ///     Refreshes all values.
        /// </summary>
        private void RefreshValues()
        {
            while (!_cancel)
            {
                _cproc.Refresh();

                MemoryUsage = _cproc.PrivateMemorySize64;
                TotalThreads = _cproc.Threads.Count;

                //start measureing
                _perf.NextValue();
                Thread.Sleep(1000);

                CpuUsage = (int) System.Math.Round(_perf.NextValue()/Environment.ProcessorCount, 0);

                Thread.Sleep(5000);
            }
        }

        /// <summary>
        ///     Cancels the service.
        /// </summary>
        /// <remarks>
        ///     If cancel is called, there is no possibility to restart this service.
        ///     You need to initialize a new instance of ProcessStatus.
        /// </remarks>
        public void Cancel()
        {
            _cancel = true;
        }
    }
}