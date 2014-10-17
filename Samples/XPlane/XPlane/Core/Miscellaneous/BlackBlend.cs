using Sharpex2D;
using Sharpex2D.Math;
using Sharpex2D.Rendering;

namespace XPlane.Core.Miscellaneous
{
    public class BlackBlend : IDrawable, IUpdateable
    {
        private readonly Rectangle _display;
        private int _alpha;
        private bool _fadeIn;

        /// <summary>
        /// Initializes a new BlackBlend class.
        /// </summary>
        public BlackBlend()
        {
            _display = new Rectangle(0, 0, 800, 480);
        }

        /// <summary>
        /// A value indicating whether the black blend is enabled.
        /// </summary>
        public bool IsEnabled { set; get; }

        /// <summary>
        /// A value indicating the BlackBlend should FadeIn.
        /// </summary>
        public bool FadeIn
        {
            set
            {
                _fadeIn = value;
                if (value)
                {
                    _alpha = 0;
                }
                else
                {
                    _alpha = 255;
                }
            }
            get { return _fadeIn; }
        }

        /// <summary>
        /// A value indicating whether the effect is completed.
        /// </summary>
        public bool IsCompleted { private set; get; }

        /// <summary>
        /// Draws the BlackBlend.
        /// </summary>
        /// <param name="spriteBatch">The spriteBatch.</param>
        /// <param name="gameTime">The GameTime.</param>
        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (!IsEnabled) return;
            spriteBatch.FillRectangle(Color.FromArgb(_alpha, 0, 0, 0), _display);
        }

        /// <summary>
        /// Updates the BlackBlend.
        /// </summary>
        /// <param name="gameTime">The GameTime.</param>
        public void Update(GameTime gameTime)
        {
            if (!IsEnabled) return;

            if (FadeIn)
            {
                if (_alpha < 253)
                {
                    _alpha += 2;
                }
                else
                {
                    IsCompleted = true;
                }
            }
            else
            {
                if (_alpha > 2)
                {
                    _alpha -= 2;
                }
                else
                {
                    IsCompleted = true;
                }
            }
        }
    }
}