// Copyright (c) 2012-2015 Sharpex2D - Kevin Scholz (ThuCommix)
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
using System.Linq;
using System.Threading;
using Sharpex2D.Framework.Rendering;

namespace Sharpex2D.Framework
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Tested)]
    public class GameLoop : IComponent
    {
        private readonly List<IDrawable> _drawables;
        private readonly GameTime _gameTime;
        private readonly Stopwatch _gameTimer;
        private readonly Thread _loopThread;
        private readonly List<IUpdateable> _updateables;

        /// <summary>
        /// Initializes a new GameLoop class.
        /// </summary>
        public GameLoop()
        {
            TargetTime = 16.666f;
            _loopThread = new Thread(Loop) {Priority = ThreadPriority.Highest};
            _gameTimer = new Stopwatch();
            _drawables = new List<IDrawable>();
            _updateables = new List<IUpdateable>();
            _gameTime = new GameTime();
            Precision = Precision.High;
            DrawMode = DrawMode.Limited;
        }

        /// <summary>
        /// Gets or sets the TargetTime.
        /// </summary>
        public float TargetTime { set; get; }

        /// <summary>
        /// A value indicating whether the gameloop is running.
        /// </summary>
        public bool IsRunning { get; private set; }

        /// <summary>
        /// Gets the measured draws per second.
        /// </summary>
        public int Draws { get; private set; }

        /// <summary>
        /// Gets the measured updates per second.
        /// </summary>
        public int Updates { get; private set; }

        /// <summary>
        /// Gets or sets the DrawMode.
        /// </summary>
        public DrawMode DrawMode { set; get; }

        /// <summary>
        /// Gets or sets the Precision.
        /// </summary>
        public Precision Precision { set; get; }

        /// <summary>
        /// Gets the Guid.
        /// </summary>
        public Guid Guid => new Guid("DD6E2432-C78E-4C15-8E99-854369D96781");

        /// <summary>
        /// Subscribes a new IDrawable to the game loop.
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
        /// Subscribes a new IUpdateable to the game loop.
        /// </summary>
        /// <param name="updateable">The IUpdateable.</param>
        public void Subscribe(IUpdateable updateable)
        {
            if (!_updateables.Contains(updateable))
            {
                _updateables.Add(updateable);
            }
        }

        /// <summary>
        /// Unsubscribes a IDrawable from the game loop.
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
        /// Unsubscribes a IUpdateable from the game loop.
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
        /// Stops the game loop.
        /// </summary>
        public void Stop()
        {
            IsRunning = false;
        }

        /// <summary>
        /// Starts the game loop.
        /// </summary>
        public void Start()
        {
            if (!IsRunning)
            {
                IsRunning = true;
                _loopThread.Start();
            }
        }

        /// <summary>
        /// The actual game loop.
        /// </summary>
        private void Loop()
        {
            //initialize render device
            GameHost.SpriteBatch = new SpriteBatch(GameHost.GraphicsManager) {GraphicsDevice = GameHost.GraphicsDevice};
            GameHost.Components.Add(GameHost.SpriteBatch);

            //load content 
            GameHost.GameInstance.OnLoadContent();

            _gameTimer.Start();
            long startTime = _gameTimer.ElapsedTicks;
            int updates = 0;
            int frames = 0;
            double frameCounter = 0;
            double unprocessedTime = 0;

            while (IsRunning)
            {
                long currentTime = _gameTimer.ElapsedTicks;
                double passedTime = currentTime - startTime;
                startTime = currentTime;
                unprocessedTime += passedTime;
                frameCounter += passedTime;
                double frameTime = Stopwatch.Frequency/(1000/TargetTime);
                bool requestRender = false;

                while (unprocessedTime > frameTime)
                {
                    _gameTime.ElapsedGameTime = (float) (unprocessedTime/Stopwatch.Frequency)*1000f;
                    _gameTime.TotalGameTime += new TimeSpan(0, 0, 0, 0, (int) TargetTime);

                    unprocessedTime -= frameTime;
                    updates++;

                    UpdateSubscribers();

                    requestRender = true;
                }

                if (frameCounter >= Stopwatch.Frequency)
                {
                    frameCounter = 0;
                    Draws = frames;
                    Updates = updates;
                    frames = 0;
                    updates = 0;
                }

                if (DrawMode == DrawMode.Limited)
                {
                    if (requestRender)
                    {
                        frames++;
                        RenderSubscribers();
                    }
                    else
                    {
                        if (Precision != Precision.High)
                        {
                            Thread.Sleep((int) Precision);
                        }
                    }
                }
                else
                {
                    frames++;
                    RenderSubscribers();
                }
            }

            //the game ended, clean up components

            foreach (IDisposable component in GameHost.Components.OfType<IDisposable>())
            {
                (component).Dispose();
#if DEBUG
                Logger.Instance.Engine($"Disposing: {component.GetType().Name}");
#endif
            }
        }

        /// <summary>
        /// Updates the subscribers.
        /// </summary>
        private void UpdateSubscribers()
        {
            for (int i = 0; i < _updateables.Count; i++)
            {
                _updateables[i].Update(_gameTime);
            }
        }

        /// <summary>
        /// Renders the subscribers.
        /// </summary>
        private void RenderSubscribers()
        {
            GameHost.SpriteBatch.Clear();
            for (int i = 0; i < _drawables.Count; i++)
            {
                _drawables[i].Draw(GameHost.SpriteBatch, _gameTime);
            }
            GameHost.SpriteBatch.Present();
        }
    }
}