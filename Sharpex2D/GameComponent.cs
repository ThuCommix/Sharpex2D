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
    public abstract class GameComponent : IDisposable, IUpdateable
    {
        private bool _enabled;
        private int _updateOrder;

        /// <summary>
        /// Initializes a new GameComponent class.
        /// </summary>
        /// <param name="game">The Game.</param>
        protected GameComponent(Game game)
        {
            Game = game;
        }

        /// <summary>
        /// Gets the game associated with this component.
        /// </summary>
        public Game Game { private set; get; }

        /// <summary>
        /// A value indicating whether the component is enabled.
        /// </summary>
        public bool Enabled
        {
            set
            {
                if (_enabled != value)
                {
                    _enabled = value;
                    if (EnabledChanged != null)
                        EnabledChanged(this, EventArgs.Empty);
                }
            }
            get { return _enabled; }
        }

        /// <summary>
        /// Gets or sets the update order.
        /// </summary>
        public int UpdateOrder
        {
            set
            {
                if (value != _updateOrder)
                {
                    _updateOrder = value;
                    if (UpdateOrderChanged != null)
                        UpdateOrderChanged(this, EventArgs.Empty);
                }
            }
            get { return _updateOrder; }
        }

        /// <summary>
        /// Disposes the object.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);

            if (Disposed != null)
                Disposed(this, EventArgs.Empty);
        }

        /// <summary>
        /// Updates the component.
        /// </summary>
        /// <param name="gameTime">The GameTime.</param>
        public virtual void Update(GameTime gameTime)
        {
        }

        /// <summary>
        /// Raises when the component is disposed.
        /// </summary>
        public event EventHandler<EventArgs> Disposed;

        /// <summary>
        /// Raises when the enabled state changed.
        /// </summary>
        public event EventHandler<EventArgs> EnabledChanged;

        /// <summary>
        /// Raises when the update order changed.
        /// </summary>
        public event EventHandler<EventArgs> UpdateOrderChanged;

        /// <summary>
        /// Deconstructs the GameComponent class.
        /// </summary>
        ~GameComponent()
        {
            Dispose(false);
        }

        /// <summary>
        /// Updates the component.
        /// </summary>
        /// <param name="gameTime">The GameTime.</param>
        internal void UpdateComponent(GameTime gameTime)
        {
            if (Enabled)
                Update(gameTime);
        }

        /// <summary>
        /// Initializes the component.
        /// </summary>
        public virtual void Initialize()
        {
        }

        /// <summary>
        /// Disposes the object.
        /// </summary>
        /// <param name="disposing">The disposing state.</param>
        public virtual void Dispose(bool disposing)
        {
        }
    }
}
