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
using System.Collections.Generic;
using Sharpex2D.Rendering;

namespace Sharpex2D.UI
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Tested)]
    public class UIManager
    {
        private readonly List<UIControl> _controls;

        /// <summary>
        /// Initializes a new UICollection class.
        /// </summary>
        internal UIManager()
        {
            _controls = new List<UIControl>();
        }

        /// <summary>
        /// Sets or gets the Description.
        /// </summary>
        public string Description { set; get; }

        /// <summary>
        /// Adds a new UIControl to the Collection.
        /// </summary>
        /// <param name="control">The Control.</param>
        public void Add(UIControl control)
        {
            _controls.Add(control);
        }

        /// <summary>
        /// Removes a new UIControl from the Collection.
        /// </summary>
        /// <param name="control">The Control.</param>
        public void Remove(UIControl control)
        {
            _controls.Remove(control);
        }

        /// <summary>
        /// Gets the spezified UIControl.
        /// </summary>
        /// <typeparam name="T">The Type.</typeparam>
        /// <returns>UIControl</returns>
        public T Get<T>() where T : UIControl
        {
            for (int i = 0; i <= _controls.Count - 1; i++)
            {
                if (typeof (T) == _controls[i].GetType())
                {
                    return (T) _controls[i];
                }
            }

            throw new ArgumentException("The UIControl " + typeof (T).Name + " could not be found.");
        }

        /// <summary>
        /// Gets the UIControl spezified by its GUID.
        /// </summary>
        /// <param name="guid">The Guid.</param>
        /// <returns>UIControl</returns>
        public UIControl Get(Guid guid)
        {
            for (int i = 0; i <= _controls.Count - 1; i++)
            {
                if (guid == _controls[i].Guid)
                {
                    return _controls[i];
                }
            }

            throw new ArgumentException("The UIControl with GUID " + guid + " could not be found.");
        }

        /// <summary>
        /// Clears the UIManager.
        /// </summary>
        public void Clear()
        {
            _controls.Clear();
        }

        /// <summary>
        /// Gets all UIControls.
        /// </summary>
        /// <returns>UIControl Array</returns>
        public UIControl[] GetAll()
        {
            return _controls.ToArray();
        }

        /// <summary>
        /// Updates the object.
        /// </summary>
        /// <param name="gameTime">The GameTime.</param>
        public void Update(GameTime gameTime)
        {
            for (int i = 0; i <= _controls.Count - 1; i++)
            {
                if (_controls[i].Enable)
                {
                    _controls[i].Update(gameTime);
                }
            }
        }

        /// <summary>
        /// Proceses a Render.
        /// </summary>
        /// <param name="spriteBatch">The SpriteBatch.</param>
        /// <param name="gameTime">The GameTime.</param>
        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            for (int i = 0; i <= _controls.Count - 1; i++)
            {
                if (_controls[i].Visible)
                {
                    _controls[i].OnDraw(spriteBatch);
                }
            }
        }
    }
}