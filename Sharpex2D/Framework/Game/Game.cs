using System;
using Sharpex2D.Framework.Components;
using Sharpex2D.Framework.Content;
using Sharpex2D.Framework.Game.Services;
using Sharpex2D.Framework.Input;
using Sharpex2D.Framework.Media.Sound;
using Sharpex2D.Framework.Media.Video;
using Sharpex2D.Framework.Rendering;
using Sharpex2D.Framework.Rendering.Scene;
using Sharpex2D.Framework.Surface;

namespace Sharpex2D.Framework.Game
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Tested)]
    public abstract class Game : IUpdateable, IDrawable, IConstructable
    {
        #region IComponent Implementation

        /// <summary>
        ///     Sets or gets the Guid of the Component.
        /// </summary>
        public Guid Guid
        {
            get { return new Guid("6782E502-BE99-4030-9472-C295E822881B"); }
        }

        #endregion

        internal RenderTarget RenderTarget;

        /// <summary>
        ///     The current InputManager.
        /// </summary>
        public InputManager Input { get; set; }

        /// <summary>
        ///     The Current SoundPlayer.
        /// </summary>
        public SoundManager SoundManager { get; internal set; }

        public VideoManager VideoManager { get; internal set; }

        /// <summary>
        ///     The Current ContentManager.
        /// </summary>
        public ContentManager Content { get; set; }

        /// <summary>
        ///     The Current SceneManager.
        /// </summary>
        public SceneManager SceneManager { get; set; }

        /// <summary>
        ///     The Current GameServices
        /// </summary>
        public GameServiceContainer GameServices { set; get; }

        /// <summary>
        ///     A value indicating whether the surface is active.
        /// </summary>
        public bool IsActive
        {
            get { return RenderTarget.SurfaceControl.IsActive(); }
        }

        #region IUpdateable Implementation

        /// <summary>
        ///     Processes a Tick.
        /// </summary>
        /// <param name="gameTime">The GameTime.</param>
        void IUpdateable.Tick(GameTime gameTime)
        {
            OnTick(gameTime);
        }

        #endregion

        #region IDrawable Implementation

        /// <summary>
        ///     Processes a Render.
        /// </summary>
        /// <param name="renderer">The Renderer.</param>
        /// <param name="gameTime">The GameTime.</param>
        void IDrawable.Render(IRenderer renderer, GameTime gameTime)
        {
            OnRendering(renderer, gameTime);
        }

        #endregion

        #region IConstructable Implementation

        /// <summary>
        ///     Constructs the component.
        /// </summary>
        void IConstructable.Construct()
        {
            RenderTarget = SGL.Components.Get<RenderTarget>();
        }

        #endregion

        /// <summary>
        ///     Processes a Game tick.
        /// </summary>
        /// <param name="gameTime">The GameTime.</param>
        public abstract void OnTick(GameTime gameTime);

        /// <summary>
        ///     Processes a Render.
        /// </summary>
        /// <param name="renderer">The GraphicRenderer.</param>
        /// <param name="gameTime">The GameTime.</param>
        public abstract void OnRendering(IRenderer renderer, GameTime gameTime);

        /// <summary>
        ///     Processes the Game initialization.
        /// </summary>
        public abstract void OnInitialize();

        /// <summary>
        ///     Processes the Game load.
        /// </summary>
        public abstract void OnLoadContent();

        /// <summary>
        ///     Processes the Game unload.
        /// </summary>
        public abstract void OnUnload();

        /// <summary>
        ///     Processes the Game close.
        /// </summary>
        public abstract void OnClose();

        /// <summary>
        ///     Processes if the surface is activated.
        /// </summary>
        public virtual void OnActivated()
        {
        }

        /// <summary>
        ///     Processes if the surface is deactivated.
        /// </summary>
        public virtual void OnDeactivated()
        {
        }

        /// <summary>
        ///     Exits the game.
        /// </summary>
        public void ExitGame()
        {
            SGL.Shutdown();
        }

        /// <summary>
        ///     Restarts the Game with the specified LaunchParameters.
        /// </summary>
        /// <param name="launchParameters">The LaunchParameters.</param>
        public void Restart(LaunchParameters launchParameters)
        {
            SGL.Restart(launchParameters.ToString());
        }
    }
}