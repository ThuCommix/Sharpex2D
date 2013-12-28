using System;
using System.Collections.Generic;
using System.Windows.Forms;
using SharpexGL.Framework.Game;
using SharpexGL.Framework.Game.Timing;
using SharpexGL.Framework.Rendering;

namespace SharpexGL.Framework.Input.Devices
{
    public class Keyboard : IKeyboard, IGameHandler
    {
        #region IDevice Implementation

        /// <summary>
        /// A value indicating whether the device is enabled.
        /// </summary>
        public bool IsEnabled
        {
            set;
            get;
        }
        /// <summary>
        /// Gets the Guid-Identifer of the device.
        /// </summary>
        public Guid Guid { get; private set; }
        /// <summary>
        /// Gets the device description.
        /// </summary>
        public string Description { get; private set; }

        #endregion

        #region IGameHandler Implementation
        /// <summary>
        /// Called if the component should get updated.
        /// </summary>
        /// <param name="elapsed">The Elapsed.</param>
        public void Tick(float elapsed)
        {
            if (IsEnabled)
            {
                _lastkeystate.Clear();
                foreach (var current in _keystate)
                {
                    _lastkeystate.Add(current.Key, current.Value);
                }
            }
        }

        /// <summary>
        /// Processes a Render.
        /// </summary>
        /// <param name="renderer">The GraphicRenderer.</param>
        /// <param name="elapsed">The Elapsed.</param>
        public void Render(IRenderer renderer, float elapsed)
        {

        }

        /// <summary>
        /// Called if the component get initialized.
        /// </summary>
        public void Construct()
        {
            SGL.Components.Get<IGameLoop>().Subscribe(this);
        }

        #endregion

        #region IKeyboard Implementation

        /// <summary>
        /// Determines, if a specific key is pressed down.
        /// </summary>
        /// <param name="key">The Key.</param>
        /// <returns>Boolean</returns>
        public bool IsKeyDown(Keys key)
        {
            return _keystate.ContainsKey(key) && _keystate[key];
        }
        /// <summary>
        /// Determines, if a specific key is pushed up.
        /// </summary>
        /// <param name="key">The Key.</param>
        /// <returns>Boolean</returns>
        public bool IsKeyUp(Keys key)
        {
            return !_keystate.ContainsKey(key) || !_keystate[key];
        }
        /// <summary>
        /// Determines, if a specific key is pressed.
        /// </summary>
        /// <param name="key">The Key.</param>
        /// <returns>Boolean</returns>
        public bool IsKeyPressed(Keys key)
        {
            return (!_lastkeystate.ContainsKey(key) || !_lastkeystate[key]) && _keystate.ContainsKey(key) && _keystate[key];
        }

        #endregion

        private readonly Dictionary<Keys, bool> _keystate;
        private readonly Dictionary<Keys, bool> _lastkeystate;

        /// <summary>
        /// Initializes a new FluentKeyboard class.
        /// </summary>
        /// <param name="surfaceHandle">The SurfaceHandle.</param>
        public Keyboard(IntPtr surfaceHandle)
        {
            Guid = new Guid("55DDC560-40B5-487F-A47B-A265707E495D");
            Description = "Keyboard based on the surface events";
            var surface = (Form) Control.FromHandle(surfaceHandle);
            _lastkeystate = new Dictionary<Keys, bool>();
            _keystate = new Dictionary<Keys, bool>();
            surface.KeyDown += _surface_KeyDown;
            surface.KeyUp += _surface_KeyUp;
        }

        /// <summary>
        /// The KeyUp Event.
        /// </summary>
        /// <param name="sender">The Sender.</param>
        /// <param name="e">The EventArgs.</param>
        void _surface_KeyUp(object sender, KeyEventArgs e)
        {
            if (!IsEnabled) return;
            SetKeyState((Keys)e.KeyCode, false);
        }

        /// <summary>
        /// The KeyDown Event.
        /// </summary>
        /// <param name="sender">The Sender.</param>
        /// <param name="e">The EventArgs.</param>
        void _surface_KeyDown(object sender, KeyEventArgs e)
        {
            if (!IsEnabled) return;
            SetKeyState((Keys)e.KeyCode, true);
        }

        /// <summary>
        /// Sets the internal KeyState.
        /// </summary>
        /// <param name="key">The Key.</param>
        /// <param name="state">The State.</param>
        private void SetKeyState(Keys key, bool state)
        {
            if (!_keystate.ContainsKey(key))
            {
                _keystate.Add(key, state);
            }
            _keystate[key] = state;
        }

        ~Keyboard()
        {
            IsEnabled = false;
        }
    }
}
