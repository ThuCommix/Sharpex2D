using SharpexGL.Framework.Components;
using SharpexGL.Framework.Rendering;

namespace SharpexGL.Framework.Game
{
    public interface IGameHandler : IConstructable
    {
        /// <summary>
        /// Processes a Game tick.
        /// </summary>
        /// <param name="elapsed">The Elapsed.</param>
        void Tick(float elapsed);
        /// <summary>
        /// Processes a Render.
        /// </summary>
        /// <param name="renderer">The GraphicRenderer.</param>
        /// <param name="elapsed">The Elapsed.</param>
        void Render(IRenderer renderer, float elapsed);
    }
}
