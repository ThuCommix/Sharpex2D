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
using Sharpex2D.Framework.Debug.Logging;
using Sharpex2D.Framework.Rendering.Devices;

namespace Sharpex2D.Framework.Game.Timing
{
    public class XnaGameLoop : IGameLoop
    {
        private readonly List<IDrawable> _drawables;
        private readonly GameTime _gameTime;
        private readonly Task _loopTask;
        private readonly Stopwatch _sw;
        private readonly List<IUpdateable> _updateables;
        private bool _cancel;
        private RenderDevice _device;
        private float _lostms;
        private bool _skipRender;
        private float _targetFrameTime;
        private float _targetUpdateTime;
        private float _totalTime;

        /// <summary>
        ///     Initializes a new XnaGameLoop class.
        /// </summary>
        public XnaGameLoop()
        {
            TargetUpdateTime = 16.666f;
            TargetFrameTime = 16.666f;
            _drawables = new List<IDrawable>();
            _updateables = new List<IUpdateable>();
            IdleDuration = 500;
            Guid = new Guid("3D6DBB92-CED9-4EE9-8674-94BEF0F34103");
            _gameTime = new GameTime
            {
                ElapsedGameTime = 16.6f,
                IsRunningSlowly = false,
                TotalGameTime = TimeSpan.FromSeconds(0)
            };
            _sw = new Stopwatch();
            _loopTask = new Task(XnaLoop);
        }

        /// <summary>
        ///     Gets the Guid.
        /// </summary>
        public Guid Guid { get; private set; }

        /// <summary>
        ///     Gets or sets the TargetFrameTime.
        /// </summary>
        public float TargetFrameTime
        {
            get { return _targetFrameTime; }
            set
            {
                _targetFrameTime = value;
                _targetUpdateTime = value;
            }
        }

        /// <summary>
        ///     Gets or sets the TargetUpdateTime.
        /// </summary>
        public float TargetUpdateTime
        {
            get { return _targetUpdateTime; }
            set
            {
                _targetUpdateTime = value;
                _targetFrameTime = value;
            }
        }

        /// <summary>
        ///     Indicates whether the GameLoop is running.
        /// </summary>
        public bool IsRunning { get; private set; }

        /// <summary>
        ///     A value indicating whether the game loop should idle.
        /// </summary>
        public bool Idle { set; get; }

        /// <summary>
        ///     Gets or sets the IdleDuration.
        /// </summary>
        public float IdleDuration { set; get; }

        /// <summary>
        ///     Gets the Target FPS.
        /// </summary>
        public float TargetFramesPerSecond
        {
            get { return 1000/TargetFrameTime; }
        }

        /// <summary>
        ///     Starts the GameLoop.
        /// </summary>
        public void Start()
        {
            if (IsRunning)
            {
                LogManager.GetClassLogger().Warn("It has been tried to start a already running game loop.");
                return;
            }

            _device = SGL.Components.Get<RenderDevice>();

            _cancel = false;
            IsRunning = true;
            _loopTask.Start();
        }

        /// <summary>
        ///     Stops the GameLoop.
        /// </summary>
        public void Stop()
        {
            _cancel = true;
            IsRunning = false;
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

        /// <summary>
        ///     The XnaLoop.
        /// </summary>
        private void XnaLoop()
        {
            while (!_cancel)
            {
                _sw.Start();

                foreach (IUpdateable updateable in _updateables)
                {
                    updateable.Update(_gameTime);
                }

                if (!_skipRender)
                {
                    foreach (IDrawable drawable in _drawables)
                    {
                        drawable.Render(_device, _gameTime);
                    }
                }
                else
                {
                    // do another update
                    foreach (IUpdateable updateable in _updateables)
                    {
                        updateable.Update(_gameTime);
                    }
                }

                _sw.Stop();

                _totalTime += _sw.ElapsedMilliseconds;
                _gameTime.TotalGameTime = TimeSpan.FromMilliseconds(_totalTime);

                if (_sw.ElapsedMilliseconds > TargetFrameTime)
                {
                    _lostms = _sw.ElapsedMilliseconds - TargetFrameTime;
                    if (_lostms >= TargetFrameTime)
                    {
                        _skipRender = true;
                        _lostms -= TargetFrameTime;
                    }
                    _gameTime.IsRunningSlowly = true;
                    _gameTime.ElapsedGameTime = _sw.ElapsedMilliseconds;
                }
                else
                {
                    _skipRender = false;
                    _gameTime.IsRunningSlowly = false;
                    var waitTime = (int) (TargetFrameTime - _sw.ElapsedMilliseconds);
                    if (waitTime > 0)
                    {
                        _loopTask.Wait(waitTime);
                    }
                    _gameTime.ElapsedGameTime = _targetFrameTime;
                }

                _sw.Reset();

                if (Idle)
                {
                    _loopTask.Wait((int) IdleDuration);
                }
            }
        }
    }
}