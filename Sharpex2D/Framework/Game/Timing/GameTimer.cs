using System;
using System.Threading;

namespace Sharpex2D.Framework.Game.Timing
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
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
        public void Tick(GameTime gameTime)
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
            UpdateMode = UpdateMode.OnGameTick;
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

            if (UpdateMode == UpdateMode.OnGameTick)
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
            if (UpdateMode == UpdateMode.OnGameTick)
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