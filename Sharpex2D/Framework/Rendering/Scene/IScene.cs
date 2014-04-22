using Sharpex2D.Framework.Content;
using Sharpex2D.Framework.Entities;

namespace Sharpex2D.Framework.Rendering.Scene
{
    public interface IScene
    {
        /// <summary>
        /// Called if the scene should be updated.
        /// </summary>
        /// <param name="elapsed">The GameTime.</param>
        void Tick(float elapsed);

        /// <summary>
        /// Called if the scene should be rendered.
        /// </summary>
        /// <param name="renderer">The Renderer.</param>
        /// <param name="elapsed">The Elapsed.</param>
        void Render(IRenderer renderer, float elapsed);

        /// <summary>
        /// Called if the scene should be initialized.
        /// </summary>
        void Initialize();

        /// <summary>
        /// Called if the scene should load its content.
        /// </summary>
        /// <param name="content">The ContentManager.</param>
        void LoadContent(ContentManager content);

        /// <summary>
        /// The Current EntityEnvironment.
        /// </summary>
        EntityEnvironment EntityEnvironment
        {
            get;
            set;
        }
    }
}
