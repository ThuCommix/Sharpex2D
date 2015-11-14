// Copyright (c) 2012-2015 Sharpex2D - Kevin Scholz (ThuCommix)
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

namespace Sharpex2D.Framework
{
    public class GameTimer : IUpdateable
    {
        private bool _enabled;
        private float _passedms;

        /// <summary>
        /// Initializes a new GameTimer class.
        /// </summary>
        public GameTimer() : this(100)
        {
        }

        /// <summary>
        /// Initializes a new GameTimer class.
        /// </summary>
        /// <param name="interval">The Interval.</param>
        public GameTimer(float interval)
        {
            Interval = interval;
        }

        /// <summary>
        /// A value indicating whether the GameTimer is enabled.
        /// </summary>
        public bool Enabled
        {
            set
            {
                _enabled = value;
                if (value)
                {
                    GameHost.Get<GameLoop>().Subscribe(this);
                }
                else
                {
                    GameHost.Get<GameLoop>().Unsubscribe(this);
                }
            }
            get { return _enabled; }
        }

        /// <summary>
        /// Gets or sets the Interval.
        /// </summary>
        public float Interval { set; get; }

        /// <summary>
        /// Updates the object.
        /// </summary>
        /// <param name="gameTime">The GameTime.</param>
        void IUpdateable.Update(GameTime gameTime)
        {
            if (Enabled)
            {
                _passedms += gameTime.ElapsedGameTime;

                if (_passedms >= Interval)
                {
                    _passedms = 0;
                    Ticked?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        /// <summary>
        /// Ticked event.
        /// </summary>
        public event EventHandler<EventArgs> Ticked;
    }
}
