// Copyright (c) 2012-2014 Sharpex2D - Kevin Scholz (ThuCommix)
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the 'Software'), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED 'AS IS', WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using System;
using Sharpex2D.Audio;
using Sharpex2D.Content;
using Sharpex2D.GameService;
using Sharpex2D.Input;
using Sharpex2D.Rendering;
using Sharpex2D.Rendering.Scene;
using Sharpex2D.Surface;

namespace Sharpex2D
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
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

        private RenderTarget _renderTarget;

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
            get { return _renderTarget.Window.IsActive; }
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
            _renderTarget = SGL.Components.Get<RenderTarget>();
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
            SGL.Components.Get<IGameLoop>().Idle = false;
        }

        /// <summary>
        ///     Processes if the surface is deactivated.
        /// </summary>
        public virtual void OnDeactivation()
        {
            SGL.Components.Get<IGameLoop>().Idle = true;
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