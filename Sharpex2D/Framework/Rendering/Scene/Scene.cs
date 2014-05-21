using Sharpex2D.Framework.Content;
using Sharpex2D.Framework.Entities;
using Sharpex2D.Framework.UI;

namespace Sharpex2D.Framework.Rendering.Scene
{
    public abstract class Scene
    {
        /// <summary>
        /// Processes a Tick.
        /// </summary>
        /// <param name="elapsed">The Elapsed.</param>
        public abstract void Tick(float elapsed);
        /// <summary>
        /// Processes a Render.
        /// </summary>
        /// <param name="renderer">The Renderer.</param>
        /// <param name="elapsed">The Elapsed.</param>
        public abstract void Render(IRenderer renderer, float elapsed);
        /// <summary>
        /// Initializes the scene.
        /// </summary>
        public abstract void Initialize();
        /// <summary>
        /// Loads the content.
        /// </summary>
        /// <param name="content">The ContentManager.</param>
        public abstract void LoadContent(ContentManager content);
        /// <summary>
        /// Sets or gets the EntityEnvironment.
        /// </summary>
        public EntityEnvironment EntityEnvironment { get; set; }
        /// <summary>
        /// Sets or gets the UIManager.
        /// </summary>
        public UIManager UIManager { set; get; }
        /// <summary>
        /// Initializes the Scene class.
        /// </summary>
        protected Scene()
        {
            EntityEnvironment = new EntityEnvironment();
            UIManager = new UIManager();
        }
    }
}
