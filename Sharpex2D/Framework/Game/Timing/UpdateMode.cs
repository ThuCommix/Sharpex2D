namespace Sharpex2D.Framework.Game.Timing
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Untested)]
    public enum UpdateMode
    {
        /// <summary>
        ///     Updates the timer via GameLoop.
        /// </summary>
        OnGameUpdate,

        /// <summary>
        ///     Updates the timer in a new thread.
        /// </summary>
        OnThreadUpdate
    }
}