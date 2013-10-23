using System;
using System.Collections.Generic;
using SharpexGL.Framework.Game;
using SharpexGL.Framework.Game.Timing;

namespace SharpexGL.Framework.Rendering.Scene
{
    public class SceneManager : IDisposable, IGameHandler
    {
        /// <summary>
        /// Enumeration of all scenes.
        /// </summary>
        public List<IScene> Scenes { private set; get; }

        /// <summary>
        /// Determines, if the SceneManager is disposed.
        /// </summary>
        public bool IsDisposed { private set; get; }

        /// <summary>
        /// Gets or sets, if the scenes should be updated with internal loop.That means, you dont need to manually call UpdateScene(s).
        /// </summary>
        public bool AutoUpdateScenes { set; get; }

        /// <summary>
        /// Adds a new scene to the enumeration.
        /// </summary>
        /// <param name="scene">The Scene.</param>
        public void AddScene(IScene scene)
        {
            Scenes.Add(scene);
        }

        /// <summary>
        /// Removes the given scene from the enumeration.
        /// </summary>
        /// <param name="scene">The Scene.</param>
        public void RemoveScene(IScene scene)
        {
            Scenes.Remove(scene);
        }

        /// <summary>
        /// Clears all scenes fro the enumeration.
        /// </summary>
        public void ClearScenes()
        {
            Scenes.Clear();
        }

        /// <summary>
        /// Returns a scene object.
        /// </summary>
        /// <param name="index">The Index.</param>
        /// <returns>IScene</returns>
        public IScene GetScene(int index)
        {
            if ((index > Scenes.Count - 1) | (index < 0))
                throw new ArgumentOutOfRangeException("index");

            return Scenes[index];
        }

        /// <summary>
        /// Renders a single scene specified by index.
        /// </summary>
        /// <param name="index">The Index.</param>
        /// <param name="graphicRenderer">The GraphicRenderer.</param>
        public void RenderScene(int index, IRenderer graphicRenderer)
        {
            if ((index > Scenes.Count - 1) | (index < 0))
                throw new ArgumentOutOfRangeException("index");

            var scene = Scenes[index];
            if (!scene.AllowRender) return;
            graphicRenderer.DrawTexture(scene.Texture, scene.Position, scene.AlphaColor);
        }

        /// <summary>
        /// Renders a single scene specified by given scene.
        /// </summary>
        /// <param name="scene">The Scene.</param>
        /// <param name="graphicRenderer">The GraphicRenderer.</param>
        public void RenderScene(IScene scene, IRenderer graphicRenderer)
        {
            if (!scene.AllowRender) return;
            graphicRenderer.DrawTexture(scene.Texture, scene.Position, scene.AlphaColor);
        }

        /// <summary>
        /// Renders the entire enumeration of scenes.
        /// </summary>
        /// <param name="graphicRenderer">The GraphicRenderer.</param>
        public void RenderScenes(IRenderer graphicRenderer)
        {
            foreach (var scene in Scenes)
            {
                if (scene.AllowRender)
                {
                    graphicRenderer.DrawTexture(scene.Texture, scene.Position, scene.AlphaColor);
                }
            }
        }

        /// <summary>
        /// Updates a single scene specified by index.
        /// </summary>
        /// <param name="index">The Index.</param>
        /// <param name="elapsed">The Elapsed.</param>
        public void UpdateScene(int index, float elapsed)
        {
            if ((index > Scenes.Count - 1) | (index < 0))
                throw new ArgumentOutOfRangeException("index");

            var scene = Scenes[index];
            if (!scene.AllowUpdate) return;
            scene.Update(elapsed);
        }

        /// <summary>
        /// Updates a single scene specified by given scene.
        /// </summary>
        /// <param name="scene">The Scene.</param>
        /// <param name="elapsed">The Elapsed.</param>
        public void UpdateScene(IScene scene, float elapsed)
        {
            if (!scene.AllowUpdate) return;
            scene.Update(elapsed);
        }

        /// <summary>
        /// Updates the entire enumeration of scenes.
        /// </summary>
        /// <param name="elapsed">The Elapsed.</param>
        public void UpdateScenes(float elapsed)
        {
            foreach (var scene in Scenes)
            {
                if (scene.AllowUpdate)
                {
                    scene.Update(elapsed);
                }
            }
        }

        /// <summary>
        /// Disposes the current SceneManager.
        /// </summary>
        public void Dispose()
        {
            IsDisposed = true;
            Scenes.Clear();
        }

        /// <summary>
        /// Updates the SceneManager.
        /// </summary>
        /// <param name="elapsed">The Elapsed.</param>
        public void Tick(float elapsed)
        {
            if (AutoUpdateScenes)
            {
                UpdateScenes(elapsed);
            }
        }

        /// <summary>
        /// Processes a Render.
        /// </summary>
        /// <param name="renderer">The GraphicRenderer.</param>
        /// <param name="elapsed">The Elapsed.</param>
        public void Render(IRenderer renderer, float elapsed)
        {
            
        }

        /// <summary>
        /// Called if the SceneManager should get initialized.
        /// </summary>
        public void Construct()
        {
            Scenes = new List<IScene>();
            SGL.Components.Get<GameLoop>().Subscribe(this);
        }
    }
}
