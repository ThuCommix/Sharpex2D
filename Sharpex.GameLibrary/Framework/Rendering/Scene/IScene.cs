using SharpexGL.Framework.Content;
using SharpexGL.Framework.Math;

namespace SharpexGL.Framework.Rendering.Scene
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
        /// <param name="graphicRenderer">The GraphicRenderer.</param>
        /// <param name="elapsed">The Elapsed.</param>
        void Render(IRenderer graphicRenderer, float elapsed);

        /// <summary>
        /// Called if the scene should be initialized.
        /// </summary>
        void Initialize();

        /// <summary>
        /// Called if the scene should load its content.
        /// </summary>
        /// <param name="content">The ContentManager.</param>
        void LoadContent(ContentManager content);
    }
}
