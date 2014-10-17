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

using Sharpex2D.Content;
using Sharpex2D.Entities;
using Sharpex2D.UI;

namespace Sharpex2D.Rendering.Scene
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Tested)]
    public abstract class Scene
    {
        /// <summary>
        /// Initializes the Scene class.
        /// </summary>
        protected Scene()
        {
            EntityEnvironment = new EntityEnvironment();
            UIManager = new UIManager();
        }

        /// <summary>
        /// Sets or gets the EntityEnvironment.
        /// </summary>
        public EntityEnvironment EntityEnvironment { get; set; }

        /// <summary>
        /// Sets or gets the UIManager.
        /// </summary>
        public UIManager UIManager { set; get; }

        /// <summary>
        /// Updates the object.
        /// </summary>
        /// <param name="gameTime">The GameTime.</param>
        public abstract void Update(GameTime gameTime);

        /// <summary>
        /// Processes a Render.
        /// </summary>
        /// <param name="spriteBatch">The SpriteBatch.</param>
        /// <param name="gameTime">The GameTime.</param>
        public abstract void Draw(SpriteBatch spriteBatch, GameTime gameTime);

        /// <summary>
        /// Initializes the scene.
        /// </summary>
        public abstract void Initialize();

        /// <summary>
        /// Called if the scene is activated by SceneManager.
        /// </summary>
        public virtual void OnSceneActivated()
        {
        }

        /// <summary>
        /// Called if the scene is deactivated by SceneManager.
        /// </summary>
        public virtual void OnSceneDeactivated()
        {
        }

        /// <summary>
        /// Loads the content.
        /// </summary>
        /// <param name="content">The ContentManager.</param>
        public abstract void LoadContent(ContentManager content);
    }
}