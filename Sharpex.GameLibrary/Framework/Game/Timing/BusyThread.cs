using System;
using System.Threading;

namespace SharpexGL.Framework.Game.Timing
{
    internal class BusyThread : IActiveWaiting
    {
        /// <summary>
        /// Initializes a new BusyThread class.
        /// </summary>
        public BusyThread()
        {
            _cancel = false;
            var beginHandle = new Thread(Begin);
            var measureHandle = new Thread(Measure);
            beginHandle.Start();
            measureHandle.Start();
        }
        /// <summary>
        /// Destruct the BusyThread class.
        /// </summary>
        ~BusyThread()
        {
            End();
        }

        private bool _cancel;
        private bool _isMeasured;
        private Int64 _iterations;
        private Int64 _lastIterations;
        private volatile bool _interrupt;

        /// <summary>
        /// Waits the thread for the specified miliseconds.
        /// </summary>
        /// <param name="timeSpan">The TimeSpan.</param>
        public void Busy(TimeSpan timeSpan)
        {
            //if we do not manage to measure yet, use sleep.
            if (!_isMeasured)
            {
                Thread.Sleep(timeSpan);
                return;
            }

            var neededIterations =
                (int)  timeSpan.Ticks * _lastIterations/(TimeSpan.TicksPerSecond*5);

            var r = 0;
            while (r < neededIterations)
            {
                r++;
            }

        }
        /// <summary>
        /// Begins the time notify.
        /// </summary>
        private void Begin()
        {
            while (!_cancel)
            {
                _interrupt = false;
                Thread.Sleep(5000);
                _interrupt = true;
                _isMeasured = true;
                _lastIterations = _iterations;
                _iterations = 0;
            }
        }

        /// <summary>
        /// Starts measuring.
        /// </summary>
        private void Measure()
        {
            while (!_cancel)
            {
                while (!_interrupt)
                {
                    _iterations++;
                }
            }
        }

        /// <summary>
        /// Stops the measuring.
        /// </summary>
        private void End()
        {
            _cancel = true;
        }
    }
}
