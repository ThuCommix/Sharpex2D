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

namespace Sharpex2D.Framework.Rendering
{
    public class FadeInOutTransition : ISceneTransition
    {
        /// <summary>
        /// Raises when the transition is completed
        /// </summary>
        public event EventHandler<EventArgs> TransitionCompleted;

        /// <summary>
        /// Raises when the scene should be changed
        /// </summary>
        public event EventHandler<EventArgs> ChangeScene;

        private Color _transitionFrom;
        private Color _transitionTo;
        private readonly float _durationOut;
        private readonly float _durationIn;
        private readonly float _transitionFromAlphaStep;
        private readonly float _transitionToAlphaStep;
        private readonly float _transitionR;
        private readonly float _transitionG;
        private readonly float _transitionB;
        private bool _flag;
        private float _elapsed;
        private float _currentAlpha;
        private float _r;
        private float _g;
        private float _b;
        private readonly Texture2D _pixel;
        private readonly Rectangle _view;

        /// <summary>
        /// Initializes a new FadeInOutTransition class
        /// </summary>
        /// <param name="duration">The duration</param>
        public FadeInOutTransition(float duration) : this(Color.Black, Color.Black, duration/2f, duration/2f)
        {
            
        }

        /// <summary>
        /// Initializes a new FadeInOutTransition class
        /// </summary>
        /// <param name="durationOut">The fading out duration</param>
        /// <param name="durationIn">The fading in duration</param>
        public FadeInOutTransition(float durationOut, float durationIn) : this(Color.Black, Color.Black, durationOut, durationIn)
        {
            
        }

        /// <summary>
        /// Initializes a new FadeInOutTransition class
        /// </summary>
        /// <param name="transitionColor">The transition color</param>
        /// <param name="duration">The duration</param>
        public FadeInOutTransition(Color transitionColor, float duration) : this(transitionColor, transitionColor, duration/2f, duration/2f)
        {
                
        }

        /// <summary>
        /// Initializes a new FadeInOutTransition class
        /// </summary>
        /// <param name="transitionColor">The transition color</param>
        /// <param name="durationOut">The fading out duration</param>
        /// <param name="durationIn">The fading in duration</param>
        public FadeInOutTransition(Color transitionColor, float durationOut, float durationIn) : this(transitionColor, transitionColor, durationOut, durationIn)
        {
            
        }

        /// <summary>
        /// Initializes a new FadeInOutTransition class
        /// </summary>
        /// <param name="transitionFrom">The transition from color</param>
        /// <param name="transitionTo">The transition to color</param>
        /// <param name="duration">The duration</param>
        public FadeInOutTransition(Color transitionFrom, Color transitionTo, float duration) : this(transitionFrom, transitionTo, duration/2f, duration/2f)
        {
            
        }

        /// <summary>
        /// Initializes a new FadeInOutTransition class
        /// </summary>
        /// <param name="transitionFrom">The transition from color</param>
        /// <param name="transitionTo">The transition to color</param>
        /// <param name="durationOut">The fading out duration</param>
        /// <param name="durationIn">The fading in duration</param>
        public FadeInOutTransition(Color transitionFrom, Color transitionTo, float durationOut, float durationIn)
        {
            _transitionFrom = transitionFrom;
            _transitionTo = transitionTo;
            _durationOut = durationOut;
            _durationIn = durationIn;

            _transitionFromAlphaStep = 255f/durationOut;
            _transitionToAlphaStep = 255f/durationIn;

            _transitionR = (transitionTo.R - transitionFrom.R)/durationIn;
            _transitionG = (transitionTo.G - transitionFrom.G)/durationIn;
            _transitionB = (transitionTo.B - transitionFrom.B)/durationIn;

            _r = transitionFrom.R;
            _g = transitionFrom.G;
            _b = transitionFrom.B;

            _transitionTo = Color.FromArgb(255, transitionFrom.R, transitionFrom.G, _transitionFrom.B);

            _pixel = new Texture2D(1, 1);
            _pixel.Lock();
            _pixel[0, 0] = Color.White;
            _pixel.Unlock();

            var graphicsManager = GameHost.Get<GraphicsDevice>().GraphicsManager;
            _view = new Rectangle(0, 0, graphicsManager.PreferredBackBufferWidth,
                graphicsManager.PreferredBackBufferHeight);
        }

        /// <summary>
        /// Updates the transition
        /// </summary>
        /// <param name="gameTime">The game time</param>
        public void Update(GameTime gameTime)
        {
            _elapsed += gameTime.ElapsedGameTime;

            if (!_flag)
            {
                if (_elapsed >= _durationOut)
                {
                    _elapsed = 0;
                    _flag = true;
                    ChangeScene?.Invoke(this, EventArgs.Empty);
                }
                else
                {
                    _currentAlpha += _transitionFromAlphaStep*gameTime.ElapsedGameTime;
                    _transitionFrom = Color.FromArgb((byte) _currentAlpha, _transitionFrom.R, _transitionFrom.G,
                        _transitionFrom.B);
                }
            }
            else
            {
                if (_elapsed >= _durationIn)
                {
                    TransitionCompleted?.Invoke(this, EventArgs.Empty);
                }
                else
                {
                    _currentAlpha -= _transitionToAlphaStep * gameTime.ElapsedGameTime;
                    _r += _transitionR*gameTime.ElapsedGameTime;
                    _g += _transitionG*gameTime.ElapsedGameTime;
                    _b += _transitionB*gameTime.ElapsedGameTime;

                    _transitionTo = Color.FromArgb((byte) _currentAlpha, (byte) _r, (byte) _g, (byte) _b);
                }
            }
        }

        /// <summary>
        /// Draws the transition
        /// </summary>
        /// <param name="spriteBatch">The sprite batch</param>
        /// <param name="gameTime">The game time</param>
        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.DrawTexture(_pixel, _view, _flag ? _transitionTo : _transitionFrom);
        }
    }
}
