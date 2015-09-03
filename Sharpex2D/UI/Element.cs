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

using System.Collections;
using System.Collections.Generic;
using Sharpex2D.Framework.Rendering;

namespace Sharpex2D.Framework.UI
{
    public abstract class Element : IEnumerable<Element>
    {
        private readonly List<Element> _elements;

        /// <summary>
        /// Initializes a new Element class.
        /// </summary>
        protected Element()
        {
            _elements = new List<Element>();
            Position = new Vector2(0, 0);
            Bounds = Rectangle.Empty;
            Visible = true;
            Enabled = true;
        }

        /// <summary>
        /// Gets or sets the bounds.
        /// </summary>
        public Rectangle Bounds { set; get; }

        /// <summary>
        /// Gets or sets the position.
        /// </summary>
        public Vector2 Position { set; get; }

        /// <summary>
        /// A value indicating whether the element is visible.
        /// </summary>
        public bool Visible { set; get; }

        /// <summary>
        /// A value indicating whether the element is enabled.
        /// </summary>
        public bool Enabled { set; get; }

        /// <summary>
        /// A value indicating whether the element has the focus.
        /// </summary>
        public bool HasFocus { private set; get; }

        /// <summary>
        /// Gets the children elements.
        /// </summary>
        public Element[] Elements => _elements.ToArray();

        /// <summary>
        /// Gets the parent element.
        /// </summary>
        public Element Parent { private set; get; }

        /// <summary>
        /// Gets the enumerator.
        /// </summary>
        /// <returns>IEnumerator.</returns>
        public IEnumerator<Element> GetEnumerator()
        {
            return _elements.GetEnumerator();
        }

        /// <summary>
        /// Gets the enumerator.
        /// </summary>
        /// <returns>IEnumerator.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// Draws the element.
        /// </summary>
        /// <param name="spriteBatch">The SpriteBatch.</param>
        /// <param name="gameTime">The GameTime.</param>
        internal void DrawElement(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (!Visible)
                return;

            OnDraw(spriteBatch, gameTime);

            foreach (var control in _elements)
                control.OnDraw(spriteBatch, gameTime);
        }

        /// <summary>
        /// Updates the element.
        /// </summary>
        /// <param name="gameTime">The GameTime.</param>
        internal void UpdateElement(GameTime gameTime)
        {
            if (!Visible || !Enabled)
                return;

            OnUpdate(gameTime);

            foreach (var control in _elements)
                control.OnUpdate(gameTime);
        }

        /// <summary>
        /// Raises if the control should be drawn.
        /// </summary>
        /// <param name="spriteBatch">The SpriteBatch.</param>
        /// <param name="gameTime">The GameTime.</param>
        public virtual void OnDraw(SpriteBatch spriteBatch, GameTime gameTime)
        {
        }

        /// <summary>
        /// Raises if the control should be updated.
        /// </summary>
        /// <param name="gameTime">The GameTime.</param>
        public virtual void OnUpdate(GameTime gameTime)
        {
        }
    }
}
