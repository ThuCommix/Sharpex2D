using System;
using System.Diagnostics;
using System.Threading;

namespace SharpexGL.Framework.Game.Timing
{
    public class PrecisionTimer : ITimeable
    {
        #region ITimeable Implemenation

        private float _intervall;
        /// <summary>
        /// Sets or gets the Intervall.
        /// </summary>
        public float Intervall
        {
            get { return _intervall; }
            set
            {
                if (value <= 0f)
                {
                    throw new ArgumentException("Value must be greater than 0.");
                }
                _intervall = value;
            }
        }


        #endregion

        /// <summary>
        /// A value indicating whether the GameTimer is running.
        /// </summary>
        public bool IsRunning { get; private set; }
        /// <summary>
        /// A value indicating whether the GameTimer is completed.
        /// </summary>
        public bool IsCompleted { private set; get; }
        /// <summary>
        /// Sets or gets the Action, which get called after the GameTimer is completed.
        /// </summary>
        public Action Action { set; get; }

        private bool _abort;

        /// <summary>
        /// Initializes a new PrecisionTimer class.
        /// </summary>
        public PrecisionTimer()
        {
            Intervall = 100;
        }
        /// <summary>
        /// Initializes a new PrecisionTimer class.
        /// </summary>
        /// <param name="intervall">The Intervall.</param>
        public PrecisionTimer(float intervall)
        {
            Intervall = intervall;
        }

        public void Start()
        {
            if (IsRunning) return;
            IsRunning = true;
            _abort = false;

            new Thread(() =>
            {
                var sw = new Stopwatch();
                sw.Start();
                if (Intervall - 1 > 1)
                {
                    //wait full miliseconds
                    Thread.Sleep((int) _intervall - 1);
                }
                while (!_abort && sw.ElapsedMilliseconds < _intervall)
                {
                    
                }
                sw.Stop();

                IsRunning = false;

                if (!_abort)
                {
                    IsCompleted = true;
                    if (Action != null)
                    {
                        Action.Invoke();
                    }
                }

            }) {IsBackground = true}.Start();
        }


        public void Stop()
        {
            _abort = true;
        }
    }
}
