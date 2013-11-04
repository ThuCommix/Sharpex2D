using System;
using System.Collections.Generic;
using System.Windows.Forms;
using SharpexGL.Framework.Components;
using SharpexGL.Framework.Events;
using SharpexGL.Framework.Input.Events;
using SharpexGL.Framework.Math;

namespace SharpexGL.Framework.Input.Listener
{
    public class MouseListener : IComponent, IInputListener
    {

        #region IInputListener Implementation

        /// <summary>
        /// A value indicating whether the InputListener is enabled.
        /// </summary>
        public bool IsEnabled { set; get; }

        #endregion

        private Dictionary<MouseButtons, bool> _mousestate;
        /// <summary>
        /// Gets the current MousePosition.
        /// </summary>
        public Vector2 Position
        {
            get;
            private set;
        }
        /// <summary>
        /// Represents the surface handle.
        /// </summary>
        public IntPtr Handle { private set; get; }
        /// <summary>
        /// Initializes a new MouseProvider.
        /// </summary>
        /// <param name="handle">The Handle.</param>
        public MouseListener(IntPtr handle)
        {
            Position = new Vector2(0f, 0f);
            var control = Control.FromHandle(handle);
            _mousestate = new Dictionary<MouseButtons, bool>();
            control.MouseMove += surface_MouseMove;
            control.MouseDown += surface_MouseDown;
            control.MouseUp += surface_MouseUp;
            Handle = handle;
            IsEnabled = true;
        }
        /// <summary>
        /// Determines, if a specific button is pressed.
        /// </summary>
        /// <param name="button">The Button.</param>
        /// <returns>Boolean</returns>
        public bool IsButtonPressed(MouseButtons button)
        {
            return _mousestate.ContainsKey(button) && _mousestate[button];
        }
        /// <summary>
        /// Determines, if a specific button is released.
        /// </summary>
        /// <param name="button">The Button.</param>
        /// <returns>Boolean</returns>
        public bool IsButtonReleased(MouseButtons button)
        {
            return _mousestate.ContainsKey(button) && _mousestate[button];
        }
        /// <summary>
        /// Sets the internal button state.
        /// </summary>
        /// <param name="button">The Button.</param>
        /// <param name="state">The State.</param>
        private void SetButtonState(MouseButtons button, bool state)
        {
            if (!_mousestate.ContainsKey(button))
            {
                _mousestate.Add(button, state);
            }
            _mousestate[button] = state;
        }
        private void surface_MouseUp(object sender, MouseEventArgs e)
        {
            if (IsEnabled)
            {
                SetButtonState(e.Button, false);
            }
        }
        private void surface_MouseDown(object sender, MouseEventArgs e)
        {
            if (IsEnabled)
            {
                SetButtonState(e.Button, true);
            }
        }
        private void surface_MouseMove(object sender, MouseEventArgs e)
        {
            Position = new Vector2(e.Location.X * SGL.GraphicsDevice.Scale.X, e.Location.Y * SGL.GraphicsDevice.Scale.Y);
            SGL.Components.Get<EventManager>().Publish(new MouseLocationChangedEvent(Position));
        }
    }
}
