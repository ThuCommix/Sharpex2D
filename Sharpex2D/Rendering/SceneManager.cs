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
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Sharpex2D.Framework.Rendering
{
    public class SceneManager : DrawableGameComponent, IEnumerable<Scene>, IComponent
    {
        private readonly List<Scene> _scenes;
        private Scene _activeScene;

        /// <summary>
        /// Gets or sets the active scene
        /// </summary>
        public Scene ActiveScene
        {
            set
            {
                _activeScene = value;
                SceneChanged?.Invoke(this, EventArgs.Empty);
            }
            get { return _activeScene; }
        }

        /// <summary>
        /// Raises when the active scene changed
        /// </summary>
        public event EventHandler<EventArgs> SceneChanged;

        /// <summary>
        /// Raises when a scene was removed
        /// </summary>
        public event EventHandler<EventArgs> SceneRemoved;

        /// <summary>
        /// Raises when a scene was added
        /// </summary>
        public event EventHandler<EventArgs> SceneAdded; 

        /// <summary>
        /// Initializes a new SceneManager class
        /// </summary>
        /// <param name="game">The Game</param>
        internal SceneManager(Game game) : base(game)
        {
            _scenes = new List<Scene>();
        }

        /// <summary>
        /// Adds a new scene
        /// </summary>
        /// <param name="scene">The Scene</param>
        public void Add(Scene scene)
        {
            if (_scenes.Contains(scene)) return;

            _scenes.Add(scene);
            SceneAdded?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Remove s a scene
        /// </summary>
        /// <param name="scene">The Scene</param>
        public void Remove(Scene scene)
        {
            if (!_scenes.Contains(scene)) return;

            _scenes.Remove(scene);
            SceneRemoved?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Clears all scenes
        /// </summary>
        public void Clear()
        {
            _scenes.Clear();
        }

        /// <summary>
        /// Gets a scene specified by the type
        /// </summary>
        /// <typeparam name="T">The Type</typeparam>
        /// <returns>Scene</returns>
        public Scene Get<T>() where T : Scene
        {
            var result = _scenes.OfType<T>().FirstOrDefault();
            if(result == null)
                throw new KeyNotFoundException("The scene was not found.");
            return result;
        }

        /// <summary>
        /// Updates the active scene
        /// </summary>
        /// <param name="gameTime">The GameTime</param>
        public override void Update(GameTime gameTime)
        {
            ActiveScene?.OnUpdate(gameTime);
        }

        /// <summary>
        /// Draws the active scene
        /// </summary>
        /// <param name="spriteBatch">The SpriteBatch</param>
        /// <param name="gameTime">The GameTime</param>
        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            ActiveScene?.OnDraw(spriteBatch, gameTime);
        }

        /// <summary>
        /// Disposes the object
        /// </summary>
        /// <param name="disposing">The disposing state</param>
        public override void Dispose(bool disposing)
        {
            foreach (var scene in _scenes)
            {
                scene.Dispose();
            }

            _scenes.Clear();
        }

        /// <summary>
        /// Gets the scene specified by the index
        /// </summary>
        /// <param name="index">The Index</param>
        /// <returns>Scene</returns>
        public Scene this[int index] => _scenes[index];

        /// <summary>
        /// Gets the enumerator
        /// </summary>
        /// <returns>Enumerator</returns>
        public IEnumerator<Scene> GetEnumerator()
        {
            return _scenes.GetEnumerator();
        }

        /// <summary>
        /// Gets the enumerator
        /// </summary>
        /// <returns>Enumerator</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}

