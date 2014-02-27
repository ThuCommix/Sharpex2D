using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SharpexGL.Framework.Events;
using SharpexGL.Framework.Game.Timing.Events;

namespace SharpexGL.Framework.Game.Timing
{
    public class DualThreadGameLoop : IGameLoop
    {
        #region IComponent Implementation

        /// <summary>
        /// Sets or gets the Guid of the Component.
        /// </summary>
        public Guid Guid
        {
            get { return new Guid("545D6C69-9340-46BE-A102-5F9E283DE04B"); }
        }

        #endregion

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
        /// Gets or sets the Target FPS.
        /// </summary>
        public float TargetFramesPerSecond
        {
            get { return _targetFramesPerSecond; }
            set
            {
                _targetFramesPerSecond = value;
                OnFpsChanged();

            }
        }

        /// <summary>
        /// Sets or gets the RenderMode.
        /// </summary>
        public RenderMode RenderMode { set; get; }

        /// <summary>
        /// Starts the GameLoop.
        /// </summary>
        public void Start()
        {
            //Calculating the TargetTime
            var targetTime = 1000/TargetFramesPerSecond;
            TargetFrameTime = targetTime;
            TargetUpdateTime = targetTime;
            _cancelFlag = false;
            _renderTask = Task.Factory.StartNew(InternalRenderingLoop);
            _updateTask = Task.Factory.StartNew(InternalUpdateLoop);
        }

        /// <summary>
        /// Stops the GameLoop.
        /// </summary>
        public void Stop()
        {
            _cancelFlag = true;
        }

        /// <summary>
        /// Subscribes a GameHandler to the IGameLoop.
        /// </summary>
        /// <param name="gameHandler">The GameHandler</param>
        public void Subscribe(IGameHandler gameHandler)
        {
            _subscribers.Add(gameHandler);
        }

        /// <summary>
        /// Unsubscribes a GameHandler from the IGameLoop.
        /// </summary>
        /// <param name="gameHandler">The GameHandler</param>
        public void Unsubscribe(IGameHandler gameHandler)
        {
            if (_subscribers.Contains(gameHandler))
            {
                _subscribers.Remove(gameHandler);
            }
        }

        #endregion

        #region Fields

        private bool _cancelFlag;
        private Task _updateTask;
        private Task _renderTask;
        private float _updateTime;
        private float _targetFramesPerSecond;
        private float _renderTime;
        private float _unprocessedTicks;
        private readonly List<IGameHandler> _subscribers = new List<IGameHandler>();

        #endregion

        #region Internal

        public DualThreadGameLoop()
        {
            RenderMode = RenderMode.Limited;
        }

        /// <summary>
        /// Called if the FPS changed.
        /// </summary>
        private void OnFpsChanged()
        {
            var targetTime = 1000/TargetFramesPerSecond;
            TargetFrameTime = targetTime;
            TargetUpdateTime = targetTime;
            SGL.GraphicsDevice.RefreshRate = TargetFramesPerSecond;

            //publish event
            SGL.Components.Get<EventManager>()
                .Publish(new TargetFrameTimeChangedEvent(TargetFramesPerSecond, targetTime));
        }

        /// <summary>
        /// Handles the update loop.
        /// </summary>
        private void InternalUpdateLoop()
        {
            var sw = new System.Diagnostics.Stopwatch();
            while (!_cancelFlag)
            {
                //Check unprocessedTicks, if they fill a Frame suppress render and update
                if (_unprocessedTicks > TargetUpdateTime)
                {
                    var lostFrames = (int) (_unprocessedTicks/TargetUpdateTime);
                    for (var i = 1; i <= lostFrames; i++)
                    {
                        //Process a tick in every subscriber
                        for (int index = 0; index < _subscribers.Count; index++)
                        {
                            var subscriber = _subscribers[index];
                            lock (subscriber)
                            {
                                subscriber.Tick(_updateTime);
                            }
                        }
                    }
                    //Reset the unprocessedTicks
                    _unprocessedTicks = 0;
                }

                sw.Start();
                //Process a tick in every subscriber
                for (var index = 0; index < _subscribers.Count; index++)
                {
                    var subscriber = _subscribers[index];
                    lock (subscriber)
                    {
                        subscriber.Tick(_updateTime);
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
                }
                else
                {
                    //The UpdateTask takes to long
                    _unprocessedTicks += (_updateTime - TargetUpdateTime);
                }
            }
        }

        /// <summary>
        /// Handles the rendering loop.
        /// </summary>
        private void InternalRenderingLoop()
        {
            var sw = new System.Diagnostics.Stopwatch();
            while (!_cancelFlag)
            {
                sw.Start();

                //Process a render in every subscriber
                for (int index = 0; index < _subscribers.Count; index++)
                {
                    var subscriber = _subscribers[index];
                    subscriber.Render(SGL.CurrentRenderer, _renderTime);
                }
                if (SGL.CurrentRenderer.VSync)
                {
                    if (sw.ElapsedMilliseconds < 15)
                        _renderTask.Wait(15 - (int) sw.ElapsedMilliseconds);
                    while (sw.ElapsedMilliseconds < TargetFrameTime)
                    {
                    }
                    _renderTime = TargetFrameTime;
                    sw.Stop();
                    sw.Reset();
                    return;
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
                    }
                    //If the RenderTask takes to long, there is no solution, we can't make the system better as it is
                }
            }
        }

        #endregion
    }
}
