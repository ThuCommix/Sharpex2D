using SharpexGL.Framework.Components;
using SharpexGL.Framework.Content;
using SharpexGL.Framework.Input;
using SharpexGL.Framework.Media.Sound;
using SharpexGL.Framework.Rendering;
using SharpexGL.Framework.Rendering.Scene;

namespace SharpexGL.Framework.Game
{
    public abstract class Game : IGameHandler
    {
        /// <summary>
        /// The current InputManager.
        /// </summary>
        public InputManager Input
        {
            get;
            set;
        }
        /// <summary>
        /// The Current SoundPlayer.
        /// </summary>
        public SoundManager SoundManager
        {
            get;
            set;
        }
        /// <summary>
        /// The Current ContentManager.
        /// </summary>
        public ContentManager Content
        {
            get;
            set;
        }
        /// <summary>
        /// The Current SceneManager.
        /// </summary>
        public SceneManager SceneManager
        {
            get;
            set;
        }

        #region IGameHandler Implementation
        void IGameHandler.Tick(float elapsed)
        {
            OnTick(elapsed);
        }

        void IGameHandler.Render(IRenderer renderer, float elapsed)
        {
            OnRendering(renderer, elapsed);
        }
        #endregion

        #region IConstructable Implementation
        void IConstructable.Construct()
        {
            OnInitialize();
        }
        #endregion

        /// <summary>
        /// Processes a Game tick.
        /// </summary>
        /// <param name="elapsed">The Elapsed.</param>
        public abstract void OnTick(float elapsed);
        /// <summary>
        /// Processes a Render.
        /// </summary>
        /// <param name="renderer">The GraphicRenderer.</param>
        /// <param name="elapsed">The Elapsed.</param>
        public abstract void OnRendering(IRenderer renderer, float elapsed);
        /// <summary>
        /// Processes the Game initialization.
        /// </summary>
        public abstract void OnInitialize();
        /// <summary>
        /// Processes the Game load.
        /// </summary>
        public abstract void OnLoadContent();
        /// <summary>
        /// Processes the Game unload.
        /// </summary>
        public abstract void OnUnload();
        /// <summary>
        /// Processes the Game close.
        /// </summary>
        public abstract void OnClose();
    }
}
