using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using SharpexGL.Framework.Events;
using SharpexGL.Framework.Game.Timing.Events;

namespace SharpexGL.Framework.Game.Timing
{
    public class SingleThreadGameLoop : IGameLoop
    {
        #region IGameLoop Implementation
        /// <summary>
        /// Gets the TargetFrameTime.
        /// </summary>
        public float TargetFrameTime { get; private set; }
        /// <summary>
        /// Gets the TargetUpdateTime.
        /// </summary>
        public float TargetUpdateTime { get; private set; }
        /// <summary>
        /// Indicates whether the GameLoop is running.
        /// </summary>
        public bool IsRunning { get; private set; }

        /// <summary>
        /// Gets or sets the Target FPS.
        /// </summary>
        public float TargetFramesPerSecond
        {
            get { return _targetFramesPerSecond; }
            set { _targetFramesPerSecond = value;
                OnFpsChanged();
            }
        }

        /// <summary>
        /// Subscribes a T GameHandler to the IGameLoop.
        /// </summary>
        /// <param name="gameHandler">The GameHandler</param>
        public void Subscribe(IGameHandler gameHandler)
        {
            if (!_subscribers.Contains(gameHandler))
            {
                _subscribers.Add(gameHandler);
            }
        }
        /// <summary>
        /// Unsubscribes a T GameHandler from the IGameLoop.
        /// </summary>
        /// <param name="gameHandler">The GameHandler</param>
        public void Unsubscribe(IGameHandler gameHandler)
        {
            if (_subscribers.Contains(gameHandler))
            {
                _subscribers.Remove(gameHandler);
            }
        }
        /// <summary>
        /// Starts the GameLoop.
        /// </summary>
        public void Start()
        {
            TargetFrameTime = 1000/TargetFramesPerSecond;
            TargetUpdateTime = TargetFrameTime;
            IsRunning = true;
            new Thread(InternalLoop) {IsBackground = true}.Start();
        }
        /// <summary>
        /// Stops the GameLoop.
        /// </summary>
        public void Stop()
        {
            IsRunning = false;
        }
        #endregion

        #region IComponent Implementation

        /// <summary>
        /// Sets or gets the Guid of the Component.
        /// </summary>
        public Guid Guid
        {
            get { return new Guid("70D99C53-7AF1-4098-A853-FCA94422F6B6"); }
        }

        #endregion

        private readonly List<IGameHandler> _subscribers;
        private float _updateTime;
        private float _renderTime;
        private float _targetFramesPerSecond;

        public SingleThreadGameLoop()
        {
            _subscribers = new List<IGameHandler>();
        }
        /// <summary>
        /// Internal GameLoop.
        /// </summary>
        private void InternalLoop()
        {
            var sw = new Stopwatch();
            while (IsRunning)
            {
                sw.Start();

                for (var index = 0; index <= _subscribers.Count - 1; index++)
                {
                    ProcessTick(_subscribers[index]);
                }

                for (var index = 0; index < _subscribers.Count; index++)
                {
                    var subscriber = _subscribers[index];
                    subscriber.Render(SGL.CurrentRenderer, _renderTime);
                }
                if (sw.ElapsedMilliseconds < TargetFrameTime)
                {
                    while (sw.ElapsedMilliseconds < TargetFrameTime)
                    {

                    }
                    _renderTime = TargetFrameTime;
                    _updateTime = TargetFrameTime;
                    sw.Stop();
                }
                else
                {
                    sw.Stop();
                    _renderTime = sw.ElapsedMilliseconds;
                    _updateTime = sw.ElapsedMilliseconds;
                }

                sw.Reset();
            }
            IsRunning = false;
        }
        /// <summary>
        /// Processes a Tick.
        /// </summary>
        /// <param name="gameHandler">The GameHandler.</param>
        private void ProcessTick(IGameHandler gameHandler)
        {
            lock (gameHandler)
            {
                gameHandler.Tick(_updateTime);
            }
        }

        /// <summary>
        /// Called if the FPS changed.
        /// </summary>
        private void OnFpsChanged()
        {
            var targetTime = 1000 / TargetFramesPerSecond;
            TargetFrameTime = targetTime;
            TargetUpdateTime = targetTime;
            SGL.GraphicsDevice.RefreshRate = TargetFramesPerSecond;

            //publish event
            SGL.Components.Get<EventManager>()
                .Publish(new TargetFrameTimeChangedEvent(TargetFramesPerSecond, targetTime));
        }
    }
}
