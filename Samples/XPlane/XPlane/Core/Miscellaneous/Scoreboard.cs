using Sharpex2D;
using Sharpex2D.Math;
using Sharpex2D.Rendering;

namespace XPlane.Core.Miscellaneous
{
    public class Scoreboard : IDrawable
    {
        private readonly Font _healthFont;
        private readonly Vector2 _healthPosition;
        private readonly Font _scoreFont;
        private readonly Vector2 _scorePosition;

        /// <summary>
        /// Initializes a new Scoreboard class.
        /// </summary>
        public Scoreboard()
        {
            _scoreFont = new Font("Segoe UI", 30, TypefaceStyle.Bold);
            _healthFont = new Font("Segoe UI", 20, TypefaceStyle.Bold);
            _scorePosition = new Vector2(5, 0);
            _healthPosition = new Vector2(5, 40);
        }

        /// <summary>
        /// Gets or sets the current score.
        /// </summary>
        public int CurrentScore { set; get; }

        /// <summary>
        /// Gets or sets the current health.
        /// </summary>
        public int CurrentHealth { set; get; }

        /// <summary>
        /// Renders the score board.
        /// </summary>
        /// <param name="renderer">The Renderer.</param>
        /// <param name="gameTime">The GameTime.</param>
        public void Render(RenderDevice renderer, GameTime gameTime)
        {
            renderer.DrawString(string.Format("score: {0}", CurrentScore), _scoreFont, _scorePosition, Color.White);
            renderer.DrawString(string.Format("health: {0}", CurrentHealth), _healthFont, _healthPosition, Color.White);
        }
    }
}