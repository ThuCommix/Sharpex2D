using Sharpex2D.Framework.Content;
using Sharpex2D.Framework.Entities;
using Sharpex2D.Framework.Game;
using Sharpex2D.Framework.UI;

namespace Sharpex2D.Framework.Rendering.Scene
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Tested)]
    public abstract class Scene
    {
        /// <summary>
        ///     Initializes the Scene class.
        /// </summary>
        protected Scene()
        {
            EntityEnvironment = new EntityEnvironment();
            UIManager = new UIManager();
        }

        /// <summary>
        ///     Sets or gets the EntityEnvironment.
        /// </summary>
        public EntityEnvironment EntityEnvironment { get; set; }

        /// <summary>
        ///     Sets or gets the UIManager.
        /// </summary>
        public UIManager UIManager { set; get; }

        /// <summary>
        ///     Updates the object.
        /// </summary>
        /// <param name="gameTime">The GameTime.</param>
        public abstract void Update(GameTime gameTime);

        /// <summary>
        ///     Processes a Render.
        /// </summary>
        /// <param name="renderer">The Renderer.</param>
        /// <param name="gameTime">The GameTime.</param>
        public abstract void Render(IRenderer renderer, GameTime gameTime);

        /// <summary>
        ///     Initializes the scene.
        /// </summary>
        public abstract void Initialize();

        /// <summary>
        ///     Loads the content.
        /// </summary>
        /// <param name="content">The ContentManager.</param>
        public abstract void LoadContent(ContentManager content);
    }
}