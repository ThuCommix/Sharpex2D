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
using System.Linq;
using Sharpex2D.Framework.Rendering;

namespace Sharpex2D.Framework.UI
{
    public abstract class Element : IEnumerable<Element>
    {
        private readonly List<Element> _elements;
        private bool _enabled;
        private bool _visible;

        /// <summary>
        /// Initializes a new Element class.
        /// </summary>
        protected Element()
        {
            _elements = new List<Element>();
            Bounds = Rectangle.Empty;
            Visible = true;
            Enabled = true;
            CanGetFocus = true;
        }

        /// <summary>
        /// Gets the element manager
        /// </summary>
        internal ElementManager UIElementManager { set; get; }

        /// <summary>
        /// Gets or sets the bounds.
        /// </summary>
        public Rectangle Bounds { set; get; }

        /// <summary>
        /// A value indicating whether the element is visible.
        /// </summary>
        public bool Visible
        {
            set
            {
                if (value == _visible) return;

                _visible = value;

                foreach (var element in _elements)
                {
                    element.Visible = value;
                }
            }
            get { return _visible; }
        }

        /// <summary>
        /// A value indicating whether the element is enabled.
        /// </summary>
        public bool Enabled
        {
            set
            {
                if (value == _enabled) return;

                _enabled = value;

                foreach (var element in _elements)
                {
                    element.Enabled = value;
                }
            }
            get { return _enabled; }
        }

        /// <summary>
        /// A value indicating whether the element has the focus.
        /// </summary>
        public bool HasFocus => UIElementManager?.FocusedElement == this;

        /// <summary>
        /// Gets the children elements.
        /// </summary>
        public Element[] Children => _elements.ToArray();

        /// <summary>
        /// Gets the parent element.
        /// </summary>
        public Element Parent { private set; get; }

        /// <summary>
        /// A value indicating whether the element is the root element
        /// </summary>
        public bool IsRoot => Parent == null;

        /// <summary>
        /// A value indicating whether the element can get the focus
        /// </summary>
        public bool CanGetFocus { set; get; }

        /// <summary>
        /// Gets the Z-Index
        /// </summary>
        public int ZIndex { set; get; }

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
        /// Sets the focus of the element
        /// </summary>
        /// <remarks></remarks>
        public void SetFocus()
        {
            if (CanGetFocus)
            {
                UIElementManager?.SetFocus(this);
            }
        }

        /// <summary>
        /// Gets a value indicating whether the position intersects with this element
        /// </summary>
        /// <param name="positon">The position</param>
        /// <returns>True when intersecting</returns>
        public bool Intersects(Vector2 positon)
        {
            return Bounds.Contains(positon);
        }

        /// <summary>
        /// Gets a value indicating whether the rectangle intersects with this element
        /// </summary>
        /// <param name="rectangle">The rectangle</param>
        /// <returns>True when intersecting</returns>
        public bool Intersects(Rectangle rectangle)
        {
            return Bounds.Intersects(rectangle);
        }

        /// <summary>
        /// Gets a value indicating whether the position intersects with any child element ordered by the z index
        /// </summary>
        /// <param name="element">The element</param>
        /// <param name="positon">The position</param>
        /// <returns>True when intersecting</returns>
        public bool Intersects(Vector2 positon, out Element element)
        {
            element = _elements.Where(rootElement => rootElement.Intersects(positon)).Where(x => x.Visible).OrderBy(x => x.ZIndex).FirstOrDefault();
            return element != null;
        }

        /// <summary>
        /// Gets a value indicating whether the rectangle intersects with any child element ordered by the z index
        /// </summary>
        /// <param name="element">The element</param>
        /// <param name="rectangle">The rectangle</param>
        /// <returns>True when intersecting</returns>
        public bool Intersects(Rectangle rectangle, out Element element)
        {
            element = _elements.Where(rootElement => rootElement.Intersects(rectangle)).Where(x => x.Visible).OrderBy(x => x.ZIndex).FirstOrDefault();
            return element != null;
        }

