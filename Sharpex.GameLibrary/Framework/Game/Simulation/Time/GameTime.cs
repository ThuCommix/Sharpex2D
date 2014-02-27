using System;
using SharpexGL.Framework.Game.Timing;
using SharpexGL.Framework.Rendering;

namespace SharpexGL.Framework.Game.Simulation.Time
{
    public class GameTime : IGameHandler
    {
        #region IComponent Implementation

        /// <summary>
        /// Sets or gets the Guid of the Component.
        /// </summary>
        public Guid Guid
        {
            get { return new Guid("292905FD-1B09-4D01-95D0-8D701A1C03FE"); }
        }

        #endregion

        #region IConstructable Implementation
        /// <summary>
        /// Constructs the Component.
        /// </summary>
        public void Construct()
        {
            SGL.Components.Get<IGameLoop>().Subscribe(this);
        }
        #endregion

        #region IGameHandler Implementation
        /// <summary>
        /// Processes a game tick.
        /// </summary>
        /// <param name="elapsed">The Elapsed.</param>
        public void Tick(float elapsed)
        {
            if (Mode == TimeMode.RealTime)
            {
                DayTime = DateTime.Now;
            }
            else
            {
                DayTime += TimeSpan.FromMilliseconds(elapsed/DayLength.TotalMilliseconds);
            }
        }
        /// <summary>
        /// Processes a render.
        /// </summary>
        /// <param name="renderer">The Renderer.</param>
        /// <param name="elapsed">The Elapsed.</param>
        public void Render(IRenderer renderer, float elapsed)
        {
        }
        #endregion

        #region GameTime Members

        /// <summary>
        /// Creates a new GameTime.
        /// </summary>
        public GameTime()
        {
            Mode = TimeMode.Simulated;
            DayLength = TimeSpan.FromMinutes(12);
        }

        /// <summary>
        /// Gets or sets the length of the simulated game day.
        /// </summary>
        public TimeSpan DayLength { set; get; }
        /// <summary>
        /// Gets the current time of the game day.
        /// </summary>
        public DateTime DayTime { private set; get; }
        /// <summary>
        /// Indicates whether the time should be simulated or real.
        /// </summary>
        public TimeMode Mode { set; get; }

        #endregion
    }
}
