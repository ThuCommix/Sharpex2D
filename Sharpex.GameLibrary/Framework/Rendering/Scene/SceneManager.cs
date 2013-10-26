using System;
using System.Collections.Generic;
using SharpexGL.Framework.Game;
using SharpexGL.Framework.Game.Timing;

namespace SharpexGL.Framework.Rendering.Scene
{
    public class SceneManager : IDisposable, IGameHandler
    {
        #region IGameHandler Implementation

        /// <summary>
        /// Called if the SceneManager should get initialized.
        /// </summary>
        public void Construct()
        {
            _scenes = new List<IScene>();
            SGL.Components.Get<GameLoop>().Subscribe(this);
        }

        /// <summary>
        /// Processes a Tick.
        /// </summary>
        /// <param name="elapsed">The Elapsed.</param>
        public void Tick(float elapsed)
        {
            if (ActiveScene != null)
            {
                ActiveScene.Tick(elapsed);
            }
        }

        /// <summary>
        /// Processes a Render.
        /// </summary>
        /// <param name="renderer">The Renderer.</param>
        /// <param name="elapsed">The Elapsed.</param>
        public void Render(IRenderer renderer, float elapsed)
        {
            if (ActiveScene != null)
            {
                ActiveScene.Render(renderer, elapsed);
            }
        }

        #endregion

        #region IDisposable Implementation

        /// <summary>
        /// Disposes the object.
        /// </summary>
        public void Dispose()
        {
            _scenes.Clear();
        }

        #endregion

        private List<IScene> _scenes;

        /// <summary>
        /// Gets the ActiveScene.
        /// </summary>
        public IScene ActiveScene { get; private set; }

        /// <summary>
        /// Gets a specified scene.
        /// </summary>
        /// <typeparam name="T">The Scene.</typeparam>
        /// <returns>Scene.</returns>
        public T Get<T>() where T : IScene
        {
            for (var i = 0; i <= _scenes.Count - 1; i++)
            {
                if (_scenes[i].GetType() == typeof (T))
                {
                    return (T)_scenes[i];
                }
            }

            throw new InvalidOperationException("The scene " + typeof (T).Name + " is not available.");
        }

        /// <summary>
        /// Sets the ActiveScene.
        /// </summary>
        /// <param name="scene">The Scene.</param>
        public void SetScene(IScene scene)
        {
            ActiveScene = scene;
        }

        /// <summary>
        /// Adds a new Scene.
        /// </summary>
        /// <param name="scene">The Scene.</param>
        public void AddScene(IScene scene)
        {
            _scenes.Add(scene);
        }

        /// <summary>
        /// Removes a Scene.
        /// </summary>
        /// <param name="scene">The Scene.</param>
        public void RemoveScene(IScene scene)
        {
            _scenes.Remove(scene);
        }

        /// <summary>
        /// Removes all Scenes.
        /// </summary>
        public void ClearScenes()
        {
            _scenes.Clear();
        }
    }
}
