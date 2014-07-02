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
using System.Threading;

namespace Sharpex2D.Framework.Game.Timing
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Untested)]
    public class PrecisionTimer : ITimeable
    {
        #region ITimeable Implemenation

        private float _interval;

        /// <summary>
        ///     Sets or gets the Intervall.
        /// </summary>
        public float Interval
        {
            get { return _interval; }
            set
            {
                if (value <= 0f)
                {
                    throw new ArgumentException("Value must be greater than 0.");
                }
                _interval = value;
            }
        }

        #endregion

        private bool _abort;

        /// <summary>
        ///     Initializes a new PrecisionTimer class.
        /// </summary>
        public PrecisionTimer()
        {
            Interval = 100;
        }

        /// <summary>
        ///     Initializes a new PrecisionTimer class.
        /// </summary>
        /// <param name="interval">The Interval.</param>
        public PrecisionTimer(float interval)
        {
            Interval = interval;
        }

        /// <summary>
        ///     A value indicating whether the GameTimer is running.
        /// </summary>
        public bool IsRunning { get; private set; }

        /// <summary>
        ///     A value indicating whether the GameTimer is completed.
        /// </summary>
        public bool IsCompleted { private set; get; }

        /// <summary>
        ///     Sets or gets the Action, which get called after the GameTimer is completed.
        /// </summary>
        public Action Action { set; get; }

        public void Start()
        {
            if (IsRunning) return;
            IsRunning = true;
            _abort = false;

            new Thread(() =>
            {
                var sw = new Stopwatch();
                sw.Start();
                if (Interval - 1 > 1)
                {
                    //wait full miliseconds
                    Thread.Sleep((int) _interval - 1);
                }
                while (!_abort && sw.ElapsedMilliseconds < _interval)
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