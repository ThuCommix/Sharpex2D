using System.Collections.Generic;
using System.Threading.Tasks;

namespace SharpexGL.Framework.Game.Timing
{
    public class GameLoop : IGameLoop<IGameHandler>
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
        public float TargetFramesPerSecond { get; set; }
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
            _subscribers.Remove(gameHandler);
        }

        #endregion

        #region Fields

        private bool _cancelFlag;
        private Task _updateTask;
        private Task _renderTask;
        private float _updateTime;
        private float _renderTime;
        private float _unprocessedTicks;
        private bool _suppressRender;
        private readonly List<IGameHandler> _subscribers = new List<IGameHandler>();

        #endregion

        #region Internal

        private void InternalUpdateLoop()
        {
            var sw = new System.Diagnostics.Stopwatch();
            while (!_cancelFlag)
            {
                //Check unprocessedTicks, if they fill a Frame suppress render and update
                if (_unprocessedTicks > TargetUpdateTime)
                {
                    _suppressRender = true;
                    var lostFrames = (int) (_unprocessedTicks/TargetUpdateTime);
                    for (var i = 1; i <= lostFrames; i++)
                    {
                        //Process a tick in every subscriber
                        foreach (var subscriber in _subscribers)
                        {
                            subscriber.Tick(_updateTime);
                        }
                    }
                    //Unlock the render
                    _suppressRender = false;
                    //Reset the unprocessedTicks
                    _unprocessedTicks = 0;
                }
                sw.Start();
                //Process a tick in every subscriber
                foreach (var subscriber in _subscribers)
                {
                    subscriber.Tick(_updateTime);
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

        private void InternalRenderingLoop()
        {
            var sw = new System.Diagnostics.Stopwatch();
            while (!_cancelFlag)
            {
                sw.Start();
                //Only render, if the rendering is not suppressed
                if (!_suppressRender)
                {
                    //Process a render in every subscriber
                    foreach (var subscriber in _subscribers)
                    {
                        subscriber.Render(SGL.CurrentRenderer, _renderTime);
                    }
                }
                sw.Stop();
                _renderTime = sw.ElapsedMilliseconds;
                sw.Reset();
                //Check if the render was shorter than TargetFrameTime
                if (_renderTime < TargetFrameTime)
                {
                    //Wait to sync
                    _renderTask.Wait((int) (TargetFrameTime - _renderTime));
                }
                //If the RenderTask takes to long, there is no solution, we can't make the system better as it is
            }
        }

        #endregion
    }
}
