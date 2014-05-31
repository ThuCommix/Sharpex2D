using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Sharpex2D.Framework.Game.Timing
{
    public class Gameloop : IGameLoop
    {
        #region IComponent Implementation
        /// <summary>
        /// Gets the Guid.
        /// </summary>
        public Guid Guid { get { return new Guid("C3966A60-F83F-400C-8F28-9928975F4908"); } }
        #endregion

        #region IGameLoop Implementation

        /// <summary>
        /// Starts the GameLoop.
        /// </summary>
        public void Start()
        {
            var renderTask = new Task(Rendering);
            var updateTask = new Task(Update);
            var waitTasks = new Task(WaitTasks, TaskCreationOptions.LongRunning);

            _taskpool.Add(renderTask);
            _taskpool.Add(updateTask);

            _renewTasks = true;

            updateTask.Start();
            renderTask.Start();
            waitTasks.Start();
        }
        /// <summary>
        /// Stops the GameLoop.
        /// </summary>
        public void Stop()
        {
            _renewTasks = false;
        }
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
        public bool IsRunning {
            get { return _renewTasks; }
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
                UpdateProperties();
            }
        }

        /// <summary>
        /// Subscribes a T GameHandler to the IGameLoop.
        /// </summary>
        /// <param name="gameHandler">The GameHandler</param>
        public void Subscribe(IGameHandler gameHandler)
        {
            _subscribers.Add(gameHandler);
        }
        /// <summary>
        /// Unsubscribes a T GameHandler from the IGameLoop.
        /// </summary>
        /// <param name="gameHandler">The GameHandler</param>
        public void Unsubscribe(IGameHandler gameHandler)
        {
            if (_subscribers.Contains(gameHandler))
            {
                lock (_subscribers)
                {
                    _subscribers.Remove(gameHandler);
                }
            }
        }

        #endregion

        private readonly List<IGameHandler> _subscribers;
        private readonly List<Task> _taskpool;
        private float _targetFramesPerSecond;
        private bool _renewTasks;
        private readonly Stopwatch _swRender;
        private readonly Stopwatch _swUpdate;
        private float _lastRenderTime = 16.6f;
        private float _lastUpdateTime = 16.6f;

        /// <summary>
        /// Initializes a new Gameloop class.
        /// </summary>
        public Gameloop()
        {
            _subscribers = new List<IGameHandler>();
            _taskpool = new List<Task>();

            _swRender = new Stopwatch();
            _swUpdate = new Stopwatch();
        }

        private void UpdateProperties()
        {
            TargetFrameTime = 1000/TargetFramesPerSecond;
            TargetUpdateTime = 1000/TargetFramesPerSecond;
        }

        /// <summary>
        /// The RenderingLoop.
        /// </summary>
        private void Rendering()
        {
            _swRender.Start();

            for (var i = 0; i <= _subscribers.Count - 1; i++)
            {
                var subsriber = _subscribers[i] as Game;
                if (subsriber != null)
                {
                    lock (subsriber)
                    {
                        subsriber.OnRendering(SGL.CurrentRenderer, _lastRenderTime);
                    }
                }
            }

            var waitFlag = false;

            while (_swRender.ElapsedMilliseconds < TargetFrameTime)
            {
                waitFlag = true;
            }

            _swRender.Stop();
            _lastRenderTime = waitFlag ? TargetFrameTime : _swUpdate.ElapsedMilliseconds;
            _swRender.Reset();
        }
        /// <summary>
        /// The UpdateLoop.
        /// </summary>
        private void Update()
        {
            _swUpdate.Start();

            for (var i = 0; i <= _subscribers.Count - 1; i++)
            {
                var subsriber = _subscribers[i];
                lock (subsriber)
                {
                    subsriber.Tick(_lastUpdateTime);
                }
            }

            var waitFlag = false;

            while (_swUpdate.ElapsedMilliseconds < TargetUpdateTime)
            {
                waitFlag = true;
            }

            _swUpdate.Stop();
            _lastUpdateTime = waitFlag ? TargetUpdateTime : _swUpdate.ElapsedMilliseconds;
            _swUpdate.Reset();
        }
        /// <summary>
        /// Waits for the loops to complete.
        /// </summary>
        private void WaitTasks()
        {
            while (_renewTasks)
            {
                Task.WaitAll(_taskpool.ToArray());

                _taskpool.Clear();

                var renderTask = new Task(Rendering);
                var updateTask = new Task(Update);
                _taskpool.Add(renderTask);
                _taskpool.Add(updateTask);
                updateTask.Start();
                renderTask.Start();
            }
        }
    }
}
