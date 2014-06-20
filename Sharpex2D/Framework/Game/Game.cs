using System;
using Sharpex2D.Framework.Components;
using Sharpex2D.Framework.Content;
using Sharpex2D.Framework.Game.Services;
using Sharpex2D.Framework.Game.Timing;
using Sharpex2D.Framework.Input;
using Sharpex2D.Framework.Media.Sound;
using Sharpex2D.Framework.Media.Video;
using Sharpex2D.Framework.Rendering.Devices;
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
        ///     Initializes a new Game class.
        /// </summary>
        protected Game()
        {
            GameComponentManager = new GameComponentManager();
        }

        /// <summary>
        ///     Gets the GameComponentManager.
        /// </summary>
        public GameComponentManager GameComponentManager { private set; get; }

        /// <summary>
        ///     The current InputManager.
        /// </summary>
        public InputManager Input { get; set; }

        /// <summary>
        ///     The Current SoundPlayer.
        /// </summary>
        public SoundManager SoundManager { get; internal set; }

        /// <summary>
        ///     The Current VideoManager.
        /// </summary>
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
        ///     The Current GameServices.
        /// </summary>
        public GameServiceContainer GameServices { set; get; }

        /// <summary>
        ///     Sets or gets the TargetFrameTime.
        /// </summary>
        public float TargetFrameTime
        {
            get { return SGL.Components.Get<IGameLoop>().TargetFrameTime; }
            set { SGL.Components.Get<IGameLoop>().TargetFrameTime = value; }
        }

        /// <summary>
        ///     Sets or gets the TargetUpdateTime.
        /// </summary>
        public float TargetUpdateTime
        {
            get { return SGL.Components.Get<IGameLoop>().TargetUpdateTime; }
            set { SGL.Components.Get<IGameLoop>().TargetUpdateTime = value; }
        }

        /// <summary>
        ///     A value indicating whether the surface is active.
        /// </summary>
        public bool IsActive
        {
            get { return RenderTarget.Window.IsActive; }
        }

        #region IUpdateable Implementation

        /// <summary>
        ///     Updates the object.
        /// </summary>
        /// <param name="gameTime">The GameTime.</param>
        void IUpdateable.Update(GameTime gameTime)
        {
            OnUpdate(gameTime);
        }

        #endregion

        #region IDrawable Implementation

        /// <summary>
        ///     Processes a Render.
        /// </summary>
        /// <param name="renderer">The Renderer.</param>
        /// <param name="gameTime">The GameTime.</param>
        void IDrawable.Render(RenderDevice renderer, GameTime gameTime)
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
        ///     Updates the object.
        /// </summary>
        /// <param name="gameTime">The GameTime.</param>
        public virtual void OnUpdate(GameTime gameTime)
        {
            foreach (IGameComponent gameComponent in GameComponentManager)
            {
                gameComponent.Update(gameTime);
            }
        }

        /// <summary>
        ///     Processes a Render.
        /// </summary>
        /// <param name="renderer">The GraphicRenderer.</param>
        /// <param name="gameTime">The GameTime.</param>
        public virtual void OnRendering(RenderDevice renderer, GameTime gameTime)
        {
            foreach (IGameComponent gameComponent in GameComponentManager)
            {
                gameComponent.Render(renderer, gameTime);
            }
        }

        /// <summary>
        ///     Processes the Game initialization.
        /// </summary>
        /// <param name="launchParameters">The LaunchParameters.</param>
        public abstract EngineConfiguration OnInitialize(LaunchParameters launchParameters);

        /// <summary>
        ///     Processes the Game load.
        /// </summary>
        public abstract void OnLoadContent();

        /// <summary>
        ///     Processes the Game unload.
        /// </summary>
        public virtual void OnUnload()
        {
        }

        /// <summary>
        ///     Processes if the surface is activated.
        /// </summary>
        public virtual void OnActivation()
        {
        }

        /// <summary>
        ///     Processes if the surface is deactivated.
        /// </summary>
        public virtual void OnDeactivation()
        {
        }

        /// <summary>
        ///     Exits the game.
        /// </summary>
        public void Exit()
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