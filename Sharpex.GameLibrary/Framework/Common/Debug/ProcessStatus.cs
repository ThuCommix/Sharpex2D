using System;
using System.Diagnostics;
using System.Threading;
using SharpexGL.Framework.Components;

namespace SharpexGL.Framework.Common.Debug
{
    public class ProcessStatus : IComponent
    {
        #region IComponent Implementation
        /// <summary>
        /// Gets the guid.
        /// </summary>
        public Guid Guid {
            get
            {
                return new Guid("493FA7A2-5B9A-485A-A7C1-3E8F6C2B55C8"); 
        } }
        #endregion

        private Process _cproc;
        private bool _cancel;
        private readonly PerformanceCounter _perf;

        /// <summary>
        /// Gets the memory usage in bytes.
        /// </summary>
        public long MemoryUsage { get; private set; }
        /// <summary>
        /// Gets the amount of threads.
        /// </summary>
        public int TotalThreads { get; private set; }
        /// <summary>
        /// Gets the cpu usage.
        /// </summary>
        public int CpuUsage { get; private set; }

        /// <summary>
        /// Initializes a new ProcessStatus class.
        /// </summary>
        public ProcessStatus()
        {
            SGL.Components.AddComponent(this);
            _perf = new PerformanceCounter("Process", "% Processor Time", _cproc.ProcessName);
            new Thread(RefreshValues) {IsBackground = true, Priority = ThreadPriority.Lowest}.Start();
        }
        /// <summary>
        /// Refreshes all values.
        /// </summary>
        private void RefreshValues()
        {
            _cproc = Process.GetCurrentProcess();

            while (!_cancel)
            {
                _cproc.Refresh();

                MemoryUsage = _cproc.PrivateMemorySize64;
                TotalThreads = _cproc.Threads.Count;

                //start measureing
                _perf.NextValue();
                Thread.Sleep(1000);

                CpuUsage = (int)System.Math.Round(_perf.NextValue(), 0);

                Thread.Sleep(5000);
            }
        }
        /// <summary>
        /// Cancels the service.
        /// </summary>
        /// <remarks>If cancel is called, there is no possibility to restart this service.
        /// You need to initialize a new instance of ProcessStatus.</remarks>
        public void Cancel()
        {
            _cancel = true;
        }
    }
}
