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
using Sharpex2D.Common.Extensions;
using Sharpex2D.Input;
using Sharpex2D.Math;
using Sharpex2D.Rendering;

namespace Sharpex2D.UI
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Tested)]
    public abstract class UIControl
    {
        #region IGameHandler Implementation

        /// <summary>
        /// Updates the object.
        /// </summary>
        /// <param name="gameTime">The GameTime.</param>
        public void Update(GameTime gameTime)
        {
            _mouseState = _inputManager.Mouse.GetState();
            _keyState = _inputManager.Keyboard.GetState();
            _mouseRectangle.X = _mouseState.Position.X;
            _mouseRectangle.Y = _mouseState.Position.Y;
            IsMouseHoverState = _mouseRectangle.Intersects(Bounds.ToRectangle());

            //check if the mouse clicked the control

            if (IsMouseHoverState && IsMouseDown(MouseButtons.Left))
            {
                SetFocus();
            }

            OnUpdate(gameTime);
        }

        #endregion

        #region UIControl

        #region Properties

        private KeyboardState _keyState;
        private Vector2 _lastRelativeMousePostion;
        private MouseState _mouseState;

        /// <summary>
        /// Gets the relative mouse position.
        /// </summary>
        public Vector2 RelativeMousePosition
        {
            get
            {
                if (!IsMouseHoverState)
                {
                    return _lastRelativeMousePostion;
                }

                _lastRelativeMousePostion = _mouseState.Position - Position;

                return _lastRelativeMousePostion;
            }
        }

        /// <summary>
        /// Gets the Bounds of the UIControl.
        /// </summary>
        public UIBounds Bounds { private set; get; }

        /// <summary>
        /// Sets or gets the Position of the UIControl.
        /// </summary>
        public Vector2 Position
        {
            get { return _position; }
            set
            {
                _position = value;
                UpdateBounds();
            }
        }

        /// <summary>
        /// A value indicating whether the UIControl is visible.
        /// </summary>
        public bool Visible { set; get; }

        /// <summary>
        /// A value indicating whether the UIConrol is enabled.
        /// </summary>
        public bool Enable { set; get; }

        /// <summary>
        /// A value indicating whether the UIControl is available to get the focus.
        /// </summary>
        public bool CanGetFocus { set; get; }

        /// <summary>
        /// Sets or gets the Size of the UIControl.
        /// </summary>
        public UISize Size
        {
            get { return _size; }
            set
            {
                _size = value;
                UpdateBounds();
            }
        }

        /// <summary>
        /// A value indicating whether the mouse is hovering the UIControl.
        /// </summary>
        public bool IsMouseHoverState { private set; get; }

        /// <summary>
        /// A value indicating whether the UIControl has focus.
        /// </summary>
        public bool HasFocus { internal set; get; }

        /// <summary>
        /// Gets the Guid-Identifer.
        /// </summary>
        public Guid Guid { private set; get; }

        /// <summary>
        /// Sets or gets the Parent UIControl.
        /// </summary>
        public UIControl Parent
        {
            set { SetParent(value); }
            get { return _parent; }
        }

        /// <summary>
        /// Gets the children of the UIControl.
        /// </summary>
        public List<UIControl> Children { internal set; get; }

        /// <summary>
        /// Sets or gets the UIManager.
        /// </summary>
        internal UIManager UIManager { set; get; }

        #endregion

        #region Methods

        /// <summary>
        /// Initializes a new UIControl class.
        /// </summary>
        /// <param name="assignedUIManager">The assigned UIManager.</param>
        protected UIControl(UIManager assignedUIManager)
        {
            _position = new Vector2(0, 0);
            _size = new UISize(0, 0);
            UpdateBounds();
            _mouseRectangle = new Rectangle {Width = 1, Height = 1};
            Guid = Guid.NewGuid();
            _inputManager = SGL.Components.Get<InputManager>();
            CanGetFocus = true;
            Enable = true;
            Visible = true;
            _parent = null;
            _lastRelativeMousePostion = new Vector2(0, 0);
            Children = new List<UIControl>();
            UIManager = assignedUIManager;
            UIManager.Add(this);
        }

        /// <summary>
        /// Updates the Bounds of the UIControl.
        /// </summary>
        internal void UpdateBounds()
        {
            Bounds = new UIBounds((int) Position.X, (int) Position.Y, Size.Width, Size.Height);
        }

        /// <summary>
        /// Sets the Parent.
        /// </summary>
        /// <param name="parent">The Parent.</param>
        internal void SetParent(UIControl parent)
        {
            if (parent != null)
            {
                parent.Children.Add(this);
                _parent = parent;
            }
            else
            {
                _parent.RemoveChild(this);
                _parent = null;
            }
        }

        /// <summary>
        /// Sets the Focus for this UIControl.
        /// </summary>
        public void SetFocus()
        {
            //check if we can get the focus

            if (!CanGetFocus || !Enable) return;

            // unset the last focused element
            foreach (UIControl ctrl in UIManager.GetAll())
            {
                ctrl.HasFocus = false;
            }

            HasFocus = true;
        }

        /// <summary>
        /// Removes a UIControl from the Childs.
        /// </summary>
        /// <param name="control">The UIControl.</param>
        public void RemoveChild(UIControl control)
        {
            if (Children.Contains(control))
            {
                Children.Remove(control);
            }
        }

        /// <summary>
        /// Removes the Focus of the UIControl.
        /// </summary>
        public void RemoveFocus()
        {
            HasFocus = false;
        }

        /// <summary>
        /// Determines, if a MouseButton was pressed.
        /// </summary>
        /// <param name="mouseButton">The MouseButton.</param>
        /// <returns>True if pressed</returns>
        public bool IsMouseDown(MouseButtons mouseButton)
        {
            //while the cursor do not intersect our control, return false
            return IsMouseHoverState && Enable && _mouseState.IsMouseButtonUp(mouseButton);
        }

        /// <summary>
        /// Determines, if a Key was pressed down.
        /// </summary>
        /// <param name="key">The Key.</param>
        /// <returns>True if pressed down</returns>
        public bool IsKeyDown(Keys key)
        {
            return HasFocus && Enable && _keyState.IsKeyDown(key);
        }

        /// <summary>
        /// Determines, if a Key was relased.
        /// </summary>
        /// <param name="key">The Key.</param>
        /// <returns>True if pressed</returns>
        public bool IsKeyUp(Keys key)
        {
            return HasFocus && Enable && _keyState.IsKeyUp(key);
        }

        #endregion

        #region Private Fields

        private readonly InputManager _inputManager;
        private Rectangle _mouseRectangle;
        private UIControl _parent;
        private Vector2 _position;
        private UISize _size;

        #endregion

        #region Abstract

        /// <summary>
        /// Processes a Render call.
        /// </summary>
        /// <param name="spriteBatch">The SpriteBatch.</param>
        public abstract void OnDraw(SpriteBatch spriteBatch);

        /// <summary>
        /// Updates the object.
        /// </summary>
        /// <param name="gameTime">The GameTime.</param>
        public virtual void OnUpdate(GameTime gameTime)
        {
        }

        #endregion

        #endregion
    }
}