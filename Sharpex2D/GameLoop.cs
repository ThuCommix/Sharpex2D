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
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Sharpex2D
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
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
        ///     A value indicating whether the game loop should idle.
        /// </summary>
        public bool Idle { set; get; }

        /// <summary>
        ///     Gets or sets the IdleDuration.
        /// </summary>
        public float IdleDuration { set; get; }

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
            _drawables = new List<IDrawable>();
            _updateables = new List<IUpdateable>();
            _totalGameTime = TimeSpan.FromSeconds(0);
            TargetFrameTime = 16.666f;
            TargetUpdateTime = 16.666f;
            IdleDuration = 500;
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

            if (SGL.State == EngineState.Running)
            {
                SGL.GraphicsDevice.RefreshRate = TargetFramesPerSecond;
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

                if (Idle)
                {
                    _totalGameTimeTask.Wait((int) IdleDuration);
                }
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

                if (Idle)
                {
                    _updateTask.Wait((int) IdleDuration);
                }
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

                _renderGameTime.ElapsedGameTime = _renderTime;

                if (Idle)
                {
                    _renderTask.Wait((int) IdleDuration);
                }
            }
        }

        #endregion
    }
}