
namespace SharpexGL.Framework.Game.Timing
{
    public enum UpdateMode
    {
        /// <summary>
        /// Updates the timer via GameLoop.
        /// </summary>
        OnGameTick,
        /// <summary>
        /// Updates the timer in a new thread.
        /// </summary>
        OnThreadTick
    }
}
