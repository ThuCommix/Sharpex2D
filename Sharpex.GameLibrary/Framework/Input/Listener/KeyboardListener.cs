using System;
using System.Collections.Generic;
using System.Windows.Forms;
using SharpexGL.Framework.Game;
using SharpexGL.Framework.Game.Timing;
using SharpexGL.Framework.Rendering;

namespace SharpexGL.Framework.Input.Listener
{
    public class KeyboardListener : IGameHandler, IInputListener
    {

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
                foreach (KeyValuePair<Keys, bool> current in _keystate)
                {
                    _lastkeystate.Add(current.Key, current.Value);
                }
                _keystate.Clear();
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
            SGL.Components.Get<GameLoop>().Subscribe(this);
        }

        #endregion

        #region IInputListener Implementation

        /// <summary>
        /// A value indicating whether the InputListener is enabled.
        /// </summary>
        public bool IsEnabled { set; get; }

        #endregion

        private Dictionary<Keys, bool> _keystate;
        private Dictionary<Keys, bool> _lastkeystate;
        /// <summary>
        /// Initializes a new KeyboardProvider.
        /// </summary>
        /// <param name="handle">The Handle.</param>
        public KeyboardListener(IntPtr handle)
        {
            Control control = Control.FromHandle(handle);
            _keystate = new Dictionary<Keys, bool>();
            _lastkeystate = new Dictionary<Keys, bool>();
            control.KeyDown += surface_KeyDown;
            control.KeyUp += surface_KeyUp;
            IsEnabled = true;
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
        private void surface_KeyUp(object sender, KeyEventArgs e)
        {
            if (IsEnabled)
            {
                SetKeyState((Keys) e.KeyData, false);
            }
        }
        private void surface_KeyDown(object sender, KeyEventArgs e)
        {
            if (IsEnabled)
            {
                SetKeyState((Keys) e.KeyData, true);
            }
        }
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
    }
}
