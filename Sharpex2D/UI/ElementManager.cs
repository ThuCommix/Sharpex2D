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
    public class ElementManager : DrawableGameComponent, IEnumerable<Element>
    {
        private readonly List<Element> _rootElements;

        /// <summary>
        /// Gets the root elements
        /// </summary>
        public Element[] RootElements => _rootElements.ToArray();

        /// <summary>
        /// Gets the current focused element
        /// </summary>
        public Element FocusedElement { get; private set; }

        /// <summary>
        /// Initializes a new ElementManager class
        /// </summary>
        /// <param name="game">The game</param>
        public ElementManager(Game game) : base(game)
        {
            _rootElements = new List<Element>();
        }

        /// <summary>
        /// Adds a new root element
        /// </summary>
        /// <param name="element">The element</param>
        public void AddRootElement(Element element)
        {
            if (!element.IsRoot) return;

            element.ElementManager = this;
            _rootElements.Add(element);
        }

        /// <summary>
        /// Removes the a root element
        /// </summary>
        /// <param name="element">The element</param>
        /// <returns>True on success</returns>
        public bool RemoveRootElement(Element element)
        {
            var result = _rootElements.Remove(element);
            if (result)
                element.ElementManager = null;

            return result;
        }


        /// <summary>
        /// Gets a value indicating whether the position intersects with any element ordered by the z index
        /// </summary>
        /// <param name="element">The element</param>
        /// <param name="positon">The position</param>
        /// <returns>True when intersecting</returns>
        public bool Intersects(Vector2 positon, out Element element)
        {
            element = _rootElements.Where(rootElement => rootElement.Intersects(positon)).Where(x => x.Visible).OrderBy(x => x.ZIndex).FirstOrDefault();
            return element != null;
        }

        /// <summary>
        /// Gets a value indicating whether the rectangle intersects with any element ordered by the z index
        /// </summary>
        /// <param name="element">The element</param>
        /// <param name="rectangle">The rectangle</param>
        /// <returns>True when intersecting</returns>
        public bool Intersects(Rectangle rectangle, out Element element)
        {
            element = _rootElements.Where(rootElement => rootElement.Intersects(rectangle)).Where(x => x.Visible).OrderBy(x => x.ZIndex).FirstOrDefault();
            return element != null;
        }

        /// <summary>
        /// Gets a value indicating whether the rectangle intersects with the specified element
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
        /// Gets a value indicating whether the position intersects with the specified element
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
        /// Sets the focus of an element
        /// </summary>
        /// <param name="element">The element</param>
        public void SetFocus(Element element)
        {
            FocusedElement = element;
        }

        /// <summary>
        /// Draws all elements
        /// </summary>
        /// <param name="spriteBatch">The spritebatch</param>
        /// <param name="gameTime">The game time</param>
        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            foreach (var rootElement in _rootElements.OrderBy(x => x.ZIndex))
            {
                rootElement.Draw(spriteBatch, gameTime);
            }
        }

        /// <summary>
        /// Updates all elements
        /// </summary>
        /// <param name="gameTime">The game time</param>
        public override void Update(GameTime gameTime)
        {
            foreach (var rootElement in _rootElements.OrderBy(x => x.ZIndex))
            {
                rootElement.Update(gameTime);
            }
        }

        /// <summary>
        /// Sets the input state for the focused element if it is enabled
        /// </summary>
        /// <param name="inputState">The input state</param>
        public void SetInputState(InputState inputState)
        {
            FocusedElement?.InternalInputReceived(inputState);
        }

        /// <summary>
        /// Gets the enumerator
        /// </summary>
        /// <returns>Enumerator</returns>
        public IEnumerator<Element> GetEnumerator()
        {
            return _rootElements.GetEnumerator();
        }

        /// <summary>
        /// Gets the enumerator
        /// </summary>
        /// <returns>Enumerator</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
