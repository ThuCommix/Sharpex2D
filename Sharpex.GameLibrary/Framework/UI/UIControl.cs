using System;
using System.Windows.Forms;
using SharpexGL.Framework.Common.Extensions;
using SharpexGL.Framework.Game;
using SharpexGL.Framework.Game.Timing;
using SharpexGL.Framework.Math;
using SharpexGL.Framework.Rendering;
using InputManager = SharpexGL.Framework.Input.InputManager;

namespace SharpexGL.Framework.UI
{
    public abstract class UIControl : IGameHandler
    {
        #region IGameHandler Implementation
        /// <summary>
        /// Constructs the Component
        /// </summary>
        public void Construct()
        {

        }
        /// <summary>
        /// Processes a Game tick.
        /// </summary>
        /// <param name="elapsed">The Elapsed.</param>
        public void Tick(float elapsed)
        {
            IsMouseDownState = IsMouseDown(MouseButtons.Left);
            _mouseRectangle.X = _inputManager.Mouse.Position.X;
            _mouseRectangle.Y = _inputManager.Mouse.Position.Y;
            IsMouseHoverState = _mouseRectangle.Intersects(Bounds.ToRectangle());
            OnTick(elapsed);
        }
        /// <summary>
        /// Processes a Render.
        /// </summary>
        /// <param name="renderer">The GraphicRenderer.</param>
        /// <param name="elapsed">The Elapsed.</param>
        public void Render(IRenderer renderer, float elapsed)
        {
            OnRender(renderer);
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
        /// A value indicating whether the mouse clicked on the UIControl.
        /// </summary>
        public bool IsMouseDownState { private set; get; }

        /// <summary>
        /// Gets the Guid-Identifer.
        /// </summary>
        public Guid Guid { private set; get; }

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
            SGL.Components.Get<GameLoop>().Subscribe(this);
            SGL.Components.AddComponent(this);
        }

        /// <summary>
        /// Determines, if a MouseButton was pressed.
        /// </summary>
        /// <param name="mouseButton">The MouseButton.</param>
        /// <returns>True if pressed</returns>
        public bool IsMouseDown(MouseButtons mouseButton)
        {
            return _inputManager.Mouse.IsButtonPressed(mouseButton);
        }

        /// <summary>
        /// Determines, if a Key was pressed down.
        /// </summary>
        /// <param name="key">The Key.</param>
        /// <returns>True if pressed down</returns>
        public bool IsKeyDown(Input.Keys key)
        {
            return _inputManager.Keyboard.IsKeyDown(key);
        }

        /// <summary>
        /// Determines, if a Key was pressed.
        /// </summary>
        /// <param name="key">The Key.</param>
        /// <returns>True if pressed</returns>
        public bool IsKeyPressed(Input.Keys key)
        {
            return _inputManager.Keyboard.IsKeyPressed(key);
        }

        #endregion

        #region Private Fields

        private Vector2 _position;
        private UISize _size;
        private readonly InputManager _inputManager;
        private Rectangle _mouseRectangle;

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
