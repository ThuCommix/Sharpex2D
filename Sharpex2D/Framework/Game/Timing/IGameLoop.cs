using Sharpex2D.Framework.Components;

namespace Sharpex2D.Framework.Game.Timing
{
    public interface IGameLoop : IComponent
    {
        /// <summary>
        /// Starts the GameLoop.
        /// </summary>
        void Start();
        /// <summary>
        /// Stops the GameLoop.
        /// </summary>
        void Stop();
        /// <summary>
        /// Gets the TargetFrameTime.
        /// </summary>
        float TargetFrameTime { get; }
        /// <summary>
        /// Gets the TargetUpdateTime.
        /// </summary>
        float TargetUpdateTime {  get; }
        /// <summary>
        /// Indicates whether the GameLoop is running.
        /// </summary>
        bool IsRunning { get; }
        /// <summary>
        /// Gets or sets the Target FPS.
        /// </summary>
        float TargetFramesPerSecond { set; get; }
        /// <summary>
        /// Subscribes a T GameHandler to the IGameLoop.
        /// </summary>
        /// <param name="gameHandler">The GameHandler</param>
        void Subscribe(IGameHandler gameHandler);
        /// <summary>
        /// Unsubscribes a T GameHandler from the IGameLoop.
        /// </summary>
        /// <param name="gameHandler">The GameHandler</param>
        void Unsubscribe(IGameHandler gameHandler);
    }
}
