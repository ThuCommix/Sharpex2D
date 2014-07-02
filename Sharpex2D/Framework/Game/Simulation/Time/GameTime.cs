// Copyright (c) 2012-2014 Sharpex2D - Kevin Scholz (ThuCommix)
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the 'Software'), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED 'AS IS', WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using System;
using Sharpex2D.Framework.Game.Timing;

namespace Sharpex2D.Framework.Game.Simulation.Time
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
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
        ///     Updates the object.
        /// </summary>
        /// <param name="gameTime">The GameTime.</param>
        public void Update(Framework.Game.GameTime gameTime)
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