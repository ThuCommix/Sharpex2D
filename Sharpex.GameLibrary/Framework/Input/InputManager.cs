using System;
using SharpexGL.Framework.Game;
using SharpexGL.Framework.Input.Listener;
using SharpexGL.Framework.Rendering;

namespace SharpexGL.Framework.Input
{
    public class InputManager : IGameHandler
    {

        #region IGameHandler Implementation
        /// <summary>
        /// Constructs the Component.
        /// </summary>
        public void Construct()
        {
            Keyboard.Construct();
        }

        /// <summary>
        /// Called if the component should get updated.
        /// </summary>
        /// <param name="elapsed">The Elapsed</param>
        public void Tick(float elapsed)
        {
            Keyboard.Tick(elapsed);
        }
        /// <summary>
        /// Processes a Render.
        /// </summary>
        /// <param name="renderer">The GraphicRenderer.</param>
        /// <param name="elapsed">The Elapsed.</param>
        public void Render(IGraphicRenderer renderer, float elapsed)
        {

        }
        #endregion

        /// <summary>
        /// Initializes a new InputManager Instance.
        /// </summary>
        /// <param name="handle">The GameWindowHandle.</param>
        public InputManager(IntPtr handle)
        {
            Keyboard = new KeyboardListener(handle);
            Mouse = new MouseListener(handle);
        }
        /// <summary>
        /// Gets the KeyboardListener.
        /// </summary>
        public KeyboardListener Keyboard { get; private set; }
        /// <summary>
        /// Gets the MouseListener.
        /// </summary>
        public MouseListener Mouse { get; private set; }
    }
}
