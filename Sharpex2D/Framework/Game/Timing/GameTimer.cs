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
using System.Threading;

namespace Sharpex2D.Framework.Game.Timing
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Untested)]
    public class GameTimer : ITimeable, IUpdateable
    {
        #region IComponent Implementation

        /// <summary>
        ///     Sets or gets the Guid of the Component.
        /// </summary>
        public Guid Guid
        {
            get { return new Guid("953618E5-A313-4607-BFD1-E668C1CD7DB7"); }
        }

        #endregion

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

        #region IUpdateable Implementation

        /// <summary>
        ///     Processes a Game tick.
        /// </summary>
        /// <param name="gameTime">The GameTime.</param>
        public void Update(GameTime gameTime)
        {
            Update(gameTime.ElapsedGameTime);
        }

        /// <summary>
        ///     Constructs the Component
        /// </summary>
        public void Construct()
        {
        }

        #endregion

        private bool _abort;
        private float _totalElapsed;

        /// <summary>
        ///     Initializes a new GameTimer class.
        /// </summary>
        public GameTimer()
        {
            Interval = 100;
            UpdateMode = UpdateMode.OnGameUpdate;
        }

        /// <summary>
        ///     Initializes a new GameTimer class.
        /// </summary>
        /// <param name="updateMode">The UpdateMode.</param>
        public GameTimer(UpdateMode updateMode)
        {
            Interval = 100;
            UpdateMode = updateMode;
        }

        /// <summary>
        ///     Initializes a new GameTimer class.
        /// </summary>
        /// <param name="interval">The Interval.</param>
        /// <param name="updateMode">The UpdateMode.</param>
        public GameTimer(float interval, UpdateMode updateMode)
        {
            Interval = interval;
            UpdateMode = updateMode;
        }

        /// <summary>
        ///     A value indicating whether the GameTimer is running.
        /// </summary>
        public bool IsRunning { get; private set; }

        /// <summary>
        ///     Gets the UpdateMode.
        /// </summary>
        public UpdateMode UpdateMode { private set; get; }

        /// <summary>
        ///     A value indicating whether the GameTimer is completed.
        /// </summary>
        public bool IsCompleted { private set; get; }

        /// <summary>
        ///     Sets or gets the Action, which get called after the GameTimer is completed.
        /// </summary>
        public Action Action { set; get; }

        /// <summary>
        ///     Updates the GameTimer.
        /// </summary>
        /// <param name="elapsed">The Elapsed.</param>
        private void Update(float elapsed)
        {
            _totalElapsed += elapsed;
            if (_totalElapsed >= Interval)
            {
                Stop();
                IsCompleted = true;
                if (Action != null)
                {
                    Action.Invoke();
                }
            }
        }

        /// <summary>
        ///     Starts the GameTimer.
        /// </summary>
        public void Start()
        {
            if (IsRunning) return;

            IsCompleted = false;
            _totalElapsed = 0f;
            IsRunning = true;
            _abort = false;

            if (UpdateMode == UpdateMode.OnGameUpdate)
            {
                SGL.Components.Get<IGameLoop>().Subscribe(this);
            }
            else
            {
                new Thread(() =>
                {
                    while (!_abort)
                    {
                        Thread.Sleep(1);
                        Update(1);
                    }
                    IsRunning = false;
                }) {IsBackground = true}.Start();
            }
        }

        public void Stop()
        {
            if (UpdateMode == UpdateMode.OnGameUpdate)
            {
                SGL.Components.Get<IGameLoop>().Unsubscribe(this);
            }
            else
            {
                _abort = true;
            }

            IsRunning = false;
            IsCompleted = false;
        }
    }
}