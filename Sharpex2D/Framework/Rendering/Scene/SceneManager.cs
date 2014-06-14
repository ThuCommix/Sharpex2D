using System;
using System.Collections.Generic;
using Sharpex2D.Framework.Components;
using Sharpex2D.Framework.Content;
using Sharpex2D.Framework.Events;
using Sharpex2D.Framework.Game;
using Sharpex2D.Framework.Rendering.Scene.Events;

namespace Sharpex2D.Framework.Rendering.Scene
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Tested)]
    public class SceneManager : IUpdateable, IConstructable
    {
        #region IComponent Implementation

        /// <summary>
        ///     Sets or gets the Guid of the Component.
        /// </summary>
        public Guid Guid
        {
            get { return new Guid("00C8ED49-0C1B-47A8-B990-C71F5C4CB09E"); }
        }

        #endregion

        #region IGameHandler Implementation

        /// <summary>
        ///     Called if the SceneManager should get initialized.
        /// </summary>
        public void Construct()
        {
            _eventManager = SGL.Components.Get<EventManager>();
        }

        /// <summary>
        ///     Updates the object.
        /// </summary>
        /// <param name="gameTime">The GameTime.</param>
        public void Update(GameTime gameTime)
        {
            if (ActiveScene != null)
            {
                ActiveScene.Update(gameTime);
            }
        }

        /// <summary>
        ///     Processes a Render.
        /// </summary>
        /// <param name="renderer">The Renderer.</param>
        /// <param name="gameTime">The GameTime.</param>
        public void Render(IRenderer renderer, GameTime gameTime)
        {
            if (ActiveScene != null)
            {
                ActiveScene.Render(renderer, gameTime);
            }
        }

        #endregion

        private readonly List<Scene> _scenes;
        private EventManager _eventManager;

        /// <summary>
        ///     Initializes a new SceneManager class.
        /// </summary>
        public SceneManager()
        {
            _scenes = new List<Scene>();
        }

        /// <summary>
        ///     Gets the ActiveScene.
        /// </summary>
        public Scene ActiveScene { get; private set; }

        /// <summary>
        ///     Gets a specified scene.
        /// </summary>
        /// <typeparam name="T">The Scene.</typeparam>
        /// <returns>Scene.</returns>
        public T Get<T>() where T : Scene
        {
            for (int i = 0; i <= _scenes.Count - 1; i++)
            {
                if (_scenes[i].GetType() == typeof (T))
                {
                    return (T) _scenes[i];
                }
            }

            throw new InvalidOperationException("The scene " + typeof (T).Name + " is not available.");
        }

        /// <summary>
        ///     Sets the ActiveScene.
        /// </summary>
        /// <param name="scene">The Scene.</param>
        public void SetScene(Scene scene)
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
        ///     Adds a new Scene.
        /// </summary>
        /// <param name="scene">The Scene.</param>
        public void AddScene(Scene scene)
        {
            scene.Initialize();
            scene.LoadContent(SGL.Components.Get<ContentManager>());
            _scenes.Add(scene);
        }

        /// <summary>
        ///     Removes a Scene.
        /// </summary>
        /// <param name="scene">The Scene.</param>
        public void RemoveScene(Scene scene)
        {
            _scenes.Remove(scene);
        }

        /// <summary>
        ///     Removes all Scenes.
        /// </summary>
        public void ClearScenes()
        {
            _scenes.Clear();
        }
    }
}