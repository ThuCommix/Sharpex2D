using SharpexGL.Framework.Content;
using SharpexGL.Framework.Math;

namespace SharpexGL.Framework.Rendering.Scene
{
    public interface IScene
    {
        /// <summary>
        /// Rendering Texture.
        /// </summary>
        Texture Texture { set; get; }

        /// <summary>
        /// The position on which the scene should be rendered.
        /// </summary>
        Vector2 Position { set; get; }

        /// <summary>
        /// The alpha color, which get overdrawed on the scene
        /// </summary>
        Color AlphaColor { set; get; }

        /// <summary>
        /// Sets or gets if the scene should be updated.
        /// </summary>
        bool AllowUpdate { set; get; }

        /// <summary>
        /// Sets or gets if the scene should be rendered.
        /// </summary>
        bool AllowRender { set; get; }

        /// <summary>
        /// Called if the scene should be updated.
        /// </summary>
        /// <param name="elapsed">The GameTime.</param>
        void Update(float elapsed);

        /// <summary>
        /// Called if the scene should be rendered.
        /// </summary>
        /// <param name="graphicRenderer">The GraphicRenderer.</param>
        /// <param name="elapsed">The Elapsed.</param>
        void Render(IGraphicRenderer graphicRenderer, float elapsed);

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