        /// <summary>
        /// Finds the deepest intersecting element ordered by z index.
        /// </summary>
        /// <param name="rectangle">The rectangle</param>
        /// <param name="element">The element</param>
        /// <returns>True on success</returns>
        public bool DeepIntersects(Rectangle rectangle, out Element element)
        {
            if (Intersects(rectangle, out element))
            {
                var lastElement = element;
                while (element.Intersects(rectangle, out element))
                {
                    lastElement = element;
                }

                element = lastElement;
            }

            return false;
        }

        /// <summary>
        /// Finds the deepest intersecting element ordered by z index.
        /// </summary>
        /// <param name="position">The position</param>
        /// <param name="element">The element</param>
        /// <returns>True on success</returns>
        public bool DeepIntersects(Vector2 position, out Element element)
        {
            if (Intersects(position, out element))
            {
                var lastElement = element;
                while (element.Intersects(position, out element))
                {
                    lastElement = element;
                }

                element = lastElement;
            }

            return false;
        }

        /// <summary>
        /// Gets a value indicating whether the rectangle intersects with the specified child element
        /// </summary>
        /// <param name="element">The element</param>
        /// <param name="rectangle">The rectangle</param>
        /// <returns>True on success</returns>
        public bool Intersects(Element element, Rectangle rectangle)
        {
            Element intersectElement;
            if (Intersects(rectangle, out intersectElement))
            {
                return intersectElement == element;
            }

            return false;
        }

        /// <summary>
        /// Gets a value indicating whether the position intersects with the specified child element
        /// </summary>
        /// <param name="element">The element</param>
        /// <param name="position">The position</param>
        /// <returns>True on success</returns>
        public bool Intersects(Element element, Vector2 position)
        {
            Element intersectElement;
            if (Intersects(position, out intersectElement))
            {
                return intersectElement == element;
            }

            return false;
        }

        /// <summary>
        /// Adds a new element
        /// </summary>
        /// <param name="element">The element</param>
        public void AddChild(Element element)
        {
            element.Parent = this;
            element.Enabled = Enabled;
            element.Visible = Visible;

            _elements.Add(element);
        }

        /// <summary>
        /// Removes an element
        /// </summary>
        /// <param name="element">The element</param>
        /// <returns>True on success</returns>
        public bool RemoveChild(Element element)
        {
            var result = _elements.Remove(element);
            if (result)
                element.Parent = null;

            return result;
        }

        /// <summary>
        /// Draws the element.
        /// </summary>
        /// <param name="spriteBatch">The SpriteBatch.</param>
        /// <param name="gameTime">The GameTime.</param>
        internal void InternalDraw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (!Visible)
                return;

            Draw(spriteBatch, gameTime);

            foreach (var control in _elements)
                control.Draw(spriteBatch, gameTime);
        }

        /// <summary>
        /// Updates the element.
        /// </summary>
        /// <param name="gameTime">The GameTime.</param>
        internal void InternalUpdate(GameTime gameTime)
        {
            if (!Visible || !Enabled)
                return;

            Update(gameTime);

            foreach (var control in _elements)
                control.Update(gameTime);
        }

        /// <summary>
        /// Raises when the element receives input
        /// </summary>
        /// <param name="inputState">The input state</param>
        internal void InternalInputReceived(InputState inputState)
        {
            if (Enabled)
            {
                InputStateReceived(inputState);
            }
        }

        /// <summary>
        /// Raises when the control should be drawn.
        /// </summary>
        /// <param name="spriteBatch">The SpriteBatch.</param>
        /// <param name="gameTime">The GameTime.</param>
        public virtual void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
        }

        /// <summary>
        /// Raises when the control should be updated.
        /// </summary>
        /// <param name="gameTime">The GameTime.</param>
        public virtual void Update(GameTime gameTime)
        {
        }

        /// <summary>
        /// Raises when the element receives input
        /// </summary>
        /// <param name="inputState">The input state</param>
        public virtual void InputStateReceived(InputState inputState)
        {           
        }
    }
}
