using Sharpex2D;
using Sharpex2D.Math;
using Sharpex2D.Rendering;

namespace XPlane.Core.Miscellaneous
{
    public class ScoreIndicator : IDrawable, IUpdateable
    {
        /// <summary>
        /// Gets the life time in ms.
        /// </summary>
        public float LifeTime { get; private set; }

        /// <summary>
        /// A value indicating whether the score indicator is visible.
        /// </summary>
        public bool IsVisible { private set; get; }

        /// <summary>
        /// Gets or sets the displayed score.
        /// </summary>
        public int Score { set; get; }

        /// <summary>
        /// Gets or sets the Position.
        /// </summary>
        public Vector2 Position { set; get; }

        /// <summary>
        /// Gets or sets the Color.
        /// </summary>
        public Color Color { get; set; }

        private readonly Font _font;

        /// <summary>
        /// Initializes a new ScoreIndicator class.
        /// </summary>
        public ScoreIndicator()
        {
            LifeTime = 750;
            Position = new Vector2(0);
            IsVisible = true;
            _font = new Font("Segoe UI", 18, TypefaceStyle.Bold);
        }

        /// <summary>
        /// Draws the DamageIndicator.
        /// </summary>
        /// <param name="spriteBatch">The spriteBatch.</param>
        /// <param name="gameTime">The GameTime.</param>
        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (!IsVisible) return;

            spriteBatch.DrawString(string.Format("+ {0}", Score), _font, Position, Color);
        }

        /// <summary>
        /// Updates the DamageIndicator.
        /// </summary>
        /// <param name="gameTime">The GameTime.</param>
        public void Update(GameTime gameTime)
        {
            if (!IsVisible) return;

            LifeTime -= gameTime.ElapsedGameTime;
            if (LifeTime <= 0)
            {
                LifeTime = 0;
                IsVisible = false;
            }

            Position = new Vector2(Position.X, Position.Y - 0.5f);
        }
    }
}
