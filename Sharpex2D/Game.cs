// Copyright (c) 2012-2015 Sharpex2D - Kevin Scholz (ThuCommix)
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

using System.Collections.Generic;
using Sharpex2D.Framework.Audio;
using Sharpex2D.Framework.Audio.WaveOut;
using Sharpex2D.Framework.Content;
using Sharpex2D.Framework.Rendering;
using Sharpex2D.Framework.Rendering.OpenGL;

namespace Sharpex2D.Framework
{
    public abstract class Game : IComponent, IUpdateable, IDrawable
    {
        /// <summary>
        /// Initializes a new Game class.
        /// </summary>
        protected Game()
        {
            Components = new GameComponentCollection();
        }

        /// <summary>
        /// Gets the components.
        /// </summary>
        public GameComponentCollection Components { get; }

        /// <summary>
        /// Gets the component manager.
        /// </summary>
        public ComponentManager ComponentManager => GameHost.Components;

        /// <summary>
        /// Gets the media player.
        /// </summary>
        public SoundPlayer SoundPlayer { get; internal set; }

        /// <summary>
        /// Gets the content manager.
        /// </summary>
        public ContentManager Content { get; internal set; }

        /// <summary>
        /// Gets the scene manager.
        /// </summary>
        public SceneManager SceneManager { get; internal set; }

        /// <summary>
        /// Gets or sets the graphics manager.
        /// </summary>
        public GraphicsManager GraphicsManager { get; set; }

        /// <summary>
        /// Gets or sets the sound manager.
        /// </summary>
        public SoundManager SoundManager { set; get; }

        /// <summary>
        /// Sets or gets the TargetFrameTime.
        /// </summary>
        public float TargetTime
        {
            get { return GameHost.Get<GameLoop>().TargetTime; }
            set { GameHost.Get<GameLoop>().TargetTime = value; }
        }

        /// <summary>
        /// A value indicating whether the game window is focused.
        /// </summary>
        public bool IsFocused => Window.IsFocused;

        /// <summary>
        /// Gets the game window.
        /// </summary>
        public GameWindow Window { get; internal set; }

        #region IDrawable Implementation

        /// <summary>
        /// Processes a Render.
        /// </summary>
        /// <param name="spriteBatch">The SpriteBatch.</param>
        /// <param name="gameTime">The GameTime.</param>
        void IDrawable.Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            Draw(spriteBatch, gameTime);
        }

        #endregion

        #region IUpdateable Implementation

        /// <summary>
        /// Updates the object.
        /// </summary>
        /// <param name="gameTime">The GameTime.</param>
        void IUpdateable.Update(GameTime gameTime)
        {
            Update(gameTime);
        }

        #endregion

        /// <summary>
        /// Updates the components.
        /// </summary>
        /// <param name="gameTime">The GameTime.</param>
        public virtual void Update(GameTime gameTime)
        {
            IEnumerable<GameComponent> components = Components.GetUpdateables();
            foreach (GameComponent gameComponent in components)
            {
                gameComponent.Update(gameTime);
            }
        }

        /// <summary>
        /// Draws the components.
        /// </summary>
        /// <param name="spriteBatch">The SpriteBatch.</param>
        /// <param name="gameTime">The GameTime.</param>
        public virtual void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            IEnumerable<DrawableGameComponent> components = Components.GetDrawables();
            foreach (DrawableGameComponent gameComponent in components)
            {
                gameComponent.Draw(spriteBatch, gameTime);
            }
        }

        /// <summary>
        /// Processes the game setup
        /// </summary>
        /// <param name="launchParameters">The LaunchParameters.</param>
        public virtual void Setup(LaunchParameters launchParameters)
        {
            GraphicsManager = new GLGraphicsManager
            {
                PreferredBackBufferWidth = 800,
                PreferredBackBufferHeight = 480
            };

            Content.RootPath = "Content";

            SoundManager = new WaveOutSoundManager();
        }

        /// <summary>
        /// Initializes the game
        /// </summary>
        public virtual void Initialize()
        {
            
        }

        /// <summary>
        /// Runs the game.
        /// </summary>
        public void Run()
        {
            GameHost.Initialize(this);
        }

        /// <summary>
        /// Processes the Game load.
        /// </summary>
        public abstract void LoadContent();

        /// <summary>
        /// Processes the Game unload.
        /// </summary>
        public virtual void Unload()
        {
        }

        /// <summary>
        /// Processes if the surface is activated.
        /// </summary>
        public virtual void OnActivation()
        {
        }

        /// <summary>
        /// Processes if the surface is deactivated.
        /// </summary>
        public virtual void OnDeactivation()
        {
        }

        /// <summary>
        /// Exits the game.
        /// </summary>
        public void Exit()
        {
            GameHost.Shutdown();
        }

        /// <summary>
        /// Restarts the Game with the specified LaunchParameters.
        /// </summary>
        /// <param name="launchParameters">The LaunchParameters.</param>
        public void Restart(LaunchParameters launchParameters)
        {
            GameHost.Restart(launchParameters.ToString());
        }

        /// <summary>
        /// Gets a component.
        /// </summary>
        /// <typeparam name="T">The Type.</typeparam>
        /// <returns>IComponent.</returns>
        public T Get<T>() where T : IComponent
        {
            return GameHost.Get<T>();
        }
    }
}
