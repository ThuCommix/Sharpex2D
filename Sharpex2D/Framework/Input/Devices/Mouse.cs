using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Sharpex2D.Framework.Events;
using Sharpex2D.Framework.Game.Timing;
using Sharpex2D.Framework.Input.Events;
using Sharpex2D.Framework.Math;
using Sharpex2D.Framework.Rendering;

namespace Sharpex2D.Framework.Input.Devices
{
    public class Mouse : IMouse
    {
        #region IGameHandler Implementation

        /// <summary>
        /// Constructs the Element.
        /// </summary>
        public void Construct()
        {
            SGL.Components.Get<IGameLoop>().Subscribe(this);
        }
        /// <summary>
        /// Processes a Game tick.
        /// </summary>
        /// <param name="elapsed">The Elapsed.</param>
        public void Tick(float elapsed)
        {
            _mousestate.Clear();
        }
        /// <summary>
        /// Processes a Render.
        /// </summary>
        /// <param name="renderer">The GraphicRenderer.</param>
        /// <param name="elapsed">The Elapsed.</param>
        public void Render(IRenderer renderer, float elapsed)
        {
            
        }

        #endregion

        #region IDevice Implementation

        /// <summary>
        /// A value indicating whether the device is enabled.
        /// </summary>
        public bool IsEnabled { get; set; }
        /// <summary>
        /// Gets the Guid-Identifer of the device.
        /// </summary>
        public Guid Guid { get; private set; }
        /// <summary>
        /// Gets the device description.
        /// </summary>
        public string Description { get; private set; }
        /// <summary>
        /// Initializes the Device.
        /// </summary>
        public void InitializeDevice()
        {

        }

        #endregion

        #region IMouse Implementation

        /// <summary>
        /// Gets the current MousePosition.
        /// </summary>
        public Vector2 Position
        {
            get;
            private set;
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

        #endregion

        private readonly EventManager _eventManager;
        
        /// <summary>
        /// Initializes a new Mouse class.
        /// </summary>
        /// <param name="handle">The Handle.</param>
        public Mouse(IntPtr handle)
        {
            Guid = new Guid("5D0749E7-80A2-40EA-857B-0776CB7859CF");
            Description = "MouseDevice";
            IsEnabled = true;

            Position = new Vector2(0f, 0f);
            var control = Control.FromHandle(handle);
            _mousestate = new Dictionary<MouseButtons, bool>();
            control.MouseMove += surface_MouseMove;
            control.MouseDown += surface_MouseDown;
            control.MouseUp += surface_MouseUp;
            Handle = handle;
            _eventManager = SGL.Components.Get<EventManager>();
        }

        private readonly Dictionary<MouseButtons, bool> _mousestate;

        /// <summary>
        /// Represents the surface handle.
        /// </summary>
        public IntPtr Handle { private set; get; }

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
                SetButtonState((MouseButtons)e.Button, false);
            }
        }
        private void surface_MouseDown(object sender, MouseEventArgs e)
        {
            if (IsEnabled)
            {
                SetButtonState((MouseButtons)e.Button, true);
            }
        }
        private void surface_MouseMove(object sender, MouseEventArgs e)
        {
            Position = new Vector2(e.Location.X / SGL.GraphicsDevice.Scale.X, e.Location.Y / SGL.GraphicsDevice.Scale.Y);
            _eventManager.Publish(new MouseLocationChangedEvent(Position));
        }

    }
}
