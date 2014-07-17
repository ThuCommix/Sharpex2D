using System.Globalization;
using Sharpex2D.Math;
using Sharpex2D.Rendering;

namespace FlyingBird.Misc
{
    public class Scoreboard
    {
        private readonly Font _font;

        /// <summary>
        ///     Initializes a new Scoreboard class.
        /// </summary>
        public Scoreboard()
        {
            _font = new Font("Segoe UI", 45, TypefaceStyle.Bold);
        }

        /// <summary>
        ///     Gets or sets the Score.
        /// </summary>
        public int Score { set; get; }

        /// <summary>
        ///     Renders the object.
        /// </summary>
        /// <param name="renderer">The Renderer.</param>
        public void Render(RenderDevice renderer)
        {
            Vector2 dim = renderer.MeasureString(Score.ToString(CultureInfo.InvariantCulture), _font);
            renderer.DrawString(Score.ToString(CultureInfo.InvariantCulture), _font, new Vector2(320 - (dim.X/2), 50),
                Color.White);
        }
    }
}