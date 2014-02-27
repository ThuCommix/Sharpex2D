using System;
using System.Collections.Generic;
using SharpexGL.Framework.Content;
using SharpexGL.Framework.Events;
using SharpexGL.Framework.Game;
using SharpexGL.Framework.Rendering.Scene.Events;

namespace SharpexGL.Framework.Rendering.Scene
{
    public class SceneManager : IDisposable, IGameHandler
    {
        #region IComponent Implementation

        /// <summary>
        /// Sets or gets the Guid of the Component.
        /// </summary>
        public Guid Guid
        {
            get { return new Guid("00C8ED49-0C1B-47A8-B990-C71F5C4CB09E"); }
        }

        #endregion

        #region IGameHandler Implementation

        /// <summary>
        /// Called if the SceneManager should get initialized.
        /// </summary>
        public void Construct()
        {
            _eventManager = SGL.Components.Get<EventManager>();
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

        /// <summary>
        /// Initializes a new SceneManager class.
        /// </summary>
        public SceneManager()
        {
            _scenes = new List<IScene>();
        }

        private readonly List<IScene> _scenes;
        private EventManager _eventManager;

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
            if (_eventManager != null && ActiveScene != null)
            {
                //publish event
                _eventManager.Publish(new BeforeSceneChangedEvent(ActiveScene));
            }

            ActiveScene = scene;

            if (_eventManager != null && ActiveScene != null)
            {
                //publish event
                _eventManager.Publish(new AfterSceneChangedEvent(ActiveScene));
            }

        }

        /// <summary>
        /// Adds a new Scene.
        /// </summary>
        /// <param name="scene">The Scene.</param>
        public void AddScene(IScene scene)
        {
            scene.Initialize();
            scene.LoadContent(SGL.Components.Get<ContentManager>());
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
