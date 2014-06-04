using System;
using Sharpex2D.Framework.Game.Timing;

namespace Sharpex2D.Framework.Game.Simulation.Time
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Tested)]
    public class GameTime : IUpdateable
    {
        #region IComponent Implementation

        /// <summary>
        ///     Sets or gets the Guid of the Component.
        /// </summary>
        public Guid Guid
        {
            get { return new Guid("292905FD-1B09-4D01-95D0-8D701A1C03FE"); }
        }

        #endregion

        #region IConstructable Implementation

        /// <summary>
        ///     Constructs the Component.
        /// </summary>
        public void Construct()
        {
            SGL.Components.Get<IGameLoop>().Subscribe(this);
        }

        #endregion

        #region IGameHandler Implementation

        /// <summary>
        ///     Processes a game tick.
        /// </summary>
        /// <param name="gameTime">The GameTime.</param>
        public void Tick(Framework.Game.GameTime gameTime)
        {
            if (Mode == TimeMode.RealTime)
            {
                DayTime = DateTime.Now;
            }
            else
            {
                DayTime += TimeSpan.FromMilliseconds(gameTime.ElapsedGameTime/DayLength.TotalMilliseconds);
            }
        }

        #endregion

        #region GameTime Members

        /// <summary>
        ///     Creates a new GameTime.
        /// </summary>
        public GameTime()
        {
            Mode = TimeMode.Simulated;
            DayLength = TimeSpan.FromMinutes(12);
        }

        /// <summary>
        ///     Gets or sets the length of the simulated game day.
        /// </summary>
        public TimeSpan DayLength { set; get; }

        /// <summary>
        ///     Gets the current time of the game day.
        /// </summary>
        public DateTime DayTime { private set; get; }

        /// <summary>
        ///     Indicates whether the time should be simulated or real.
        /// </summary>
        public TimeMode Mode { set; get; }

        #endregion
    }
}