using Sharpex2D.Framework.Components;

namespace Sharpex2D.Framework.Game.Timing
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Tested)]
    public interface IGameLoop : IComponent
    {
        /// <summary>
        ///     Gets or sets the TargetFrameTime.
        /// </summary>
        float TargetFrameTime { get; set; }

        /// <summary>
        ///     Gets or sets the TargetUpdateTime.
        /// </summary>
        float TargetUpdateTime { get; set; }

        /// <summary>
        ///     Indicates whether the GameLoop is running.
        /// </summary>
        bool IsRunning { get; }

        /// <summary>
        ///     Gets the Target FPS.
        /// </summary>
        float TargetFramesPerSecond { get; }

        /// <summary>
        ///     Starts the GameLoop.
        /// </summary>
        void Start();

        /// <summary>
        ///     Stops the GameLoop.
        /// </summary>
        void Stop();

        /// <summary>
        ///     Subscribes a IDrawable to the game loop.
        /// </summary>
        /// <param name="drawable">The IDrawable.</param>
        void Subscribe(IDrawable drawable);

        /// <summary>
        ///     Unsubscribes a IDrawable from the game loop.
        /// </summary>
        /// <param name="drawable">The IDrawable.</param>
        void Unsubscribe(IDrawable drawable);

        /// <summary>
        ///     Subscribes a IUpdateable to the game loop.
        /// </summary>
        /// <param name="updateable">The IDrawable.</param>
        void Subscribe(IUpdateable updateable);

        /// <summary>
        ///     Unsubscribes a IUpdateable from the game loop.
        /// </summary>
        /// <param name="updateable">The IUpdateable.</param>
        void Unsubscribe(IUpdateable updateable);
    }
}