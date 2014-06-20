using System;

namespace Sharpex2D.Framework.Game
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Tested)]
    public class GameTime
    {
        /// <summary>
        ///     Gets the ElapsedGameTime.
        /// </summary>
        public float ElapsedGameTime { set; get; }

        /// <summary>
        ///     A value indicating whether the updateloop hangs behind. If your game encounter this, do something to catch up e.G.
        ///     disable physics.
        /// </summary>
        public bool IsRunningSlowly { set; get; }

        /// <summary>
        ///     Gets the TotalGameTime.
        /// </summary>
        public TimeSpan TotalGameTime { set; get; }
    }
}