using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Sharpex2D.Framework.Events;
using Sharpex2D.Framework.Game.Timing.Events;

namespace Sharpex2D.Framework.Game.Timing
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Tested)]
    public class GameLoop : IGameLoop, IDisposable
    {
        #region IComponent Implementation

        /// <summary>
        ///     Sets or gets the Guid of the Component.
        /// </summary>
        public Guid Guid
        {
            get { return new Guid("545D6C69-9340-46BE-A102-5F9E283DE04B"); }
        }

        #endregion

        #region IGameLoop Implementation

        /// <summary>
        ///     Sets or gets the RenderMode.
        /// </summary>
        public RenderMode RenderMode { set; get; }

        /// <summary>
        ///     Gets the TargetFrameTime.
        /// </summary>
        public float TargetFrameTime
        {
            get { return _targetFrameTime; }
            set
            {
                _targetFrameTime = value;
                OnFpsChanged();
            }
        }

        /// <summary>
        ///     Gets the TargetUpdateTime.
        /// </summary>
        public float TargetUpdateTime
        {
            get { return _targetUpdateTime; }
            set
            {
                _targetUpdateTime = value;
                OnFpsChanged();
            }
        }

        /// <summary>
        ///     Indicates whether the GameLoop is running.
        /// </summary>
        public bool IsRunning
        {
            get
            {
                if (_updateTask == null) return false;
                if (_renderTask == null) return false;
                return (_renderTask.Status == TaskStatus.Running) & (_updateTask.Status == TaskStatus.Running);
            }
        }

        /// <summary>
        ///     Gets or sets the Target FPS.
        /// </summary>
        public float TargetFramesPerSecond { get; private set; }

        /// <summary>
        ///     Starts the GameLoop.
        /// </summary>
        public void Start()
        {
            //Calculating the TargetTime
            float targetTime = 1000/TargetFramesPerSecond;
            TargetFrameTime = targetTime;
            TargetUpdateTime = targetTime;
            _cancelFlag = false;
            _renderTask = Task.Factory.StartNew(InternalRenderingLoop);
            _updateTask = Task.Factory.StartNew(InternalUpdateLoop);
            _totalGameTimeTask.Start();
        }

        /// <summary>
        ///     Stops the GameLoop.
        /// </summary>
        public void Stop()
        {
            _cancelFlag = true;
        }

        /// <summary>
        ///     Subscribes a IDrawable to the game loop.
        /// </summary>
        /// <param name="drawable">The IDrawable.</param>
        public void Subscribe(IDrawable drawable)
        {
            if (!_drawables.Contains(drawable))
            {
                _drawables.Add(drawable);
            }
        }

        /// <summary>
        ///     Unsubscribes a IDrawable from the game loop.
        /// </summary>
        /// <param name="drawable">The IDrawable.</param>
        public void Unsubscribe(IDrawable drawable)
        {
            if (_drawables.Contains(drawable))
            {
                _drawables.Remove(drawable);
            }
        }

        /// <summary>
        ///     Subscribes a IUpdateable to the game loop.
        /// </summary>
        /// <param name="updateable">The IDrawable.</param>
        public void Subscribe(IUpdateable updateable)
        {
            if (!_updateables.Contains(updateable))
            {
                _updateables.Add(updateable);
            }
        }

        /// <summary>
        ///     Unsubscribes a IUpdateable from the game loop.
        /// </summary>
        /// <param name="updateable">The IUpdateable.</param>
        public void Unsubscribe(IUpdateable updateable)
        {
            if (_updateables.Contains(updateable))
            {
                _updateables.Remove(updateable);
            }
        }

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
                    _updateTask.Dispose();
                    _totalGameTimeTask.Dispose();
                    _renderTask.Dispose();
                }
            }
        }

        #endregion

        #region Fields

        private readonly List<IDrawable> _drawables;
        private readonly GameTime _renderGameTime;
        private readonly Task _totalGameTimeTask;
        private readonly GameTime _updateGameTime;
        private readonly List<IUpdateable> _updateables;
        private bool _cancelFlag;
        private Task _renderTask;
        private float _renderTime;
        private float _targetFrameTime;
        private float _targetUpdateTime;
        private TimeSpan _totalGameTime;
        private float _unprocessedTicks;
        private Task _updateTask;
        private float _updateTime;

        #endregion

        #region Internal

        /// <summary>
        ///     Initializes a new GameLoop class.
        /// </summary>
        public GameLoop()
        {
            RenderMode = RenderMode.Limited;
            _drawables = new List<IDrawable>();
            _updateables = new List<IUpdateable>();
            _totalGameTime = TimeSpan.FromSeconds(0);
            TargetFrameTime = 16.666f;
            TargetUpdateTime = 16.666f;
            _updateGameTime = new GameTime
            {
                ElapsedGameTime = TargetUpdateTime,
                IsRunningSlowly = false,
                TotalGameTime = _totalGameTime
            };
            _renderGameTime = new GameTime
            {
                ElapsedGameTime = TargetFrameTime,
                IsRunningSlowly = false,
                TotalGameTime = _totalGameTime
            };
            _totalGameTimeTask = new Task(TotalGameTime, TaskCreationOptions.LongRunning);
        }

        /// <summary>
        ///     Called if the FPS changed.
        /// </summary>
        private void OnFpsChanged()
        {
            TargetFramesPerSecond = 1000/TargetFrameTime;

            if (SGL.State == SGLState.Running)
            {
                SGL.GraphicsDevice.RefreshRate = TargetFramesPerSecond;

                //publish event
                SGL.Components.Get<EventManager>()
                    .Publish(new TargetFrameTimeChangedEvent(TargetFramesPerSecond, TargetFrameTime));
            }
        }

        /// <summary>
        ///     Updates the total game time.
        /// </summary>
        private void TotalGameTime()
        {
            while (!_cancelFlag)
            {
                _totalGameTimeTask.Wait((int) TargetUpdateTime);
                _totalGameTime = _totalGameTime.Add(TimeSpan.FromMilliseconds((int) TargetUpdateTime));

                _updateGameTime.TotalGameTime = _totalGameTime;
                _renderGameTime.TotalGameTime = _totalGameTime;
            }
        }

        /// <summary>
        ///     Handles the update loop.
        /// </summary>
        private void InternalUpdateLoop()
        {
            var sw = new Stopwatch();
            while (!_cancelFlag)
            {
                //Check unprocessedTicks, if they fill a Frame suppress render and update
                if (_unprocessedTicks > TargetUpdateTime)
                {
                    var lostFrames = (int) (_unprocessedTicks/TargetUpdateTime);
                    for (int i = 1; i <= lostFrames; i++)
                    {
                        //Process a tick in every subscriber
                        for (int index = 0; index < _updateables.Count; index++)
                        {
                            IUpdateable updateable = _updateables[index];
                            lock (updateable)
                            {
                                updateable.Update(_updateGameTime);
                            }
                        }
                    }
                    //Reset the unprocessedTicks
                    _unprocessedTicks = 0;
                }

                sw.Start();
                //Process a tick in every subscriber
                for (int index = 0; index < _updateables.Count; index++)
                {
                    IUpdateable updateable = _updateables[index];
                    lock (updateable)
                    {
                        updateable.Update(_updateGameTime);
                    }
                }

                sw.Stop();
                _updateTime = sw.ElapsedMilliseconds;
                sw.Reset();
                //Check if the update was shorter than the TargetUpdateTime
                if (_updateTime < TargetUpdateTime)
                {
                    //Wait to sync
                    _updateTask.Wait((int) (TargetUpdateTime - _updateTime));
                    _updateTime = TargetUpdateTime;
                    _updateGameTime.IsRunningSlowly = false;
                }
                else
                {
                    //The UpdateTask takes to long
                    _unprocessedTicks += (_updateTime - TargetUpdateTime);
                    _updateGameTime.IsRunningSlowly = true;
                }

                _updateGameTime.ElapsedGameTime = _updateTime;
            }
        }

        /// <summary>
        ///     Handles the rendering loop.
        /// </summary>
        private void InternalRenderingLoop()
        {
            var sw = new Stopwatch();
            while (!_cancelFlag)
            {
                sw.Start();

                //Process a render in every subscriber
                for (int index = 0; index < _drawables.Count; index++)
                {
                    IDrawable drawable = _drawables[index];
                    drawable.Render(SGL.RenderDevice, _renderGameTime);
                }

                sw.Stop();
                _renderTime = sw.ElapsedMilliseconds;
                sw.Reset();
                //only limit render thread if rendermode is limited
                if (RenderMode == RenderMode.Limited)
                {
                    //Check if the render was shorter than TargetFrameTime
                    if (_renderTime < TargetFrameTime)
                    {
                        //Wait to sync
                        _renderTask.Wait((int) (TargetFrameTime - _renderTime));
                        _renderTime = TargetFrameTime;
                        _renderGameTime.IsRunningSlowly = false;
                    }
                    else
                    {
                        //If the RenderTask takes to long, there is no solution, we can't make the system better as it is
                        _renderGameTime.IsRunningSlowly = true;
                    }
                }

                _renderGameTime.ElapsedGameTime = _renderTime;
            }
        }

        #endregion
    }
}