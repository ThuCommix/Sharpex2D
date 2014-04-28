using System;
using System.Collections.Generic;
using Sharpex2D.Framework.Common.Extensions;
using Sharpex2D.Framework.Input;
using Sharpex2D.Framework.Math;
using Sharpex2D.Framework.Rendering;

namespace Sharpex2D.Framework.UI
{
    public abstract class UIControl
    {
        #region IGameHandler Implementation
        /// <summary>
        /// Processes a Game tick.
        /// </summary>
        /// <param name="elapsed">The Elapsed.</param>
        public void Tick(float elapsed)
        {
            _mouseRectangle.X = _inputManager.Mouse.Position.X;
            _mouseRectangle.Y = _inputManager.Mouse.Position.Y;
            IsMouseHoverState = _mouseRectangle.Intersects(Bounds.ToRectangle());

            //check if the mouse clicked the control

            if (IsMouseHoverState && IsMouseDown(MouseButtons.Left))
            {
                SetFocus();
            }

            OnTick(elapsed);
        }

        #endregion

        #region UIControl

        #region Properties

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
            get
            {
                return _parent;
            }
        }

        /// <summary>
        /// Gets the childs of the UIControl.
        /// </summary>
        public List<UIControl> Childs { internal set; get; }

        /// <summary>
        /// Sets or gets the UIManager.
        /// </summary>
        internal UIManager UIManager { set; get; }

        #endregion

        #region Methods

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
                parent.Childs.Add(this);
                _parent = parent;
            }
            else
            {
                _parent.RemoveChild(this);
                _parent = null;
            }
        }

        /// <summary>
        /// Initializes a new UIControl class.
        /// </summary>
        protected UIControl()
        {
            _position = new Vector2(0, 0);
            _size = new UISize(0, 0);
            UpdateBounds();
            _mouseRectangle = new Rectangle {Width = 1, Height = 1};
            Guid = Guid.NewGuid();
            _inputManager = SGL.Components.Get<InputManager>();
            CanGetFocus = true;
            Enable = true;
            _parent = null;
            Childs = new List<UIControl>();
            UIManager = SGL.Components.Get<UIManager>();
            UIManager.Add(this);
        }

        /// <summary>
        /// Sets the Focus for this UIControl.
        /// </summary>
        public void SetFocus()
        {
            //check if we can get the focus

            if (!CanGetFocus || !Enable) return;

            // unset the last focused element
            foreach (var ctrl in UIManager.GetAll())
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
            if (Childs.Contains(control))
            {
                Childs.Remove(control);
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
            return IsMouseHoverState && Enable && _inputManager.Mouse.IsButtonPressed(mouseButton);
        }

        /// <summary>
        /// Determines, if a Key was pressed down.
        /// </summary>
        /// <param name="key">The Key.</param>
        /// <returns>True if pressed down</returns>
        public bool IsKeyDown(Input.Keys key)
        {
            return HasFocus && Enable && _inputManager.Keyboard.IsKeyDown(key);
        }

        /// <summary>
        /// Determines, if a Key was pressed.
        /// </summary>
        /// <param name="key">The Key.</param>
        /// <returns>True if pressed</returns>
        public bool IsKeyPressed(Input.Keys key)
        {
            return HasFocus && Enable && _inputManager.Keyboard.IsKeyPressed(key);
        }

        #endregion

        #region Private Fields

        private Vector2 _position;
        private UISize _size;
        private readonly InputManager _inputManager;
        private Rectangle _mouseRectangle;
        private UIControl _parent;

        #endregion

        #region Abstract

        /// <summary>
        /// Processes a Render call.
        /// </summary>
        /// <param name="renderer">The Renderer.</param>
        public abstract void OnRender(IRenderer renderer);

        /// <summary>
        /// Processes a Tick call.
        /// </summary>
        /// <param name="elapsed">The Elapsed.</param>
        public virtual void OnTick(float elapsed)
        {
            
        }

        #endregion

        #endregion
    }
}
