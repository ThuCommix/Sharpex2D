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

using System;
using System.Linq;
using Sharpex2D.Framework.UI;

namespace Sharpex2D.Framework.Rendering
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Tested)]
    public abstract class Scene : IUpdateable, IDrawable, IDisposable
    {
        /// <summary>
        /// Gets the associated game
        /// </summary>
        public Game Game { get; }

        /// <summary>
        /// Gets the UI manager
        /// </summary>
        public UIManager UIManager { get; }

        /// <summary>
        /// A value indicating whether the scene is attached to the scene manager
        /// </summary>
        public bool IsAttached
        {
            get { return Game.SceneManager.Any(x => x == this); }
        }

        /// <summary>
        /// A value indicating whether this scene is currently active
        /// </summary>
        public bool IsActive => Game.SceneManager.ActiveScene == this;

        /// <summary>
        /// Initializes a new Scene class
        /// </summary>
        protected Scene()
        {
            Game = GameHost.GameInstance;
            UIManager = new UIManager();
        }

        /// <summary>
        /// Deconstructs the scene
        /// </summary>
        ~Scene()
        {
            Dispose(false);
        }

        /// <summary>
        /// Updates the scene
        /// </summary>
        /// <param name="gameTime">The GameTime</param>
        public abstract void OnUpdate(GameTime gameTime);

        /// <summary>
        /// Draws the scene
        /// </summary>
        /// <param name="spriteBatch">The SpriteBatch</param>
        /// <param name="gameTime">The GameTime</param>
        public abstract void OnDraw(SpriteBatch spriteBatch, GameTime gameTime);

        /// <summary>
        /// Updates the scene
        /// </summary>
        /// <param name="gameTime">The GameTime</param>
        void IUpdateable.Update(GameTime gameTime)
        {
            OnUpdate(gameTime);
        }

        /// <summary>
        /// Draws the scene
        /// </summary>
        /// <param name="spriteBatch">The SpriteBatch</param>
        /// <param name="gameTime">The GameTime</param>
        void IDrawable.Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            OnDraw(spriteBatch, gameTime);
        }

        /// <summary>
        /// Raises when the scene is activated
        /// </summary>
        public virtual void OnSceneActivated()
        {
            
        }

        /// <summary>
        /// Raises when the scene is deactivated
        /// </summary>
        public virtual void OnSceneDeactivated()
        {
            
        }

        /// <summary>
        /// Disposes the object
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Disposes the object
        /// </summary>
        /// <param name="disposing">The disposing state</param>
        protected virtual void Dispose(bool disposing)
        {
            
        }
    }
}
