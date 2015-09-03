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
using Sharpex2D.Framework.Content;
using Sharpex2D.Framework.Rendering;

namespace Sharpex2D.Framework
{
    public abstract class DrawableGameComponent : GameComponent, IDrawable
    {
        private int _drawOrder;
        private bool _visible;

        /// <summary>
        /// Initializes a new DrawableGameComponent class.
        /// </summary>
        /// <param name="game">The Game.</param>
        protected DrawableGameComponent(Game game) : base(game)
        {
        }

        /// <summary>
        /// Gets or sets the draw order.
        /// </summary>
        public int DrawOrder
        {
            set
            {
                if (_drawOrder != value)
                {
                    _drawOrder = value;
                    DrawOrderChanged?.Invoke(this, EventArgs.Empty);
                }
            }
            get { return _drawOrder; }
        }

        /// <summary>
        /// A value indicating whether the component is visible.
        /// </summary>
        public bool Visible
        {
            set
            {
                if (_visible != value)
                {
                    _visible = value;
                    VisibleChanged?.Invoke(this, EventArgs.Empty);
                }
            }
            get { return _visible; }
        }

        /// <summary>
        /// Draws the component.
        /// </summary>
        /// <param name="spriteBatch">The SpriteBatch.</param>
        /// <param name="gameTime">The GameTime.</param>
        public virtual void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
        }

        /// <summary>
        /// Raises when the draw order changed.
        /// </summary>
        public event EventHandler<EventArgs> DrawOrderChanged;

        /// <summary>
        /// Raises when the visible state changed.
        /// </summary>
        public event EventHandler<EventArgs> VisibleChanged;

        /// <summary>
        /// Draws the component.
        /// </summary>
        /// <param name="spriteBatch">The SpriteBatch.</param>
        /// <param name="gameTime">The GameTime.</param>
        internal void DrawComponent(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (Visible)
                Draw(spriteBatch, gameTime);
        }

        /// <summary>
        /// Loads the content.
        /// </summary>
        /// <param name="content">The ContentManager.</param>
        public virtual void LoadContent(ContentManager content)
        {
        }

        /// <summary>
        /// Unloads the content.
        /// </summary>
        public virtual void UnloadContent()
        {
        }
    }
}
